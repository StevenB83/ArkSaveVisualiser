using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Diagnostics;
using SavegameToolkit;
using System.Drawing;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentPack
    {

        
        [DataMember] public string MapFilename { get; set; } = "TheIsland.ark";
        [DataMember] public DateTime ContentDate { get; set; } = DateTime.Now;
        [DataMember] public long ExportedForTribe { get; set; } = 0;
        [DataMember] public long ExportedForPlayer { get; set; } = 0;
        [DataMember] public DateTime ExportedTimestamp { get; set; } = DateTime.Now;
        [DataMember] public List<ContentStructure> TerminalMarkers { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> GlitchMarkers { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> ChargeNodes { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> BeaverDams { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> WyvernNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> DrakeNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> MagmaNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> DeinoNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> OilVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> WaterVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> GasVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> Artifacts { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> PlantZ { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentDroppedItem> DroppedItems { get; set; } = new List<ContentDroppedItem>();
        [DataMember] public List<ContentWildCreature> WildCreatures { get; set; } = new List<ContentWildCreature>();
        [DataMember] public List<ContentTribe> Tribes { get; set; } = new List<ContentTribe>();
        [DataMember] public ContentLocalProfile LocalProfile { get; set; } = new ContentLocalProfile();
        [DataMember] public List<ContentLeaderboard> Leaderboards { get; set; } = new List<ContentLeaderboard>();

        public List<ContentStructure> OrphanedStructures { get; set; } = new List<ContentStructure>();
        public List<ContentTamedCreature> OrphanedTames { get; set; } = new List<ContentTamedCreature>();

        bool IncludeGameStructures { get; set; } = true;
        bool IncludeGameStructureContent { get; set; } = true;
        bool IncludeTribesPlayers { get; set; } = true;
        bool IncludeTamed { get; set; } = true;
        bool IncludeWild { get; set; } = true;
        bool IncludePlayerStructures { get; set; } = true;
        bool IncludeDroppedItems { get; set; } = true;
        decimal FilterLatitude { get; set; } = 50;
        decimal FilterLongitude { get; set; } = 50;
        decimal FilterRadius { get; set; } = 100;


        ConcurrentBag<ContentInventory> inventoryBag = new ConcurrentBag<ContentInventory>();

        public ContentPack()
        {
            //initialize defaults
            MapFilename = "TheIsland.ark";
            ExportedForTribe = 0;
            ExportedForPlayer = 0;
            FilterLatitude = 50;
            FilterLongitude = 50;
            FilterRadius = 100;

            TerminalMarkers = new List<ContentStructure>();
            GlitchMarkers = new List<ContentStructure>();
            ChargeNodes = new List<ContentStructure>();
            BeaverDams = new List<ContentStructure>();
            WyvernNests = new List<ContentStructure>();
            DrakeNests = new List<ContentStructure>();
            MagmaNests = new List<ContentStructure>();
            DeinoNests = new List<ContentStructure>();
            OilVeins = new List<ContentStructure>();
            WaterVeins = new List<ContentStructure>();
            GasVeins = new List<ContentStructure>();
            Artifacts = new List<ContentStructure>();
            PlantZ = new List<ContentStructure>();
            WildCreatures = new List<ContentWildCreature>();
            Tribes = new List<ContentTribe>();
            DroppedItems = new List<ContentDroppedItem>();
        }

        public ContentPack(ContentPack sourcePack, long selectedTribeId, long selectedPlayerId, decimal lat, decimal lon, decimal rad, bool includeGameStructures, bool includeGameStructureContent, bool includeTribesPlayers, bool includeTamed, bool includeWild, bool includePlayerStructures, bool includeDropped) : this()
        {
            
            ExportedForTribe = selectedTribeId;
            ExportedForPlayer = selectedPlayerId;

            FilterLatitude = lat;
            FilterLongitude = lon;
            FilterRadius = rad;

            IncludeGameStructures = includeGameStructures;
            IncludeGameStructureContent = includeGameStructureContent;
            IncludeTribesPlayers = includeTribesPlayers;
            IncludeTamed = includeTamed;
            IncludeWild = includeWild;
            IncludePlayerStructures = includePlayerStructures;

            LoadPackData(sourcePack);

        }

        
        public ContentPack(ContentContainer container, long selectedTribeId, long selectedPlayerId, decimal lat, decimal lon, decimal rad, bool includeGameStructures, bool includeGameStructureContent, bool includeTribesPlayers, bool includeTamed, bool includeWild, bool includePlayerStructures, bool includeDropped) : this()
        {
            ExportedForTribe = selectedTribeId;
            ExportedForPlayer = selectedPlayerId;

            FilterLatitude = lat;
            FilterLongitude = lon;
            FilterRadius = rad;

            IncludeGameStructures = includeGameStructures;
            IncludeGameStructureContent = includeGameStructureContent;
            IncludeTribesPlayers = includeTribesPlayers;
            IncludeTamed = includeTamed;
            IncludeWild = includeWild;
            IncludePlayerStructures = includePlayerStructures;
            LocalProfile = container.LocalProfile;
            Leaderboards = container.Leaderboards;

            LoadGameData(container);

        }

        public ContentPack(byte[] dataPack): this()
        {
            string jsonContent = Unzip(dataPack);
            LoadJson(jsonContent);
        }

        public void LoadJson(string jsonPack)
        {
            //load content from json
            try
            {
                ContentPack loaded = new ContentPack();
                loaded = JsonConvert.DeserializeObject<ContentPack>(jsonPack);

                MapFilename = loaded.MapFilename;
                ExportedTimestamp = loaded.ExportedTimestamp;
                ExportedForTribe = loaded.ExportedForTribe;
                ExportedForPlayer = loaded.ExportedForPlayer;
                TerminalMarkers = loaded.TerminalMarkers;
                GlitchMarkers = loaded.GlitchMarkers;
                ChargeNodes = loaded.ChargeNodes;
                BeaverDams = loaded.BeaverDams;
                WyvernNests = loaded.WyvernNests;
                DrakeNests = loaded.DrakeNests;
                MagmaNests = loaded.MagmaNests;
                OilVeins = loaded.OilVeins;
                WaterVeins = loaded.WaterVeins;
                GasVeins = loaded.GasVeins;
                Artifacts = loaded.Artifacts;
                PlantZ = loaded.PlantZ;
                WildCreatures = loaded.WildCreatures;
                Tribes = loaded.Tribes;
                DroppedItems = loaded.DroppedItems;
                LocalProfile = loaded.LocalProfile;

            }
            catch
            {

            }
            
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void ExportPack(string fileName)
        {
           
            string filePath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);


            try
            {
                ContentPack pack = this;

                string jsonContent = JsonConvert.SerializeObject(pack);
                var compressedContent = Zip(jsonContent);

                if (File.Exists(fileName)) File.Delete(fileName);

                using(var writer = new FileStream(fileName,FileMode.CreateNew))
                {
                    writer.Write(compressedContent, 0, compressedContent.Length);
                    writer.Flush();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        private void LoadGameData(ContentContainer container)
        {
            MapFilename = container.MapName;
            ContentDate = container.GameSaveTime;
            if (IncludeGameStructures)
            {

                ConcurrentBag<ContentStructure> loadedStructures = new ConcurrentBag<ContentStructure>();
                var mapDetectedTerminals = container.MapStructures.Where(s => (s.ClassName.StartsWith("TributeTerminal_") || s.ClassName.Contains("CityTerminal_")) && s.Latitude != null);
                if (mapDetectedTerminals != null)
                {
                    Parallel.ForEach(mapDetectedTerminals, terminal =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_Terminal",
                            Latitude = terminal.Latitude.GetValueOrDefault(0),
                            Longitude = terminal.Longitude.GetValueOrDefault(0),
                            X = terminal.X,
                            Y = terminal.Y,
                            Z = terminal.Z,
                            CreatedDateTime = terminal.CreatedDateTime,
                            CreatedTimeInGame = terminal.CreatedTimeInGame,
                            Inventory = IncludeGameStructureContent ? terminal.Inventory : new ContentInventory()
                        });

                    });
                }


                //user defined terminals
                //if (Program.ProgramConfig.TerminalMarkers != null)
                //{
                //    var mapTerminals = loadedStructures.ToList();
                //    var terminals = Program.ProgramConfig.TerminalMarkers
                //        .Where(m =>
                //            m.Map.ToLower().StartsWith(MapFilename.ToLower())
                //            //exclude any that match map detected terminal location
                //            & !mapTerminals.Any(t => t.Latitude.ToString().StartsWith(m.Lat.ToString()) && t.Longitude.ToString().StartsWith(m.Lon.ToString()))
                //        ).ToList();

                //    if (terminals != null)
                //    {
                //        Parallel.ForEach(terminals, terminal =>
                //        {
                //            loadedStructures.Add(new ContentStructure()
                //            {
                //                ClassName = "ASV_Terminal",
                //                Latitude = (float)terminal.Lat,
                //                Longitude = (float)terminal.Lon,
                //                X = terminal.X,
                //                Y = terminal.Y,
                //                Z = terminal.Z
                //            });

                //        });
                //    }

                //}
                if (!loadedStructures.IsEmpty) TerminalMarkers.AddRange(loadedStructures.ToList());


                //Charge nodes
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var chargeNodes = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("PrimalItem_PowerNodeCharge")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (chargeNodes != null && chargeNodes.Count > 0)
                {
                    Parallel.ForEach(chargeNodes, chargeNode =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_ChargeNode",
                            Latitude = chargeNode.Latitude.GetValueOrDefault(0),
                            Longitude = chargeNode.Longitude.GetValueOrDefault(0),
                            X = chargeNode.X,
                            Y = chargeNode.Y,
                            Z = chargeNode.Z,
                            CreatedDateTime = chargeNode.CreatedDateTime,
                            CreatedTimeInGame = chargeNode.CreatedTimeInGame,
                            Inventory = IncludeGameStructureContent ? chargeNode.Inventory : new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) ChargeNodes.AddRange(loadedStructures.ToList());

                //GlitchMarkers
                /*
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var glitches = Program.ProgramConfig.GlitchMarkers
                    .Where(
                        m => m.Map.ToLower().StartsWith(MapFilename.ToLower())
                        && (Math.Abs((decimal)m.Lat - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)m.Lon - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (glitches != null && glitches.Count > 0)
                {
                    Parallel.ForEach(glitches, glitch =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_Glitch",
                            Latitude = (float)glitch.Lat,
                            Longitude = (float)glitch.Lon,
                            X = glitch.X,
                            Y = glitch.Y,
                            Z = glitch.Z,
                            InventoryId = null
                        });

                    });
                }
                if (!loadedStructures.IsEmpty) GlitchMarkers.AddRange(loadedStructures.ToList());
                */


                //BeaverDams 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var beaverHouses = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("BeaverDam_C")
                        && s.Latitude!=null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (beaverHouses != null && beaverHouses.Count > 0)
                {
                    Parallel.ForEach(beaverHouses, house =>
                    {
                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_BeaverDam",
                            Latitude = house.Latitude,
                            Longitude = house.Longitude,
                            X = house.X,
                            Y = house.Y,
                            Z = house.Z,
                            Inventory = IncludeGameStructureContent ? house.Inventory : new ContentInventory()
                        };

                        loadedStructures.Add(loadedStructure);

                    });
                }
                if (!loadedStructures.IsEmpty) BeaverDams.AddRange(loadedStructures.ToList());


                //WyvernNests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var wyvernNests = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("WyvernNest_")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();
                if (wyvernNests != null && wyvernNests.Count > 0)
                {
                    Parallel.ForEach(wyvernNests, nest =>
                    {
                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_WyvernNest",
                            Latitude = nest.Latitude,
                            Longitude = nest.Longitude,
                            X = nest.X,
                            Y = nest.Y,
                            Z = nest.Z,
                            Inventory = IncludeGameStructureContent ? nest.Inventory : new ContentInventory()
                        };

                        loadedStructures.Add(loadedStructure);

                    });
                }
                if (!loadedStructures.IsEmpty) WyvernNests.AddRange(loadedStructures.ToList());


                //DrakeNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var drakeNests = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("RockDrakeNest_C")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (drakeNests != null && drakeNests.Count > 0)
                {
                    Parallel.ForEach(drakeNests, nest =>
                    {
                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_DrakeNest",
                            Latitude = nest.Latitude,
                            Longitude = nest.Longitude,
                            X = nest.X,
                            Y = nest.Y,
                            Z = nest.Z,
                            Inventory = IncludeGameStructureContent ? nest.Inventory : new ContentInventory()
                        };

                        loadedStructures.Add(loadedStructure);

                    });
                }
                if (!loadedStructures.IsEmpty) DrakeNests.AddRange(loadedStructures.ToList());


                //Deino nests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var deinoNests = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("DeinonychusNest_C")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (deinoNests != null && deinoNests.Count > 0)
                {
                    Parallel.ForEach(deinoNests, nest =>
                    {
                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_DeinoNest",
                            Latitude = (float)nest.Latitude,
                            Longitude = (float)nest.Longitude,
                            X = nest.X,
                            Y = nest.Y,
                            Z = nest.Z,
                            Inventory = IncludeGameStructureContent ? nest.Inventory : new ContentInventory()
                        };

                        loadedStructures.Add(loadedStructure);

                    });
                }
                if (!loadedStructures.IsEmpty) DeinoNests.AddRange(loadedStructures.ToList());

                //MagmaNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var magmaNests = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("CherufeNest_C")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (magmaNests != null && magmaNests.Count > 0)
                {
                    Parallel.ForEach(magmaNests, nest =>
                    {

                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_MagmaNest",
                            Latitude = nest.Latitude,
                            Longitude = nest.Longitude,
                            X = nest.X,
                            Y = nest.Y,
                            Z = nest.Z,
                            Inventory = IncludeGameStructureContent ? nest.Inventory : new ContentInventory()
                        };
                       
                        loadedStructures.Add(loadedStructure);

                    });
                }
                if (!loadedStructures.IsEmpty) MagmaNests.AddRange(loadedStructures.ToList());


                //OilVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var oilVeins = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("OilVein_")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (oilVeins != null && oilVeins.Count > 0)
                {
                    Parallel.ForEach(oilVeins, vein =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_OilVein",
                            Latitude = vein.Latitude,
                            Longitude = vein.Longitude,
                            X = vein.X,
                            Y = vein.Y,
                            Z = vein.Z,
                            Inventory = IncludeGameStructureContent ? vein.Inventory : new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) OilVeins.AddRange(loadedStructures.ToList());

                //WaterVeins 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var waterVeins = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("WaterVein_")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (waterVeins != null && waterVeins.Count > 0)
                {
                    Parallel.ForEach(waterVeins, vein =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_WaterVein",
                            Latitude = vein.Latitude,
                            Longitude = vein.Longitude,
                            X = vein.X,
                            Y = vein.Y,
                            Z = vein.Z,
                            Inventory = IncludeGameStructureContent ? vein.Inventory : new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) WaterVeins.AddRange(loadedStructures.ToList());

                //GasVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var gasVeins = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("GasVein_")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (gasVeins != null && gasVeins.Count > 0)
                {
                    Parallel.ForEach(gasVeins, vein =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_GasVein",
                            Latitude = vein.Latitude,
                            Longitude = vein.Longitude,
                            X = vein.X,
                            Y = vein.Y,
                            Z = vein.Z,
                            Inventory = IncludeGameStructureContent ? vein.Inventory : new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) GasVeins.AddRange(loadedStructures.ToList());

                //Artifacts 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var artifacts = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("ArtifactCrate_")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (artifacts != null && artifacts.Count > 0)
                {
                    Parallel.ForEach(artifacts, artifact =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_Artifact",
                            Latitude = artifact.Latitude,
                            Longitude = artifact.Longitude,
                            X = artifact.X,
                            Y = artifact.Y,
                            Z = artifact.Z,
                            Inventory = IncludeGameStructureContent ? artifact.Inventory : new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) Artifacts.AddRange(loadedStructures.ToList());

                //PlantZ
                //Artifacts 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var plants = container.MapStructures
                    .Where(s =>
                        s.ClassName.StartsWith("Structure_PlantSpeciesZ")
                        && s.Latitude != null
                        && (Math.Abs((decimal)s.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if (plants != null && plants.Count > 0)
                {
                    Parallel.ForEach(plants, plant =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_PlantZ",
                            Latitude = plant.Latitude,
                            Longitude = plant.Longitude,
                            X = plant.X,
                            Y = plant.Y,
                            Z = plant.Z,
                            Inventory = IncludeGameStructureContent?plant.Inventory:new ContentInventory()
                        });
                    });
                }
                if (!loadedStructures.IsEmpty) PlantZ.AddRange(loadedStructures.ToList());


            }






            if (IncludeWild)
            {
                
                //WildCreatures
                WildCreatures = container.WildCreatures
                .Where(w =>
                    (w.Latitude.HasValue &!float.IsNaN(w.Latitude.Value))
                    && (w.Longitude.HasValue & !float.IsNaN(w.Longitude.Value))
                    && (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                    && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                ).ToList();
            }

            if (IncludeTribesPlayers)
            {
                Tribes = container.Tribes;

                //remove players not in range or not selected filter player
                if (Tribes.Count > 0)
                {
                    Tribes.ForEach(t =>
                    {
                        t.Players.ToList().RemoveAll(p =>
                            (Math.Abs((decimal)p.Latitude.GetValueOrDefault(0) - FilterLatitude) > FilterRadius)
                            && (Math.Abs((decimal)p.Longitude.GetValueOrDefault(0) - FilterLongitude) > FilterRadius)
                            && (ExportedForPlayer == 0 || (ExportedForPlayer!=0 && p.Id != ExportedForPlayer))
                        );

                        if (!IncludeTamed)
                        {
                            t.Tames = new ConcurrentBag<ContentTamedCreature>(); ;
                        }

                        if (!IncludePlayerStructures)
                        {
                            t.Structures = new ConcurrentBag<ContentStructure>();
                        }
                    });
                }
            }

            if (IncludeDroppedItems)
            {
                //Dropped items
                ConcurrentBag<ContentDroppedItem> loadedDroppedItems = new ConcurrentBag<ContentDroppedItem>();
                DroppedItems = new List<ContentDroppedItem>();
                DroppedItems.AddRange(container.DroppedItems
                    .Where(i =>
                        (i.DroppedByPlayerId == 0 || i.DroppedByPlayerId == ExportedForPlayer || ExportedForPlayer == 0)
                        && i.Latitude != null
                        && (Math.Abs((decimal)i.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)i.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                    ).ToList());

            }
        }


        private void LoadPackData(ContentPack pack)
        {
            //load content pack from Ark savegame 
            MapFilename = pack.MapFilename;
            ContentDate = pack.ContentDate;
            LocalProfile = pack.LocalProfile;

            if (IncludeGameStructures)
            {
                //all locations
                TerminalMarkers = pack.TerminalMarkers;
                                
                
                
                //possibly location restricted
                ChargeNodes = pack.ChargeNodes.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                GlitchMarkers = pack.GlitchMarkers.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                BeaverDams = pack.BeaverDams.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                WyvernNests = pack.WyvernNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                DrakeNests = pack.DrakeNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                DeinoNests = pack.DeinoNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                MagmaNests = pack.MagmaNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                OilVeins = pack.OilVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                WaterVeins = pack.WaterVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                GasVeins = pack.GasVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                Artifacts = pack.Artifacts.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                PlantZ = pack.PlantZ.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 

                if (!IncludeGameStructureContent)
                {
                    //remove linked inventory, and then unassign
                    BeaverDams.ForEach(x =>
                    {
                        x.Inventory.Items.Clear();
                        
                    });
                    WyvernNests.ForEach(x =>
                    {
                        x.Inventory.Items.Clear();

                    });
                    DrakeNests.ForEach(x =>
                    {
                        x.Inventory.Items.Clear();

                    });
                    DeinoNests.ForEach(x =>
                    {
                        x.Inventory.Items.Clear();

                    });
                    MagmaNests.ForEach(x =>
                    {
                        x.Inventory.Items.Clear();

                    });


                }

            }


            if (IncludeWild)
            {
                //WildCreatures
                WildCreatures = pack.WildCreatures.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
            }

            if (IncludeTribesPlayers)
            {
                //Tribes 
                Tribes = pack.Tribes.Where(t => (t.TribeId == ExportedForTribe || ExportedForTribe == 0) || (t.Players.Any(p=>p.Id == ExportedForPlayer && ExportedForPlayer!=0))).ToList();
            }

            //player structures
            if (Tribes != null && Tribes.Count > 0)
            {
                if (!IncludePlayerStructures)
                {
                    //remove structures, not included in the filter
                    Tribes.ForEach(t => {
                        t.Structures = new ConcurrentBag<ContentStructure>();
                    });
                }
                if (!IncludeTamed)
                {
                    Tribes.ForEach(t => {
                        t.Tames = new ConcurrentBag<ContentTamedCreature>();
                    });
                }

                if (ExportedForPlayer != 0)
                {
                    //specific player, dont give data for all tribe members
                    Tribes.ForEach(t => 
                    {

                        t.Players.ToList().RemoveAll(p => p.Id != ExportedForPlayer);
                        
                    });
                }
            }

            if (IncludeDroppedItems)
            {
                DroppedItems = pack.DroppedItems.Where(i =>
                                                            (i.DroppedByPlayerId == ExportedForPlayer || ExportedForPlayer == 0)
                                                            &&  (Math.Abs((decimal)i.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)i.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                      ).ToList();
            }
        }


        
        private void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        private byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        private string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }


    }
}
