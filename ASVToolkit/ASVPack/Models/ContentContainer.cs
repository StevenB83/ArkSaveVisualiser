using SavegameToolkit;
using SavegameToolkit.Types;
using SavegameToolkitAdditions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ASVPack.Extensions;
using SavegameToolkit.Propertys;
using SavegameToolkit.Arrays;
using System.Collections.Concurrent;
using SavegameToolkit.Structs;
using SavegameToolkit.Data;
using System.Diagnostics;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentContainer
    {

        Dictionary<string, Tuple<float, float, float, float>> latlonCalcs = new Dictionary<string, Tuple<float, float, float, float>>
        {
            { "theisland", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "hope", Tuple.Create(50f, 6850f, 50f, 6850f) },
            { "thecenter", Tuple.Create(30.34223747253418f, 9584.0f, 55.10416793823242f, 9600.0f) },
            { "scorchedearth_p", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "aberration_p", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "extinction", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "valhalla", Tuple.Create(48.813560485839844f, 14750.0f, 48.813560485839844f, 14750.0f) },
            { "mortemtupiu", Tuple.Create(32.479148864746094f, 20000.0f, 40.59893798828125f, 16000.0f) },
            { "shigoislands", Tuple.Create(50.001777870738339260f, 9562.0f, 50.001777870738339260f, 9562.0f) },
            { "ragnarok", Tuple.Create(50.009388f, 13100f, 50.009388f, 13100f) },
            { "thevolcano", Tuple.Create(50.0f, 9181.0f, 50.0f, 9181.0f) },
            { "pgark", Tuple.Create(0.0f, 6080.0f, 0.0f, 6080.0f) },
            { "crystalisles" , Tuple.Create(48.7f, 16000f, 50.0f, 17000.0f) },
            { "valguero_p" , Tuple.Create(50.0f, 8161.0f, 50.0f, 8161.0f) },
            { "genesis", Tuple.Create(50.0f, 10500.0f, 50.0f, 10500.0f)},
            { "gen2", Tuple.Create(49.6f, 14500.0f, 49.6f, 14500.0f)},
            { "astralark", Tuple.Create(50.0f, 2000.0f, 50.0f, 2000.0f)},
            { "tunguska_p", Tuple.Create(46.8f, 14000.0f,49.29f, 13300.0f) },
            { "caballus_p", Tuple.Create(50.0f, 8125.0f,50.0f, 8125.0f)},
            { "viking_p", Tuple.Create(50.0f, 7140.0f,50.0f, 7140.0f)},
            { "tiamatprime", Tuple.Create(50.0f, 8000.0f,50.0f, 8000.0f)},
            { "glacius_p", Tuple.Create(50.0f, 16250.0f,50.0f, 16250.0f)},
            { "antartika", Tuple.Create(50.0f, 8000.0f,50.0f, 8000.0f)},
            { "lostisland", Tuple.Create(48.7f, 16000.0f,50.0f, 15500.0f)}
        };

        Tuple<float, float, float, float> mapLatLonCalcs = new Tuple<float, float, float, float>(50.0f, 8000.0f, 50.0f, 8000.0f); //default to same as The Island map

        [DataMember] public string MapName { get; set; } = "";
        [DataMember] public List<ContentStructure> MapStructures { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentWildCreature> WildCreatures { get; set; } = new List<ContentWildCreature>();
        [DataMember] public List<ContentTribe> Tribes { get; set; } = new List<ContentTribe>();
        [DataMember] public List<ContentDroppedItem> DroppedItems { get; set; } = new List<ContentDroppedItem>();
        [DataMember] public DateTime GameSaveTime { get; set; }  = new DateTime();
        [DataMember] public float GameSeconds { get; set; } = 0;

        private void LoadDefaults()
        {
            GameSaveTime = DateTime.MinValue;
            GameSeconds = 0;
            MapStructures = new List<ContentStructure>();
            WildCreatures = new List<ContentWildCreature>();
            DroppedItems = new List<ContentDroppedItem>();
            Tribes = new List<ContentTribe>();


        }

        public void LoadSaveGame(string fileName)
        {
            LoadDefaults();

            long startTicks = DateTime.Now.Ticks;
            try
            {
               

                latlonCalcs.TryGetValue(Path.GetFileNameWithoutExtension(fileName).ToLower(), out mapLatLonCalcs);

                List<ContentTribe> tribeContentList = new List<ContentTribe>();

                GameObjectContainer objectContainer = null;
                GameSaveTime = new FileInfo(fileName).LastWriteTimeUtc.ToLocalTime();

                using (Stream stream = new MemoryStream(File.ReadAllBytes(fileName)))
                {
                    using (ArkArchive archive = new ArkArchive(stream))
                    {

                        ArkSavegame arkSavegame = new ArkSavegame();

                        arkSavegame.ReadBinary(archive, ReadingOptions.Create()
                                .WithThreadCount(int.MaxValue)
                                .WithDataFiles(false)
                                .WithEmbeddedData(false)
                                .WithDataFilesObjectMap(false)
                                .WithBuildComponentTree(false));


                        MapName = Path.GetFileNameWithoutExtension(fileName);



                        if (!arkSavegame.HibernationEntries.Any())
                        {
                            objectContainer = arkSavegame;
                            GameSeconds = arkSavegame.GameTime;
                        }
                        else
                        {
                            List<GameObject> combinedObjects = arkSavegame.Objects;

                            foreach (HibernationEntry entry in arkSavegame.HibernationEntries)
                            {
                                ObjectCollector collector = new ObjectCollector(entry, 1);
                                combinedObjects.AddRange(collector.Remap(combinedObjects.Count));
                            }

                            objectContainer = new GameObjectContainer(combinedObjects);
                            GameSeconds = arkSavegame.GameTime;
                        }

                        long saveLoadTime = DateTime.Now.Ticks;
                        TimeSpan timeTaken = TimeSpan.FromTicks(saveLoadTime - startTicks);
                        Console.WriteLine($"Game data loaded in: {timeTaken.TotalSeconds.ToString("f1")} seconds.");

                        long structureStart = DateTime.Now.Ticks;

                        //map structures we care about
                        MapStructures = objectContainer.Objects.Where(x =>
                            x.Location != null
                            & !x.HasAnyProperty("TargetingTeam")
                            && (x.ClassName.Name.StartsWith("TributeTerminal_")
                            || x.ClassName.Name.Contains("CityTerminal_")
                            || x.ClassName.Name.StartsWith("PrimalItem_PowerNodeCharge")
                            || x.ClassName.Name.StartsWith("BeaverDam_C")
                            || x.ClassName.Name.StartsWith("WyvernNest_")
                            || x.ClassName.Name.StartsWith("RockDrakeNest_C")
                            || x.ClassName.Name.StartsWith("DeinonychusNest_C")
                            || x.ClassName.Name.StartsWith("CherufeNest_C")
                            || x.ClassName.Name.StartsWith("OilVein_")
                            || x.ClassName.Name.StartsWith("WaterVein_")
                            || x.ClassName.Name.StartsWith("GasVein_")
                            || x.ClassName.Name.StartsWith("ArtifactCrate_")
                            || x.ClassName.Name.StartsWith("Structure_PlantSpeciesZ")
                            )
                        ).Select(s =>
                        {
                            ContentStructure structure = s.AsStructure();

                            structure.Latitude = mapLatLonCalcs.Item1 + structure.Y / mapLatLonCalcs.Item2;
                            structure.Longitude = mapLatLonCalcs.Item3 + structure.X / mapLatLonCalcs.Item4;

                            ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                        //check for inventory
                        ObjectReference inventoryRef = s.GetPropertyValue<ObjectReference>("MyInventoryComponent");
                            if (inventoryRef != null)
                            {
                                structure.Inventory = new ContentInventory();


                                objectContainer.TryGetValue(inventoryRef.ObjectId, out GameObject inventory);
                                if (inventory != null)
                                {
                                //inventory found, add items
                                if (inventory.HasAnyProperty("InventoryItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventory.GetTypedProperty<PropertyArray>("InventoryItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    var item = itemObject.AsItem();

                                                    if (!item.IsEngram)
                                                    {
                                                        inventoryItems.Add(item);
                                                    }
                                                }
                                            });
                                        }
                                    }
                                }
                            }
                            else
                            {
                            //no inventory component, check for any "egg" within range of nest location
                            if (s.ClassName.Name.Contains("Nest"))
                                {
                                    var eggInRange = objectContainer.Objects.FirstOrDefault(x =>
                                        x.ClassName.Name.Contains("Egg")
                                        && x.Location != null
                                        && (((x.Location.X > s.Location.X) ? x.Location.X - s.Location.X : s.Location.X - x.Location.X) < 1500)
                                        && (((x.Location.Z > s.Location.Z) ? x.Location.Z - s.Location.Z : s.Location.Z - x.Location.Z) < 1500)
                                    );

                                    if (eggInRange != null)
                                    {
                                        inventoryItems.Add(new ContentItem()
                                        {
                                            ClassName = eggInRange.ClassString,
                                            CustomName = eggInRange.GetPropertyValue<string>("DroppedByName"),
                                            Quantity = 1
                                        });
                                    }
                                }


                            }
                            if (inventoryItems.Count > 0) structure.Inventory.Items.AddRange(inventoryItems.ToArray());



                            return structure;
                        }).Where(s => s != null).ToList();

                        long structureEnd = DateTime.Now.Ticks;
                        var structureTime = TimeSpan.FromTicks(structureEnd - structureStart);
                        Console.WriteLine($"Structures loaded in: {structureTime.TotalSeconds.ToString("f1")} seconds.");

                        long wildStart = DateTime.Now.Ticks;

                        //wilds
                        WildCreatures = objectContainer.Objects.Where(x => x.IsWild())
                            .Select(x =>
                            {
                                ObjectReference statusRef = x.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent") ?? x.GetPropertyValue<ObjectReference>("MyDinoStatusComponent");
                                if (statusRef != null)
                                {
                                    objectContainer.TryGetValue(statusRef.ObjectId, out GameObject statusObject);
                                    ContentWildCreature wild = x.AsWildCreature(statusObject);
                                    wild.Latitude = mapLatLonCalcs.Item1 + wild.Y / mapLatLonCalcs.Item2;
                                    wild.Longitude = mapLatLonCalcs.Item3 + wild.X / mapLatLonCalcs.Item4;

                                    return wild;
                                }
                                else
                                {
                                    return null;
                                }

                            }).Where(x => x != null).Distinct().ToList();



                        long wildEnd = DateTime.Now.Ticks;
                        var wildTime = TimeSpan.FromTicks(wildEnd - wildStart);
                        Console.WriteLine($"Wilds loaded in: {wildTime.TotalSeconds.ToString("f1")} seconds.");


                        var allTames = objectContainer.Where(x => x.IsTamed() && x.ClassString != "MotorRaft_BP_C" && x.ClassString != "Raft_BP_C"); //exclude rafts.. no idea why these are "creatures"
                        var playerStructures = objectContainer.Where(x => x.IsStructure() && x.GetPropertyValue<int>("TargetingTeam") >= 50_000).GroupBy(x=>x.Names[0]).Select(s=>s.First()).ToList();


                        long tribeLoadStart = DateTime.Now.Ticks;
                        List<ContentTribe> tribeList = new List<ContentTribe>();

                        //ASV fake tribes
                        tribeList.Add(new ContentTribe()
                        {
                            IsSolo = true,
                            TribeId = 2_000_000_000,
                            TribeName = "[ASV Unclaimed]"
                        });

                        tribeList.Add(new ContentTribe()
                        {
                            IsSolo = true,
                            TribeId = int.MinValue,
                            TribeName = "[ASV Abandoned]"
                        });


                        //find player data in game file
                        var gamePlayers = objectContainer.Where(o => o.IsPlayer()).GroupBy(x => x.GetPropertyValue<long>("LinkedPlayerDataID")).Select(x => x.First());

                        //load .arkprofile(s) to create tribe and player containers
                        string fileFolder = Path.GetDirectoryName(fileName);
                        var profileFiles = Directory.GetFiles(fileFolder, "*.arkprofile");
                        if (profileFiles != null && profileFiles.Length > 0)
                        {
                            foreach (string profileFilename in profileFiles)
                            {
                                using (Stream streamProfile = new FileStream(profileFilename, FileMode.Open))
                                {
                                    using (ArkArchive archiveProfile = new ArkArchive(streamProfile))
                                    {
                                        ArkProfile arkProfile = new ArkProfile();
                                        arkProfile.ReadBinary(archiveProfile, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                        string profileMapName = arkProfile.Profile.Names[3].Name.ToLower();
                                        if (profileMapName == Path.GetFileNameWithoutExtension(fileName).ToLower())
                                        {

                                            ContentPlayer contentPlayer = arkProfile.AsPlayer();
                                            if(contentPlayer.Id  != 0)
                                            {

                                                contentPlayer.LastActiveDateTime = GetApproxDateTimeOf(contentPlayer.LastTimeInGame);

                                                ContentTribe contentTribe = tribeList.FirstOrDefault(x => x.TribeId == contentPlayer.TargetingTeam || x.TribeId == contentPlayer.Id);
                                                if (contentTribe == null)
                                                {
                                                    //not yet added
                                                    contentTribe = new ContentTribe();

                                                    string tribeFilename = Path.Combine(fileFolder, $"{contentPlayer.TargetingTeam}.arktribe");
                                                    if (File.Exists(tribeFilename))
                                                    {
                                                        using (Stream streamTribe = new FileStream(tribeFilename, FileMode.Open))
                                                        {
                                                            using (ArkArchive archiveTribe = new ArkArchive(streamTribe))
                                                            {
                                                                ArkTribe arkTribe = new ArkTribe();
                                                                arkTribe.ReadBinary(archiveTribe, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                                                contentTribe = arkTribe.Tribe.AsTribe();

                                                                contentTribe.TribeFileDate = File.GetLastWriteTimeUtc(tribeFilename).ToLocalTime();
                                                                contentTribe.HasGameFile = true;

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //solo tribe?
                                                        contentTribe = new ContentTribe()
                                                        {
                                                            HasGameFile = false,
                                                            IsSolo = true,
                                                            Logs = new string[0],
                                                            TribeName = $"Tribe of {contentPlayer.CharacterName}",
                                                            TribeId = contentPlayer.Id
                                                        };

                                                    }

                                                    tribeList.Add(contentTribe);

                                                }


                                                //add player to tribe
                                                contentTribe.Players.Add(contentPlayer);

                                            }
                                        }
                                    }
                                }
                            }
                        }

                        var tribesAndPlayers = objectContainer.Where(x => x.IsPlayer())
                            .Where(x=> !tribeList.Any(t => t.TribeId == x.GetPropertyValue<int>("TargetingTeam")))
                            .GroupBy(x => x.GetPropertyValue<int>("TargetingTeam")).ToList();

                        if(tribesAndPlayers!=null && tribesAndPlayers.Count > 0)
                        {
                            foreach(var tribe in tribesAndPlayers)
                            {
                                //we know there's no .arkprofile so check for .arktribe
                                string tribeFilename = Path.Combine(fileFolder, $"{tribe.Key}.arktribe");
                                if (File.Exists(tribeFilename))
                                {
                                    using (Stream streamTribe = new FileStream(tribeFilename, FileMode.Open))
                                    {
                                        using (ArkArchive archiveTribe = new ArkArchive(streamTribe))
                                        {
                                            ArkTribe arkTribe = new ArkTribe();
                                            arkTribe.ReadBinary(archiveTribe, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                            ContentTribe contentTribe = arkTribe.Tribe.AsTribe();
                                            contentTribe.TribeFileDate = File.GetLastWriteTimeUtc(tribeFilename).ToLocalTime();
                                            contentTribe.HasGameFile = true;

                                            //players
                                            foreach(GameObject arkPlayer in tribe)
                                            {
                                                //get status component
                                                var statusRef = arkPlayer.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent");
                                                if (statusRef != null)
                                                {
                                                    objectContainer.TryGetValue(statusRef.ObjectId, out GameObject playerStatus);

                                                    //convert to contentplayer
                                                    ContentPlayer contentPlayer = arkPlayer.AsPlayer(playerStatus);
                                                    contentPlayer.LastActiveDateTime = GetApproxDateTimeOf(contentPlayer.LastTimeInGame);
                                                    

                                                    contentTribe.Players.Add(contentPlayer);

                                                }

                                            }

                                            tribeList.Add(contentTribe);
                                        }
                                    }
                                }
                            }
                        }

                        //attempt to get missing tribe data from structures
                        var missingStructureTribes = playerStructures
                            .Where(x => !tribeList.Any(t => t.TribeId == x.GetPropertyValue<int>("TargetingTeam")))
                            .Select(x => new
                            {
                                TribeId = x.GetPropertyValue<int>("TargetingTeam"),
                                TribeName = x.GetPropertyValue<string>("OwnerName")
                            }).GroupBy(x=>x.TribeId)
                            .Select(x=>x.First())
                            .ToList();

                        if (missingStructureTribes != null && missingStructureTribes.Count > 0)
                        {
                            missingStructureTribes.ForEach(tribe =>
                            {


                                //we know there's no .arkprofile so check for .arktribe
                                string tribeFilename = Path.Combine(fileFolder, $"{tribe.TribeId}.arktribe");
                                if (File.Exists(tribeFilename))
                                {
                                    using (Stream streamTribe = new FileStream(tribeFilename, FileMode.Open))
                                    {
                                        using (ArkArchive archiveTribe = new ArkArchive(streamTribe))
                                        {
                                            ArkTribe arkTribe = new ArkTribe();
                                            arkTribe.ReadBinary(archiveTribe, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                            ContentTribe contentTribe = arkTribe.Tribe.AsTribe();
                                            contentTribe.TribeFileDate = File.GetLastWriteTimeUtc(tribeFilename).ToLocalTime();
                                            contentTribe.HasGameFile = true;
                                            contentTribe.Players = new ConcurrentBag<ContentPlayer>();                                            

                                            tribeList.Add(contentTribe);
                                        }
                                    }
                                }

                            });
                            
                        }



                        //attempt to get missing tribe data from tames
                        var missingTameTribes = allTames
                            .Where(x => !tribeList.Any(t => t.TribeId == x.GetPropertyValue<int>("TargetingTeam")))
                            .Select(x => new
                            {
                                TribeId = x.GetPropertyValue<int>("TargetingTeam"),
                                TribeName = x.GetPropertyValue<string>("OwnerName")
                            }).GroupBy(x => x.TribeId)
                            .Select(x => x.First())
                            .ToList();

                        if (missingTameTribes != null && missingTameTribes.Count > 0)
                        {
                            missingTameTribes.ForEach(tribe =>
                            {
                                //we know there's no .arkprofile so check for .arktribe
                                string tribeFilename = Path.Combine(fileFolder, $"{tribe.TribeId}.arktribe");
                                if (File.Exists(tribeFilename))
                                {
                                    using (Stream streamTribe = new FileStream(tribeFilename, FileMode.Open))
                                    {
                                        using (ArkArchive archiveTribe = new ArkArchive(streamTribe))
                                        {
                                            ArkTribe arkTribe = new ArkTribe();
                                            arkTribe.ReadBinary(archiveTribe, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                            ContentTribe contentTribe = arkTribe.Tribe.AsTribe();
                                            contentTribe.TribeFileDate = File.GetLastWriteTimeUtc(tribeFilename).ToLocalTime();
                                            contentTribe.HasGameFile = true;
                                            contentTribe.Players = new ConcurrentBag<ContentPlayer>();

                                            tribeList.Add(contentTribe);
                                        }
                                    }
                                }
                            });
                        }

                        //load inventories, locations etc.
                        var allPlayers = tribeList.SelectMany(t => t.Players);
                        Parallel.ForEach(allPlayers, player =>
                        //foreach(var player in allPlayers)
                        {

                            GameObject arkPlayer = gamePlayers.FirstOrDefault(x=>x.GetPropertyValue<long>("LinkedPlayerDataID") == player.Id);

                            if (arkPlayer != null)
                            {
                                ObjectReference statusRef = arkPlayer.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent");
                                objectContainer.TryGetValue(statusRef.ObjectId, out GameObject playerStatus);
                                ContentPlayer contentPlayer = arkPlayer.AsPlayer(playerStatus);

                                player.X = contentPlayer.X;
                                player.Y = contentPlayer.Y;
                                player.Z = contentPlayer.Z;
                                player.Latitude = mapLatLonCalcs.Item1 + player.Y / mapLatLonCalcs.Item2;
                                player.Longitude = mapLatLonCalcs.Item3 + player.X / mapLatLonCalcs.Item4;


                                player.LastTimeInGame = contentPlayer.LastTimeInGame;
                                player.LastActiveDateTime = GetApproxDateTimeOf(player.LastTimeInGame);
                                player.Gender = contentPlayer.Gender;
                                player.Level = contentPlayer.Level;
                                player.Stats = contentPlayer.Stats;

                                if (arkPlayer.GetPropertyValue<ObjectReference>("MyInventoryComponent")!=null)
                                {
                                    int inventoryRefId = arkPlayer.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);

                                    ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();
                                    if (inventoryComponent != null && inventoryComponent.HasAnyProperty("InventoryItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    var item = itemObject.AsItem();
                                                    if (!item.IsEngram)
                                                    {
                                                        inventoryItems.Add(item);
                                                    }
                                                }
                                            });
                                        }
                                    }

                                    if (inventoryComponent.HasAnyProperty("EquippedItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;
                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    var item = itemObject.AsItem();
                                                    if (!item.IsEngram)
                                                    {
                                                        inventoryItems.Add(item);
                                                    }
                                                }
                                            });
                                        }
                                    }

                                    player.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }
                            }

                        }
                        );



                        long tribeLoadEnd = DateTime.Now.Ticks;
                        var tribeLoadTime = TimeSpan.FromTicks(tribeLoadEnd - tribeLoadStart);
                        Console.WriteLine($"Tribe players loaded in: {tribeLoadTime.TotalSeconds.ToString("f1")} seconds.");


                        //Parallel.ForEach(allTames, x =>
                        foreach(GameObject x in allTames)
                        {
                            //find appropriate tribe to add to
                            var teamId = x.GetPropertyValue<int>("TargetingTeam");
                            var tribe = tribeList.FirstOrDefault(t => t.TribeId == teamId) ?? tribeList.FirstOrDefault(t => t.TribeId == int.MinValue); //tribe or abandoned



                 
                            ObjectReference statusRef = x.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent") ?? x.GetPropertyValue<ObjectReference>("MyDinoStatusComponent");
                            if (statusRef != null)
                            {
                                objectContainer.TryGetValue(statusRef.ObjectId, out GameObject statusObject);

                                ContentTamedCreature creature = x.AsTamedCreature(statusObject);

                                creature.Latitude = mapLatLonCalcs.Item1 + creature.Y / mapLatLonCalcs.Item2;
                                creature.Longitude = mapLatLonCalcs.Item3 + creature.X / mapLatLonCalcs.Item4;

                                creature.TamedAtDateTime = GetApproxDateTimeOf(creature.TamedTimeInGame);
                                //get inventory items
                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                                if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent") != null)
                                {
                                    int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                    if (inventoryComponent != null && inventoryComponent.HasAnyProperty("InventoryItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    var item = itemObject.AsItem();
                                                    if (!item.IsEngram)
                                                    {
                                                        inventoryItems.Add(item);
                                                    }
                                                }
                                            });
                                        }
                                    }

                                    if (inventoryComponent.HasAnyProperty("EquippedItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;
                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    var item = itemObject.AsItem();
                                                    if (!item.IsEngram)
                                                    {
                                                        inventoryItems.Add(item);
                                                    }
                                                }
                                            });
                                        }
                                    }

                                    creature.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }

                                if (tribe.Tames.Count(t => t.Id == creature.Id) == 0)
                                {
                                    tribe.Tames.Add(creature);
                                }

                            }

                        }
                        //);



                        //TODO:// add unclaimed babies, with living parent belonging to tribe



                        //structures
                        Parallel.ForEach(playerStructures, x =>
                        {

                            var teamId = x.GetPropertyValue<int>("TargetingTeam");
                            var tribe = tribeList.FirstOrDefault(t => t.TribeId == teamId) ?? tribeList.FirstOrDefault(t => t.TribeId == int.MinValue); //tribe or abandoned

                            ContentStructure structure = x.AsStructure();
                            ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                            structure.Latitude = mapLatLonCalcs.Item1 + structure.Y / mapLatLonCalcs.Item2;
                            structure.Longitude = mapLatLonCalcs.Item3 + structure.X / mapLatLonCalcs.Item4;

                            structure.CreatedDateTime = GetApproxDateTimeOf(structure.CreatedTimeInGame);

                            //inventory
                            if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent")!=null)
                            {
                                int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                if (inventoryComponent != null && inventoryComponent.HasAnyProperty("InventoryItems"))
                                {
                                    PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                    if (inventoryItemsArray != null)
                                    {
                                        ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                        Parallel.ForEach(objectReferences, objectReference =>
                                        {
                                            objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                            if (itemObject != null)
                                            {
                                                var item = itemObject.AsItem();
                                                if (!item.IsEngram)
                                                {
                                                    inventoryItems.Add(item);
                                                }
                                            }
                                        });
                                    }

                                }

                                if (inventoryComponent.HasAnyProperty("EquippedItems"))
                                {
                                    PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                    if (inventoryItemsArray != null)
                                    {
                                        ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;
                                        Parallel.ForEach(objectReferences, objectReference =>
                                        {
                                            objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                            if (itemObject != null)
                                            {
                                                var item = itemObject.AsItem();
                                                if (!item.IsEngram)
                                                {
                                                    inventoryItems.Add(item);
                                                }
                                            }
                                        });
                                    }
                                }

                                structure.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                            }
                            if (!tribe.Structures.Contains(structure)) tribe.Structures.Add(structure);
                        });
                        if (tribeList.Count > 0) Tribes.AddRange(tribeList.ToList());



                        //dropped 
                        DroppedItems = new List<ContentDroppedItem>();

                        //..items
                        DroppedItems.AddRange(objectContainer.Where(o => o.IsDroppedItem())
                            .Select(x =>
                            {
                                ContentDroppedItem droppedItem = x.AsDroppedItem();
                                ObjectReference itemRef = x.GetPropertyValue<ObjectReference>("MyItem");
                                objectContainer.TryGetValue(itemRef.ObjectId, out GameObject itemObject);

                                droppedItem.ClassName = itemObject.ClassString;
                                droppedItem.IsDeathCache = itemObject.IsDeathItemCache();


                                droppedItem.Latitude = mapLatLonCalcs.Item1 + droppedItem.Y / mapLatLonCalcs.Item2;
                                droppedItem.Longitude = mapLatLonCalcs.Item3 + droppedItem.X / mapLatLonCalcs.Item4;

                                return droppedItem;
                            }).ToList()
                        );

                        //.. corpses
                        DroppedItems.AddRange(objectContainer.Where(x => x.IsPlayer() && x.HasAnyProperty("MyDeathHarvestingComponent"))
                            .Select(x =>
                            {
                                ContentDroppedItem droppedItem = x.AsDroppedItem();

                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                                droppedItem.ClassName = x.ClassString;
                                droppedItem.IsDeathCache = true;
                                droppedItem.Latitude = mapLatLonCalcs.Item1 + droppedItem.Y / mapLatLonCalcs.Item2;
                                droppedItem.Longitude = mapLatLonCalcs.Item3 + droppedItem.X / mapLatLonCalcs.Item4;

                                if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent")!=null)
                                {
                                    int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                    if (inventoryComponent != null && inventoryComponent.HasAnyProperty("InventoryItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    if (!itemObject.HasAnyProperty("bIsInitialItem"))
                                                    {

                                                        var item = itemObject.AsItem();
                                                        if (!item.IsEngram)
                                                        {
                                                            inventoryItems.Add(item);
                                                        }

                                                    }
                                                }
                                            });
                                        }

                                    }

                                    if (inventoryComponent.HasAnyProperty("EquippedItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;
                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    if (!itemObject.HasAnyProperty("bIsInitialItem"))
                                                    {

                                                        var item = itemObject.AsItem();
                                                        if (!item.IsEngram)
                                                        {
                                                            inventoryItems.Add(item);
                                                        }

                                                    }
                                                }
                                            });
                                        }
                                    }

                                    droppedItem.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }



                                return droppedItem;
                            }).ToList()
                        );

                        //.. bags
                        DroppedItems.AddRange(objectContainer.Where(x => x.IsDeathItemCache())
                            .Select(x =>
                            {
                                ContentDroppedItem droppedItem = x.AsDroppedItem();
                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();
                                droppedItem.ClassName = x.ClassString;
                                droppedItem.IsDeathCache = true;


                                droppedItem.Latitude = mapLatLonCalcs.Item1 + droppedItem.Y / mapLatLonCalcs.Item2;
                                droppedItem.Longitude = mapLatLonCalcs.Item3 + droppedItem.X / mapLatLonCalcs.Item4;

                                if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent")!=null)
                                {
                                    int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                    if (inventoryComponent != null && inventoryComponent.HasAnyProperty("InventoryItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    if (!itemObject.HasAnyProperty("bIsInitialItem"))
                                                    {

                                                        var item = itemObject.AsItem();
                                                        if (!item.IsEngram)
                                                        {
                                                            inventoryItems.Add(item);
                                                        }

                                                    }
                                                }
                                            });
                                        }

                                    }

                                    if (inventoryComponent.HasAnyProperty("EquippedItems"))
                                    {
                                        PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                        if (inventoryItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;
                                            Parallel.ForEach(objectReferences, objectReference =>
                                            {
                                                objectContainer.TryGetValue(objectReference.ObjectId, out GameObject itemObject);
                                                if (itemObject != null)
                                                {
                                                    if (!itemObject.HasAnyProperty("bIsInitialItem"))
                                                    {

                                                        var item = itemObject.AsItem();
                                                        if (!item.IsEngram)
                                                        {
                                                            inventoryItems.Add(item);
                                                        }

                                                    }
                                                }
                                            });
                                        }
                                    }

                                    droppedItem.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }


                                return droppedItem;
                            }).ToList()
                        );




                    }
                }

                long endTicks = DateTime.Now.Ticks;
                var duration = TimeSpan.FromTicks(endTicks - startTicks);

                Console.WriteLine($"Loaded in: {duration.TotalSeconds.ToString("f1")} seconds.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load: {ex.Message}");
                throw;
            }

            




        }

        public DateTime? GetApproxDateTimeOf(double? objectTime)
        {
            return objectTime.HasValue 
                && GameSeconds > 0 ? GameSaveTime.AddSeconds(objectTime.Value - GameSeconds) : (DateTime?)null;
        }


    }
}
