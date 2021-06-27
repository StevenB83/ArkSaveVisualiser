using ARKViewer.Configuration;
using ARKViewer.Models;
using ASVPack.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    static class Program
    {

        static Dictionary<string, int> ItemImageKeyMap = new Dictionary<string, int>();
        static Dictionary<string, int> MarkerImageKeyMap = new Dictionary<string, int>();
        public static ImageList ItemImageList { get; set; } = new ImageList();
        public static ImageList MarkerImageList { get; set; } = new ImageList();
        public static string ItemImageFolder { get; set; } = "";
        public static string MarkerImageFolder { get; set; } = "";
        public static string LastLoadedSaveFilename { get; set; } = "";
        public static ContentPack LoadedPack { get; set; } = null;
        public static ViewerConfiguration ProgramConfig { get; set; }
        public static ApiConfiguration ApiConfig { get; set; }


        public static Dictionary<string, string> MapFilenameMap = new Dictionary<string, string>
            {
                { "theisland.ark", "The Island" },
                { "thecenter.ark", "The Center" },
                { "scorchedearth_p.ark","Scorched Earth"},
                { "aberration_p.ark", "Aberration"},
                { "extinction.ark", "Extinction"},
                { "ragnarok.ark", "Ragnarok"},
                { "valguero_p.ark", "Valguero" },
                { "crystalisles.ark", "Crystal Isles" },
                { "genesis.ark", "Genesis" },
                { "gen2.ark", "Genesis 2" },
                { "astralark.ark", "AstralARK" },
                { "hope.ark", "Hope"},
                { "tunguska_p.ark", "Tunguska"},
                { "caballus_p.ark", "Caballus"},
                { "viking_p.ark", "Fjördur"},
                { "tiamatprime.ark", "Tiamat Prime"},
                { "glacius_p.ark", "Glacius"},
                { "antartika.ark", "Anartika" },
                { "lostisland.ark", "Lost Island" }
            };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //param setup
            string appFolder = AppContext.BaseDirectory;
            string logFilename = Path.Combine(appFolder, @"ASV.log");

            ItemImageFolder = Path.Combine(appFolder, @"images\icons\");
            MarkerImageFolder = Path.Combine(appFolder, @"images\");

            if (!Directory.Exists(MarkerImageFolder)) Directory.CreateDirectory(MarkerImageFolder);
            if (!Directory.Exists(ItemImageFolder)) Directory.CreateDirectory(ItemImageFolder);

            //load config
            ProgramConfig = new ViewerConfiguration();

            //support quoted command line arguments which doesn't seem to be supported with Environment.GetCommandLineArgs() 
            string[] commandArguments = Regex.Split(Environment.CommandLine.Trim(), " (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            commandArguments = commandArguments.Where(a => string.IsNullOrEmpty(a) == false).ToArray();

            if (commandArguments != null && commandArguments.Length > 1)
            {

                ExportWithCommandLineOptions(commandArguments);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                frmViewer mainForm = new frmViewer();
                mainForm.UpdateProgress("Loading content pack...");
                mainForm.Show();
                mainForm.BringToFront();
                Application.DoEvents();
                mainForm.LoadContent(ProgramConfig.SelectedFile);

                Application.Run(mainForm);
            }
        }

        private static void ExportWithCommandLineOptions(string[] commandArguments)
        {
            string logFilename = Path.Combine(AppContext.BaseDirectory, @"ASV.log");

            //used when exporting ASV pack data
            string commandOptionCheck = commandArguments[1].ToString().Trim().ToLower();
            string exportFilePath = Path.Combine(AppContext.BaseDirectory, @"Export\");
            string exportFilename = Path.Combine(exportFilePath, "");

            //arguments provided, export and exit
            using (TextWriter logWriter = new StreamWriter(logFilename))
            {
                logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - ASV Command Line Started: {commandArguments.Length}");
                logWriter.Flush();

                int argIndex = 0;
                foreach (string arg in commandArguments)
                {
                    logWriter.WriteLine($"\tArg-{argIndex} = {arg}");
                    argIndex++;
                }
                logWriter.Flush();


                //command line, load save game data for export
                string inputFilename = "";
                if (commandArguments.Length > 2)
                {
                    //config specified
                    inputFilename = commandArguments[2].ToString().Trim().Replace("\"", "");
                }

                if (commandArguments.Length > 3)
                {
                    exportFilename = commandArguments[3].ToString().Trim().Replace("\"", "");
                    exportFilePath = Path.GetDirectoryName(exportFilename);
                }

                try
                {
                    switch (commandOptionCheck)
                    {
                        case "pack":
                            logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting ASV pack for coniguration: {inputFilename}");
                            logWriter.Flush();

                            ExportCommandLinePack(inputFilename);

                            break;

                        case "json":
                            logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON for configuration: {inputFilename}");
                            logWriter.Flush();

                            ExportCommandLine(inputFilename);

                            break;
                        default:
                            //direct command line export
                            ContentContainer container = new ContentContainer();
                            container.LoadSaveGame(inputFilename);
                            ASVDataManager exportManger = new ASVDataManager(container);

                            switch (commandOptionCheck)
                            {
                                case "all":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (all) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportAll(exportFilePath);
                                    break;
                                case "structures":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (structures) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportPlayerStructures(exportFilename);
                                    break;
                                case "tribes":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (tribes) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportPlayerTribes(exportFilename);
                                    break;
                                case "players":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (players) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportPlayers(exportFilename);
                                    break;
                                case "wild":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (wild) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportWild(exportFilename);
                                    break;
                                case "tamed":
                                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Exporting JSON (tamed) for: {inputFilename}");
                                    logWriter.Flush();

                                    exportManger.ExportTamed(exportFilename);
                                    break;
                            }
                            exportManger = null;

                            logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Completed export for: {inputFilename}");

                            break;
                    }

                    Environment.ExitCode = 0;
                }
                catch (Exception ex)
                {

                    logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Failed to export: \n{ex.Message.ToString()}");
                    logWriter.Flush();
                    Environment.ExitCode = -1;
                }
                finally
                {
                    logWriter.Flush();
                }

            };

            Application.Exit();

        }

        private static void ExportCommandLinePack(string configFilename)
        {

            //defaults
            string mapFilename = "";
            string exportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_ContentPack.asv");
            long tribeId = 0;
            long playerId = 0;
            decimal filterLat = 50;
            decimal filterLon = 50;
            decimal filterRad = 250;
            bool packStructureLocations = true;
            bool packStructureContent = true;
            bool packDroppedItems = true;
            bool packTribesPlayers = true;
            bool packTamed = true;
            bool packWild = true;
            bool packPlayerStructures = true;


            if (File.Exists(configFilename))
            {
                //config found, load settings from file.
                string packConfigText = File.ReadAllText(configFilename);
                try
                {
                    JObject packConfig = JObject.Parse(packConfigText);

                    mapFilename = packConfig.Property("mapFilename") == null ? "" : packConfig.Property("mapFilename").Value.ToString();
                    exportFilename = packConfig.Property("tribeId") == null ? "" : packConfig.Property("exportFilename").Value.ToString();
                    tribeId = packConfig.Property("exportFilename") == null ? 0 : (long)packConfig.Property("tribeId").Value;
                    playerId = packConfig.Property("playerId") == null ? 0 : (long)packConfig.Property("playerId").Value;
                    filterLat = packConfig.Property("filterLat") == null ? 50 : (decimal)packConfig.Property("filterLat").Value;
                    filterLon = packConfig.Property("filterLon") == null ? 50 : (decimal)packConfig.Property("filterLon").Value;
                    filterRad = packConfig.Property("filterRad") == null ? 250 : (decimal)packConfig.Property("filterRad").Value;
                    packStructureLocations = packConfig.Property("packStructureLocations") == null ? true : (bool)packConfig.Property("packStructureLocations").Value;
                    packStructureContent = packConfig.Property("packStructureContent") == null ? true : (bool)packConfig.Property("packStructureContent").Value;
                    packDroppedItems = packConfig.Property("packDroppedItems") == null ? true : (bool)packConfig.Property("packDroppedItems").Value;
                    packTribesPlayers = packConfig.Property("packTribesPlayers") == null ? true : (bool)packConfig.Property("packTribesPlayers").Value;
                    packTamed = packConfig.Property("packTamed") == null ? true : (bool)packConfig.Property("packTamed").Value;
                    packWild = packConfig.Property("packWild") == null ? true : (bool)packConfig.Property("packWild").Value;
                    packPlayerStructures = packConfig.Property("packPlayerStructures") == null ? true : (bool)packConfig.Property("packPlayerStructures").Value;


                }
                catch
                {
                    //bad file data, ignore
                }
            }


            //ensure folder exists
            string exportFolder = Path.GetDirectoryName(exportFilename);
            if (exportFolder.Length == 0) exportFolder = Path.Combine(AppContext.BaseDirectory, @"Export\");
            if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

            //ensure filename set, and ends with .asv
            if (exportFilename.Length == 0) exportFilename = Path.Combine(exportFolder, "ASV_ContentPack.asv");
            if (!exportFilename.ToLower().EndsWith("asv")) exportFilename += ".asv";

            //create pack and export
            ContentContainer exportPack = new ContentContainer();
            exportPack.LoadSaveGame(mapFilename);
            ContentPack pack = new ContentPack(exportPack, tribeId, playerId, filterLat, filterLon, filterRad,packStructureLocations, packStructureContent,packTribesPlayers,packTamed,packWild,packPlayerStructures,packDroppedItems);
            pack.ExportPack(exportFilename);
        }

        private static void ExportCommandLine(string configFilename)
        {


            long tribeId = 0;
            long playerId = 0;
            decimal filterLat = 50;
            decimal filterLon = 50;
            decimal filterRad = 250;

            string tribeExportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Tribes.json");
            string tribeImageFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Tribes.png");
            bool tribeStructures = true;
            bool mapStructureContent = true;
            bool tribePlayers = true;
            bool tribeTames = true;

            string structureExportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Structures.json");
            string structureImageFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Structures.png");
            string structureClassName = "";

            string playerExportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Players.json");
            string playerImageFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Players.png");

            string wildExportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Wild.json");
            string wildImageFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Wild.png");
            string wildClassName = "";
            int wildMinLevel = 0;
            int wildMaxLevel = 999;


            string tamedExportFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Tamed.json");
            string tamedImageFilename = Path.Combine(AppContext.BaseDirectory, @"Export\ASV_Export_Tamed.png");
            string tamedClassName = "";

            string mapFilename = Program.ProgramConfig.SelectedFile;

            if (File.Exists(configFilename))
            {
                string configText = File.ReadAllText(configFilename);
                try
                {
                    JObject packConfig = JObject.Parse(configText);

                    mapFilename = packConfig.Property("mapFilename") == null ? "" : packConfig.Property("mapFilename").Value.ToString();

                    //if no save file provided, use ProgramConfig
                    if (mapFilename.Length == 0) mapFilename = Program.ProgramConfig.SelectedFile;


                    // parse filters for export options
                    tribeId = packConfig.Property("tribeId") == null ? 0 : (long)packConfig.Property("tribeId").Value;
                    playerId = packConfig.Property("playerId") == null ? 0 : (long)packConfig.Property("playerId").Value;
                    filterLat = packConfig.Property("filterLat") == null ? 50 : (decimal)packConfig.Property("filterLat").Value;
                    filterLon = packConfig.Property("filterLon") == null ? 50 : (decimal)packConfig.Property("filterLon").Value;
                    filterRad = packConfig.Property("filterRad") == null ? 250 : (decimal)packConfig.Property("filterRad").Value;
                    mapStructureContent = packConfig.Property("structureContent") == null ? false : (bool)packConfig.Property("structureContent").Value;

                    //Tribes
                    JObject exportTribes = (JObject)packConfig["exportTribes"];
                    if (exportTribes != null)
                    {
                        tribeExportFilename = exportTribes.Property("jsonFilename") == null ? "" : (string)exportTribes.Property("jsonFilename").Value;
                        tribeImageFilename = exportTribes.Property("imageFilename") == null ? "" : (string)exportTribes.Property("imageFilename").Value;
                        tribeStructures = exportTribes.Property("addStructures") == null ? true : (bool)exportTribes.Property("addStructures").Value;
                        tribePlayers = exportTribes.Property("addPlayers") == null ? true : (bool)exportTribes.Property("addPlayers").Value;
                        tribeTames = exportTribes.Property("addTames") == null ? true : (bool)exportTribes.Property("addTames").Value;
                    }

                    //Structures
                    JObject exportStructures = (JObject)packConfig["exportStructures"];
                    if (exportStructures != null)
                    {
                        structureExportFilename = exportStructures.Property("jsonFilename") == null ? "" : exportStructures.Property("jsonFilename").Value.ToString();
                        structureImageFilename = exportStructures.Property("imageFilename") == null ? "" : exportStructures.Property("imageFilename").Value.ToString();
                        structureClassName = exportStructures.Property("className") == null ? "" : exportStructures.Property("className").Value.ToString();
                    }

                    //Players
                    JObject exportPlayers = (JObject)packConfig["exportPlayers"];
                    if (exportPlayers != null)
                    {
                        playerExportFilename = exportPlayers.Property("jsonFilename") == null ? "" : exportPlayers.Property("jsonFilename").Value.ToString();
                        playerImageFilename = exportPlayers.Property("imageFilename") == null ? "" : exportPlayers.Property("imageFilename").Value.ToString();

                    }

                    //Wilds
                    JObject exportWild = (JObject)packConfig["exportWild"];
                    if (exportWild != null)
                    {
                        wildExportFilename = exportWild.Property("jsonFilename") == null ? "" : exportWild.Property("jsonFilename").Value.ToString();
                        wildImageFilename = exportWild.Property("imageFilename") == null ? "" : exportWild.Property("imageFilename").Value.ToString();
                        wildClassName = exportWild.Property("className") == null ? "" : exportWild.Property("className").Value.ToString();
                        wildMinLevel = exportWild.Property("minLevel") == null ? 0 : (int)exportWild.Property("minLevel").Value;
                        wildMaxLevel = exportWild.Property("maxLevel") == null ? 0 : (int)exportWild.Property("maxLevel").Value;

                    }

                    //Tamed
                    JObject exportTamed = (JObject)packConfig["exportTamed"];
                    if (exportTamed != null)
                    {
                        tamedExportFilename = exportTamed.Property("jsonFilename") == null ? "" : exportTamed.Property("jsonFilename").Value.ToString();
                        tamedImageFilename = exportTamed.Property("imageFilename") == null ? "" : exportTamed.Property("imageFilename").Value.ToString();
                        tamedClassName = exportTamed.Property("className") == null ? "" : exportTamed.Property("className").Value.ToString();
                    }

                }
                catch
                {
                    //bad file data, ignore
                }
            }

            //load game data
            if (!File.Exists(mapFilename))
            {
                return;
            }

            //load everything
            ContentContainer exportContainer = new ContentContainer();
            exportContainer.LoadSaveGame(mapFilename);

            //filter a pack for export
            ContentPack filteredPack = new ContentPack(exportContainer, tribeId, playerId, filterLat, filterLon, filterRad,true,mapStructureContent, tribePlayers, tribeTames, true, tribeStructures, false);

            //load manager from filtered pack
            ASVDataManager exportManger = new ASVDataManager(filteredPack);

            //Export tribes
            if (tribeExportFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(tribeExportFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                exportManger.ExportPlayerTribes(tribeExportFilename);
            }
            if (tribeImageFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(tribeImageFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                var image = exportManger.GetMapImageTribes(tribeId, tribeStructures, tribePlayers, tribeTames, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, new List<ContentMarker>());
                if (image != null)
                {
                    image.Save(tribeImageFilename);
                }
            }




            //Structures
            if (structureExportFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(structureExportFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                exportManger.ExportPlayerStructures(structureExportFilename);
            }

            if (structureImageFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(structureImageFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                var image = exportManger.GetMapImagePlayerStructures(structureImageFilename, tribeId, playerId, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, new List<ContentMarker>());
                if (image != null)
                {
                    image.Save(structureImageFilename);
                }
            }



            //Export Players
            if (playerExportFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(playerExportFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                exportManger.ExportPlayers(playerExportFilename);
            }

            if (playerImageFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(playerImageFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                var image = exportManger.GetMapImagePlayers(tribeId, playerId, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, new List<ContentMarker>());
                if (image != null)
                {
                    image.Save(playerImageFilename);
                }
            }

            //Export Wild
            if (wildExportFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(wildExportFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                exportManger.ExportWild(wildExportFilename);
            }
            if (wildImageFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(wildImageFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                var image = exportManger.GetMapImageWild(wildClassName, wildMinLevel, wildMaxLevel, (float)filterLat, (float)filterLon, (float)filterRad, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, new List<ContentMarker>());
                if (image != null)
                {
                    image.Save(tamedImageFilename);
                }
            }

            //Export tamed
            if (tamedExportFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(tamedExportFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                exportManger.ExportTamed(tamedExportFilename);
            }
            if (tamedImageFilename.Length > 0)
            {
                string exportFolder = Path.GetDirectoryName(tamedImageFilename);
                if (!Directory.Exists(exportFolder)) Directory.CreateDirectory(exportFolder);

                var image = exportManger.GetMapImageTamed(tamedClassName, true, tribeId, playerId, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, new List<ContentMarker>());
                if (image != null)
                {
                    image.Save(tamedImageFilename);
                }
            }

        }


        public static int GetItemImageIndex(string itemImageFilename)
        {
            ItemImageKeyMap.TryGetValue(itemImageFilename, out int imageIndex);
            return imageIndex;
        }

        public static int GetMarkerImageIndex(string markerImageFilename)
        {
            MarkerImageKeyMap.TryGetValue(markerImageFilename, out int imageIndex);
            return imageIndex;
        }
    }
}
