﻿using SavegameToolkit;
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
using Newtonsoft.Json;
using NLog;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentContainer
    {

        ILogger logWriter = LogManager.GetCurrentClassLogger();

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
        [DataMember] public DateTime GameSaveTime { get; set; } = new DateTime();
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
            logWriter.Trace("BEGIN LoadSaveGame()");


            LoadDefaults();

            long startTicks = DateTime.Now.Ticks;
            try
            {
                List<ContentTribe> tribeContentList = new List<ContentTribe>();

                GameObjectContainer objectContainer = null;
                GameSaveTime = new FileInfo(fileName).LastWriteTimeUtc.ToLocalTime();




                logWriter.Debug($"Reading game save data: {fileName}");

                using (Stream stream = new MemoryStream(File.ReadAllBytes(fileName)))
                {
                    using (ArkArchive archive = new ArkArchive(stream))
                    {

                        ArkSavegame arkSavegame = new ArkSavegame();

                        arkSavegame.ReadBinary(archive, ReadingOptions.Create()
                                .WithThreadCount(int.MaxValue)
                                .WithStoredCreatures(true)
                                .WithDataFiles(true)
                                .WithEmbeddedData(false)
                                .WithDataFilesObjectMap(false)
                                .WithBuildComponentTree(true));



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


                        //get map name from .ark file data
                        logWriter.Debug($"Reading map name from: {fileName}");
                        MapName = arkSavegame.DataFiles[0];
                        logWriter.Debug($"Map name returned: {MapName}");
                        latlonCalcs.TryGetValue(MapName.ToLower(), out mapLatLonCalcs);


                        long saveLoadTime = DateTime.Now.Ticks;
                        TimeSpan timeTaken = TimeSpan.FromTicks(saveLoadTime - startTicks);
                        logWriter.Info($"Game data loaded in: {timeTaken.ToString(@"mm\:ss")}.");




                        var filePath = Path.GetDirectoryName(fileName);
                        long profileStart = DateTime.Now.Ticks;
                        logWriter.Debug($"Reading .arkprofile(s)");
                        ConcurrentBag<ContentPlayer> fileProfiles = new ConcurrentBag<ContentPlayer>();
                        var profileFilenames = Directory.GetFiles(filePath, "*.arkprofile");
                        profileFilenames.AsParallel().ForAll(x =>
                        {
                            try
                            {
                                logWriter.Debug($"Reading profile data: {x}");
                                using (Stream streamProfile = new FileStream(x, FileMode.Open))
                                {


                                    using (ArkArchive archiveProfile = new ArkArchive(streamProfile))
                                    {
                                        ArkProfile arkProfile = new ArkProfile();
                                        arkProfile.ReadBinary(archiveProfile, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                        string profileMapName = arkProfile.Profile.Names[3].Name.ToLower();
                                        logWriter.Debug($"Profile map identified as: {profileMapName}");
                                        if (profileMapName == MapName.ToLower())
                                        {
                                            logWriter.Debug($"Converting to ContentPlayer: {x}");
                                            ContentPlayer contentPlayer = arkProfile.AsPlayer();
                                            if (contentPlayer.Id != 0)
                                            {

                                                contentPlayer.LastActiveDateTime = GetApproxDateTimeOf(contentPlayer.LastTimeInGame);
                                                fileProfiles.Add(contentPlayer);
                                            }
                                            
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logWriter.Debug($"Failed to read profile data: {x}");
                            }
                        });
                        long profileEnd = DateTime.Now.Ticks;





                        ConcurrentBag<ContentTribe> fileTribes = new ConcurrentBag<ContentTribe>();
                        long tribeStart = DateTime.Now.Ticks;
                        logWriter.Debug($"Reading .arktribe(s)");

                        var tribeFilenames = Directory.GetFiles(filePath, "*.arktribe");
                        tribeFilenames.AsParallel().ForAll(x =>
                        {
                            try
                            {
                                logWriter.Debug($"Reading tribe data: {x}");
                                using (Stream streamTribe = new FileStream(x, FileMode.Open))
                                {


                                    using (ArkArchive archiveTribe = new ArkArchive(streamTribe))
                                    {
                                        ArkTribe arkTribe = new ArkTribe();
                                        arkTribe.ReadBinary(archiveTribe, ReadingOptions.Create().WithBuildComponentTree(false).WithDataFilesObjectMap(false).WithGameObjects(true).WithGameObjectProperties(true));

                                        logWriter.Debug($"Converting to ContentTribe for: {x}");
                                        var contentTribe = arkTribe.Tribe.AsTribe();
                                        if (contentTribe != null && contentTribe.TribeName != null)
                                        {
                                            contentTribe.TribeFileDate = File.GetLastWriteTimeUtc(x).ToLocalTime();
                                            contentTribe.HasGameFile = true;
                                            fileTribes.Add(contentTribe);

                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logWriter.Debug($"Failed to read tribe data: {x}");
                            }

                        });

                        //add fake tribes for abandoned and unclaimed
                        fileTribes.Add(new ContentTribe()
                        {
                            IsSolo = true,
                            TribeId = 2_000_000_000,
                            TribeName = "[ASV Unclaimed]"
                        });

                        fileTribes.Add(new ContentTribe()
                        {
                            IsSolo = true,
                            TribeId = int.MinValue,
                            TribeName = "[ASV Abandoned]"
                        });


                        long tribeEnd = DateTime.Now.Ticks;


                        logWriter.Debug($"Allocating players to tribes");

                        //allocate players to tribes
                        fileProfiles.AsParallel().ForAll(p =>
                        {
                            var playerTribe = fileTribes.FirstOrDefault(t => t.TribeId == (long)p.TargetingTeam);
                            if (playerTribe == null)
                            {
                                playerTribe = new ContentTribe()
                                {
                                    IsSolo = true,
                                    HasGameFile = false,
                                    TribeId = p.Id,
                                    TribeName = $"Tribe of {p.CharacterName}"
                                };

                                fileTribes.Add(playerTribe);
                            }


                            playerTribe.Players.Add(p);
                        });
                        long allocationEnd = DateTime.Now.Ticks;


                        long structureStart = DateTime.Now.Ticks;


                        logWriter.Debug($"Identifying map structures");
                        //map structures we care about
                        MapStructures = objectContainer.Objects.Where(x =>
                            x.Location != null
                            && x.GetPropertyValue<int?>("TargetingTeam") == null
                            && (x.ClassString.StartsWith("TributeTerminal_")
                            || x.ClassString.Contains("CityTerminal_")
                            || x.ClassString.StartsWith("PrimalItem_PowerNodeCharge")
                            || x.ClassString.StartsWith("BeaverDam_C")
                            || x.ClassString.StartsWith("WyvernNest_")
                            || x.ClassString.StartsWith("RockDrakeNest_C")
                            || x.ClassString.StartsWith("DeinonychusNest_C")
                            || x.ClassString.StartsWith("CherufeNest_C")
                            || x.ClassString.StartsWith("OilVein_")
                            || x.ClassString.StartsWith("WaterVein_")
                            || x.ClassString.StartsWith("GasVein_")
                            || x.ClassString.StartsWith("ArtifactCrate_")
                            || x.ClassString.StartsWith("Structure_PlantSpeciesZ")


                            )
                        ).Select(s =>
                        {
                            ContentStructure structure = s.AsStructure();

                            structure.Latitude = mapLatLonCalcs.Item1 + structure.Y / mapLatLonCalcs.Item2;
                            structure.Longitude = mapLatLonCalcs.Item3 + structure.X / mapLatLonCalcs.Item4;

                            ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();


                            //check for inventory
                            logWriter.Debug($"Determining if structure has inventory: {s.ClassString}");
                            ObjectReference inventoryRef = s.GetPropertyValue<ObjectReference>("MyInventoryComponent");
                            if (inventoryRef != null)
                            {
                                logWriter.Debug($"Populating structure inventory: {s.ClassString}");
                                structure.Inventory = new ContentInventory();


                                objectContainer.TryGetValue(inventoryRef.ObjectId, out GameObject inventory);
                                if (inventory != null)
                                {
                                    //inventory found, add items
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

                                                if (!item.IsEngram & !item.IsBlueprint)
                                                {
                                                   
                                                    inventoryItems.Add(item);
                                                }
                                            }
                                        });
                                    }
                                    
                                }
                            }
                            else
                            {
                                //no inventory component, check for any "egg" within range of nest location
                                if (s.ClassName.Name.Contains("Nest"))
                                {
                                    logWriter.Debug($"Finding nearby eggs for: {s.ClassString}");
                                    var eggInRange = objectContainer.Objects.FirstOrDefault(x =>
                                        x.ClassName.Name.Contains("Egg")
                                        && x.Location != null
                                        && (((x.Location.X > s.Location.X) ? x.Location.X - s.Location.X : s.Location.X - x.Location.X) < 1500)
                                        && (((x.Location.Z > s.Location.Z) ? x.Location.Z - s.Location.Z : s.Location.Z - x.Location.Z) < 1500)
                                    );

                                    if (eggInRange != null)
                                    {
                                        logWriter.Debug($"Egg found: {eggInRange.ClassString} for {s.ClassString}");
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
                        logWriter.Info($"Structures loaded in: {structureTime.ToString(@"mm\:ss")}.");

                        long wildStart = DateTime.Now.Ticks;


                        logWriter.Debug($"Identifying wild creatures");
                        //wilds
                        WildCreatures = objectContainer.Objects.AsParallel().Where(x => x.IsWild())
                            .Select(x =>
                            {
                                logWriter.Debug($"Determining character status for: {x.ClassString}");
                                ObjectReference statusRef = x.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent") ?? x.GetPropertyValue<ObjectReference>("MyDinoStatusComponent");
                                if (statusRef != null)
                                {
                                    logWriter.Debug($"Character status found for: {x.ClassString}");
                                    objectContainer.TryGetValue(statusRef.ObjectId, out GameObject statusObject);
                                    ContentWildCreature wild = x.AsWildCreature(statusObject);

                                    //stryder rigs
                                    if (x.ClassString == "TekStrider_Character_BP_C")
                                    {
                                        var inventComp = x.InventoryComponent();
                                        PropertyArray equippedItemsArray = inventComp.GetTypedProperty<PropertyArray>("EquippedItems");

                                        if (equippedItemsArray != null)
                                        {
                                            ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)equippedItemsArray.Value;
                                            if (objectReferences != null && objectReferences.Count == 2)
                                            {
                                                objectContainer.TryGetValue(objectReferences[0].ObjectId, out GameObject rig1Object);
                                                var itemRig1 = rig1Object.AsItem();
                                                wild.Rig1 = itemRig1.ClassName;

                                                objectContainer.TryGetValue(objectReferences[1].ObjectId, out GameObject rig2Object);
                                                var itemRig2 = rig2Object.AsItem();
                                                wild.Rig2 = itemRig2.ClassName;
                                            }
                                        }

                                    }



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
                        logWriter.Info($"Wilds loaded in: {wildTime.ToString(@"mm\:ss")}.");

                        var parallelContainer = objectContainer.AsParallel();

                        logWriter.Debug($"Identifying tamed creatures");
                        var allTames = parallelContainer
                                        .Where(x => x.IsTamed() && x.ClassString != "MotorRaft_BP_C" && x.ClassString != "Raft_BP_C" && x.ClassString != "TekHoverSkiff_Character_BP_C") //exclude rafts.. no idea why these are "creatures"
                                        .GroupBy(x => new { TribeId = (long)x.GetPropertyValue<int>("TargetingTeam"), TribeName = x.GetPropertyValue<string>("TamerString") });


                        logWriter.Debug($"Identifying player structures");
                        var playerStructures = parallelContainer.Where(x => x.IsStructure() && x.GetPropertyValue<int>("TargetingTeam") >= 50_000).GroupBy(x => x.Names[0]).Select(s => s.First()).ToList();

                        var tribeStructures = playerStructures
                                                    .GroupBy(x => new { TribeId = x.GetPropertyValue<int>("TargetingTeam"), TribeName = x.GetPropertyValue<string>("OwnerName") ?? x.GetPropertyValue<string>("TamerString") })
                                                    .Select(x => new { TribeId = x.Key.TribeId, TribeName = x.Key.TribeName, Structures = x.ToList() })
                                                    .ToList();



                        //player and tribe data
                        long tribeLoadStart = DateTime.Now.Ticks;
                        logWriter.Debug($"Identifying in-game player data");
                        var gamePlayers = parallelContainer.Where(o => o.IsPlayer() & !o.HasAnyProperty("MyDeathHarvestingComponent")).GroupBy(x => x.GetPropertyValue<long>("LinkedPlayerDataID")).Select(x => x.First()).ToList();
                        var tribesAndPlayers = gamePlayers.GroupBy(x => x.GetPropertyValue<int>("TargetingTeam")).ToList();

                        logWriter.Debug($"Identifying in-game players with no .arkprofile");

                        var abandonedGamePlayers = tribesAndPlayers.Where(x => !fileTribes.Any(t => t.TribeId == (long)x.Key) & !fileProfiles.Any(p => p.Id == (long)x.Key)).ToList();
                        if (abandonedGamePlayers != null && abandonedGamePlayers.Count > 0)
                        {
                            abandonedGamePlayers.AsParallel().ForAll(abandonedTribe =>
                            //foreach(var abandonedTribe in abandonedGamePlayers)
                            {
                                var abandonedPlayers = abandonedTribe.ToList();
                                var newTribe = new ContentTribe()
                                {
                                    IsSolo = abandonedPlayers.Count == 1,
                                    HasGameFile = false,
                                    TribeId = abandonedTribe.Key,
                                    TribeName = abandonedTribe.First().GetPropertyValue<string>("TribeName") ?? "Tribe of " + abandonedTribe.First().GetPropertyValue<string>("PlayerName")
                                };

                                abandonedPlayers.Select(x => x.AsPlayer(x.CharacterStatusComponent())).ToList().ForEach(x =>
                                {
                                    if (x.Id != 0)
                                    {
                                        newTribe.Players.Add(x);
                                    }
                                });

                                fileTribes.Add(newTribe);
                            }
                            );
                        }



                        logWriter.Debug($"Identifying in-game missing tribes from player structures");

                        //attempt to get missing tribe data from structures
                        var missingStructureTribes = tribeStructures.AsParallel()
                            .Where(x => !fileTribes.Any(t => t.TribeId == x.TribeId))
                            .GroupBy(x => x.TribeId)
                            .Select(x => x.First())
                            .ToList();

                        if (missingStructureTribes != null && missingStructureTribes.Count > 0)
                        {
                            logWriter.Debug($"Identified player structure tribes: {missingStructureTribes.Count}");
                            missingStructureTribes.ForEach(tribe =>
                            {


                                fileTribes.Add(new ContentTribe()
                                {
                                    TribeId = tribe.TribeId,
                                    TribeName = tribe.TribeName,
                                    HasGameFile = false
                                });
                            });

                        }


                        logWriter.Debug($"Identifying in-game tribes from tames");

                        //attempt to get missing tribe data from tames
                        var missingTameTribes = allTames
                            .Where(x => !fileTribes.Any(t => t.TribeId == x.Key.TribeId))
                            .ToList();

                        if (missingTameTribes != null && missingTameTribes.Count > 0)
                        {
                            logWriter.Debug($"Identified tame tribes: {missingStructureTribes.Count}");

                            missingTameTribes.ForEach(tribe =>
                            {

                                //we know there's no .arktribe
                                fileTribes.Add(new ContentTribe()
                                {
                                    TribeId = tribe.Key.TribeId,
                                    TribeName = tribe.Key.TribeName,
                                    HasGameFile = false
                                });

                            });
                        }













                        logWriter.Debug($"Populating player data");
                        //load inventories, locations etc.

                        //fileTribes.Where(x=>x.Players.Count >0).AsParallel().ForAll(fileTribe =>
                        foreach(var fileTribe in fileTribes.Where(x=>x.Players.Count > 0))
                        {
                            var tribePlayers = fileTribe.Players;

                            tribePlayers.AsParallel().ForAll(player =>
                            //foreach (var player in tribePlayers)
                            {

                                GameObject arkPlayer = gamePlayers.FirstOrDefault(x => x.GetPropertyValue<long>("LinkedPlayerDataID") == player.Id);

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

                                    logWriter.Debug($"Retrieving player inventory: {player.Id} - {player.CharacterName}");
                                    if (arkPlayer.GetPropertyValue<ObjectReference>("MyInventoryComponent") != null)
                                    {
                                        int inventoryRefId = arkPlayer.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                        objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);

                                        ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();
                                        if (inventoryComponent != null)
                                        {
                                            PropertyArray inventoryItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("InventoryItems");
                                            if (inventoryItemsArray != null)
                                            {
                                                ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)inventoryItemsArray.Value;

                                                //Parallel.ForEach(objectReferences, objectReference =>
                                                foreach (var objectReference in objectReferences)
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
                                                }
                                                //);
                                            }


                                            PropertyArray equippedItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");

                                            if (equippedItemsArray != null)
                                            {
                                                ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)equippedItemsArray.Value;
                                                //Parallel.ForEach(objectReferences, objectReference =>
                                                foreach (var objectReference in objectReferences)
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
                                                }
                                                //);
                                            }

                                        }

                                        

                                        player.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                    }
                                }

                            }
                            );
                        }
                        //);


                        long tribeLoadEnd = DateTime.Now.Ticks;
                        var tribeLoadTime = TimeSpan.FromTicks(tribeLoadEnd - tribeLoadStart);
                        logWriter.Info($"Tribe players loaded in: {tribeLoadTime.ToString(@"mm\:ss")}.");















                        logWriter.Debug($"Populating tamed creature inventories");

                        Parallel.ForEach(allTames.SelectMany(x => x.ToList()), x =>
                        //foreach (GameObject x in allTames)
                        {
                            //find appropriate tribe to add to
                            var teamId = x.GetPropertyValue<int>("TargetingTeam");
                            var tribe = fileTribes.FirstOrDefault(t => t.TribeId == teamId) ?? fileTribes.FirstOrDefault(t => t.TribeId == int.MinValue); //tribe or abandoned

                            logWriter.Debug($"Determining character status for: {x.ClassString}");
                            ObjectReference statusRef = x.GetPropertyValue<ObjectReference>("MyCharacterStatusComponent") ?? x.GetPropertyValue<ObjectReference>("MyDinoStatusComponent");
                            if (statusRef != null)
                            {
                                objectContainer.TryGetValue(statusRef.ObjectId, out GameObject statusObject);

                                logWriter.Debug($"Converting to ContentTamedCreature: {x.ClassString}");
                                ContentTamedCreature creature = x.AsTamedCreature(statusObject);

                                creature.Latitude = mapLatLonCalcs.Item1 + creature.Y / mapLatLonCalcs.Item2;
                                creature.Longitude = mapLatLonCalcs.Item3 + creature.X / mapLatLonCalcs.Item4;

                                creature.TamedAtDateTime = GetApproxDateTimeOf(creature.TamedTimeInGame);
                                //get inventory items
                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                                logWriter.Debug($"Determining inventory status for: {creature.Id} - {creature.Name}");
                                if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent") != null)
                                {
                                    logWriter.Debug($"Retrieving inventory for: {creature.Id} - {creature.Name}");

                                    int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                    
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

                                        if (x.ClassString == "TekStrider_Character_BP_C")
                                        {
                                            
                                            PropertyArray equippedItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");

                                            if (equippedItemsArray != null)
                                            {
                                                ArkArrayObjectReference equippedReferences = (ArkArrayObjectReference)equippedItemsArray.Value;
                                                if (equippedReferences != null && equippedReferences.Count == 2)
                                                {
                                                    objectContainer.TryGetValue(equippedReferences[0].ObjectId, out GameObject rig1Object);
                                                    var itemRig1 = rig1Object.AsItem();
                                                    creature.Rig1 = itemRig1.ClassName;

                                                    objectContainer.TryGetValue(equippedReferences[1].ObjectId, out GameObject rig2Object);
                                                    var itemRig2 = rig2Object.AsItem();
                                                    creature.Rig2 = itemRig2.ClassName;
                                                }
                                            }

                                        }
                                    }


                                    

                                    creature.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }

                                tribe.Tames.Add(creature);

                            }

                        }
                        );



                        //TODO:// add unclaimed babies, with living parent belonging to tribe



                        //structures
                        logWriter.Debug($"Populating player structure inventories");
                        Parallel.ForEach(tribeStructures.SelectMany(x => x.Structures), x =>
                        {

                            var teamId = x.GetPropertyValue<int>("TargetingTeam");
                            var tribe = fileTribes.FirstOrDefault(t => t.TribeId == teamId) ?? fileTribes.FirstOrDefault(t => t.TribeId == int.MinValue); //tribe or abandoned

                            logWriter.Debug($"Converting to ContentStructure: {x.ClassString}");

                            ContentStructure structure = x.AsStructure();
                            ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                            structure.Latitude = mapLatLonCalcs.Item1 + structure.Y / mapLatLonCalcs.Item2;
                            structure.Longitude = mapLatLonCalcs.Item3 + structure.X / mapLatLonCalcs.Item4;

                            structure.CreatedDateTime = GetApproxDateTimeOf(structure.CreatedTimeInGame);
                            structure.LastAllyInRangeTime = GetApproxDateTimeOf(structure.LastAllyInRangeTimeInGame);

                            //inventory
                            logWriter.Debug($"Determining inventory status for: {structure.ClassName}");
                            if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent") != null)
                            {
                                logWriter.Debug($"Retrieving inventory for: {structure.ClassName}");
                                int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                if (inventoryComponent != null)
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


                                    PropertyArray equippedItemsArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                    if (equippedItemsArray != null)
                                    {
                                        ArkArrayObjectReference objectReferences = (ArkArrayObjectReference)equippedItemsArray.Value;
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
                        if (fileTribes.Count > 0) Tribes.AddRange(fileTribes.ToList());



                        //dropped 
                        logWriter.Debug($"Identifying dropped items");
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
                        logWriter.Debug($"Identifying any corpse");
                        DroppedItems.AddRange(objectContainer.Where(x => x.IsPlayer() && x.HasAnyProperty("MyDeathHarvestingComponent"))
                            .Select(x =>
                            {
                                ContentDroppedItem droppedItem = x.AsDroppedItem();

                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();

                                droppedItem.ClassName = x.ClassString;
                                droppedItem.IsDeathCache = true;
                                droppedItem.DroppedByTribeId = x.GetPropertyValue<int>("TargetingTeam", 0, 0);
                                droppedItem.DroppedByPlayerId = x.GetPropertyValue<long>("LinkedPlayerDataID", 0, 0);
                                droppedItem.DroppedByName = x.GetPropertyValue<string>("PlayerName");
                                droppedItem.Latitude = mapLatLonCalcs.Item1 + droppedItem.Y / mapLatLonCalcs.Item2;
                                droppedItem.Longitude = mapLatLonCalcs.Item3 + droppedItem.X / mapLatLonCalcs.Item4;

                                if (x.GetPropertyValue<ObjectReference>("MyInventoryComponent") != null)
                                {
                                    int inventoryRefId = x.GetPropertyValue<ObjectReference>("MyInventoryComponent").ObjectId;
                                    objectContainer.TryGetValue(inventoryRefId, out GameObject inventoryComponent);
                                    if (inventoryComponent != null)
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
                                                if (itemObject.GetPropertyValue<bool>("bIsInitialItem", 0, false))
                                                    {

                                                        var item = itemObject.AsItem();
                                                        if (!item.IsEngram)
                                                        {
                                                            
                                                            inventoryItems.Add(item);
                                                        }

                                                    }
                                                }
                                            });


                                            PropertyArray equippedItemArray = inventoryComponent.GetTypedProperty<PropertyArray>("EquippedItems");
                                            if (equippedItemArray != null)
                                            {
                                                ArkArrayObjectReference equippedReferences = (ArkArrayObjectReference)equippedItemArray.Value;
                                                Parallel.ForEach(equippedReferences, objectReference =>
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

                                    }

                                    

                                    droppedItem.Inventory = new ContentInventory() { Items = inventoryItems.ToList() };
                                }



                                return droppedItem;
                            }).ToList()
                        );

                        //.. bags
                        logWriter.Debug($"Identifying drop bags");

                        DroppedItems.AddRange(objectContainer.Where(x => x.IsDeathItemCache())
                            .Select(x =>
                            {
                                ContentDroppedItem droppedItem = x.AsDroppedItem();
                                ConcurrentBag<ContentItem> inventoryItems = new ConcurrentBag<ContentItem>();
                                droppedItem.ClassName = x.ClassString;
                                droppedItem.IsDeathCache = true;


                                droppedItem.Latitude = mapLatLonCalcs.Item1 + droppedItem.Y / mapLatLonCalcs.Item2;
                                droppedItem.Longitude = mapLatLonCalcs.Item3 + droppedItem.X / mapLatLonCalcs.Item4;

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

                logWriter.Info($"Loaded in: {duration.ToString(@"mm\:ss")}.");
            }
            catch (Exception ex)
            {
                logWriter.Error(ex, "LoadSaveGame failed");
                throw;
            }


            logWriter.Trace("END LoadSaveGame()");
        }

        public DateTime? GetApproxDateTimeOf(double? objectTime)
        {
            return objectTime.HasValue
                && GameSeconds > 0 ? GameSaveTime.AddSeconds(objectTime.Value - GameSeconds) : (DateTime?)null;
        }


    }
}
