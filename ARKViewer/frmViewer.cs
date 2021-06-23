using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Renci.SshNet;
using Timer = System.Windows.Forms.Timer;
using FluentFTP;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ARKViewer.Configuration;
using ARKViewer.Models;
using ASVPack.Models;
using ARKViewer.Models.NameMap;

namespace ARKViewer
{
    public partial class frmViewer : Form
    {


        Timer saveCheckTimer = new Timer();
        frmMapView MapViewer = null;

        private bool isLoading = false;
        
        private ColumnHeader SortingColumn_DetailTame = null;
        private ColumnHeader SortingColumn_DetailWild = null;
        private ColumnHeader SortingColumn_Players = null;
        private ColumnHeader SortingColumn_Structures = null;
        private ColumnHeader SortingColumn_Tribes = null;
        private ColumnHeader SortingColumn_Drops = null;
        private ColumnHeader SortingColumn_ItemList = null;

        private string savePath = Path.GetDirectoryName(Application.ExecutablePath);

        //wrapper for the information we need from ARK save data
        ASVDataManager cm = null;


        private void LoadWindowSettings()
        {
            var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);

            if (savedWindow != null)
            {
                var targetScreen = Screen.FromPoint(new Point(savedWindow.Left, savedWindow.Top));
                if (targetScreen == null) return;

                if (targetScreen.DeviceName == null || targetScreen.DeviceName == savedWindow.Monitor)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Left = savedWindow.Left;
                    this.Top = savedWindow.Top;
                    this.Width = savedWindow.Width;
                    this.Height = savedWindow.Height;
                }
            }
        }

        private void UpdateWindowSettings()
        {
            //only save location if normal window, do not save location/size if minimized/maximized
            if (this.WindowState == FormWindowState.Normal)
            {
                var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);
                if (savedWindow == null)
                {
                    savedWindow = new ViewerWindow();
                    savedWindow.Name = this.Name;
                    ARKViewer.Program.ProgramConfig.Windows.Add(savedWindow);
                }

                if (savedWindow != null)
                {
                    var restoreScreen = Screen.FromHandle(this.Handle);

                    savedWindow.Left = this.Left;
                    savedWindow.Top = this.Top;
                    savedWindow.Width = this.Width;
                    savedWindow.Height = this.Height;
                    savedWindow.Monitor = restoreScreen.DeviceName;

                }
            }
        }



        private void InitializeDefaults()
        {
            isLoading = true;

            LoadWindowSettings();
            chkCryo.Checked = Program.ProgramConfig.StoredTames;
            lblVersion.Text = $"{Application.ProductVersion}";
            Application.DoEvents();


            isLoading = false;
        }


        public void UpdateProgress(string newProgress)
        {
            lblStatus.Text = newProgress;
            lblStatus.Refresh();
        }

        public void LoadContent(string fileName)
        {
            this.Cursor = Cursors.WaitCursor;

            cm = null;
            lblMapDate.Text = "No Map Loaded";
            lblMapTypeName.Text = "Unknown Data";

            UpdateProgress("Loading content.");

            long startLoadTicks = DateTime.Now.Ticks;

            try
            {
                ContentContainer container = new ContentContainer();
                container.LoadSaveGame(fileName);
                cm = new ASVDataManager(container);

                try
                {
                    if (MapViewer != null) MapViewer.OnMapClicked -= MapViewer_OnMapClicked;
                }
                catch
                {
                }
                MapViewer = frmMapView.GetForm(cm);
                MapViewer.OnMapClicked += MapViewer_OnMapClicked;

                string mapFileDateString = (cm.ContentDate.Equals(new DateTime()) ? "n/a" : cm.ContentDate.ToString("dd MMM yyyy HH:mm"));
                lblMapDate.Text = $"{cm.MapName}: {mapFileDateString}";

                switch (Program.ProgramConfig.Mode)
                {
                    case ViewerModes.Mode_SinglePlayer:
                        lblMapTypeName.Text = $"Single Player";
                        break;
                    case ViewerModes.Mode_Offline:
                        lblMapTypeName.Text = $"Offline: {Program.ProgramConfig.SelectedFile}";
                        break;
                    case ViewerModes.Mode_ContentPack:
                        lblMapTypeName.Text = $"ASV: {Program.ProgramConfig.SelectedFile}";

                        break;
                    case ViewerModes.Mode_Ftp:
                        lblMapTypeName.Text = $"FTP: {Program.ProgramConfig.SelectedServer}";

                        break;
                    default:
                        lblMapTypeName.Text = "";
                        break;
                }



                var allWilds = cm.GetWildCreatures(0, int.MaxValue, 50, 50, float.MaxValue, "");
                if (allWilds.Count > 0)
                {
                    int maxLevel = allWilds.Max(w => w.BaseLevel);
                    udWildMax.Maximum = maxLevel;
                    udWildMin.Maximum = maxLevel;
                    udWildMax.Value = maxLevel;
                }

                RefreshWildSummary();
                RefreshTamedSummary();
                RefreshTribeSummary();


                RefreshPlayerTribes();
                RefreshTamedTribes();
                RefreshCryoTribes();
                RefreshStructureTribes();
                RefreshItemListTribes();
                RefreshStructureSummary();
                RefreshDroppedPlayers();

                DrawMap(0, 0);

                var timeLoaded = TimeSpan.FromTicks(DateTime.Now.Ticks - startLoadTicks);
                UpdateProgress($"Content pack loaded in {timeLoaded.TotalSeconds.ToString("f0")} seconds.");

                if (cm.ContentDate == null || cm.ContentDate.Equals(new DateTime()))
                {
                    //no map loaded
                    UpdateProgress("Content failed to load.  Please check settings or refresh download to try again.");
                }

            }
            catch(Exception ex)
            {
                //failed to load save game
                UpdateProgress("Content failed to load.  Please check settings or refresh download to try again.");
            }

            isLoading = true;
            cboSelectedMap.Items.Clear();
            switch (Program.ProgramConfig.Mode)
            {
                case ViewerModes.Mode_Ftp:
                    foreach(var i in Program.ProgramConfig.ServerList)
                    {
                        string localFilename = Path.Combine(AppContext.BaseDirectory, $@"{i.Name}\", i.Map);

                        int newIndex = cboSelectedMap.Items.Add(new ASVComboValue(localFilename, i.Name));
                        if (localFilename.ToLower() == Program.ProgramConfig.SelectedFile.ToLower())
                        { 
                            cboSelectedMap.SelectedIndex = newIndex; 
                        }
                    }
                    break;
                case ViewerModes.Mode_Offline:
                    foreach (var i in Program.ProgramConfig.OfflineList)
                    {
                        int newIndex = cboSelectedMap.Items.Add(new ASVComboValue(i.Key, i.Value));
                        if (i.Key.ToLower() == Program.ProgramConfig.SelectedFile.ToLower()) cboSelectedMap.SelectedIndex = newIndex;
                    }
                    break;
                case ViewerModes.Mode_SinglePlayer:

                    break;
                default:

                    break;
            }
            isLoading = false;
            
            this.Cursor = Cursors.Default;
        }

        public frmViewer()
        {
            InitializeComponent();
            InitializeDefaults();
        }



        private void RefreshPlayerTribes()
        {
            if (cm == null) return;

            cboTribes.Items.Clear();
            cboTribes.Items.Add(new ASVComboValue("0", "[All Tribes]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var tribes = cm.GetTribes(0);

            if (tribes.Count() > 0)
            {
                foreach (var tribe in tribes)
                {
                    bool addTribe = true;
                    if (Program.ProgramConfig.HideNoBody)
                    {

                        addTribe = tribe.Players.Count > 0 && !tribe.Players.All(p => (p.Latitude == 0 && p.Longitude == 0));
                    }

                    if (addTribe)
                    {
                        ASVComboValue valuePair = new ASVComboValue(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }
            if (newItems.Count > 0)
            {
                cboTribes.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTribes.Items.Add(newItem);
                }

                cboTribes.EndUpdate();
            }

            cboTribes.SelectedIndex = 0;
        }

        private void RefreshItemListTribes()
        {
            if (cm == null) return;

            cboItemListTribe.Items.Clear();
            cboItemListTribe.Items.Add(new ASVComboValue("0", "[All Tribes]"));

            cboItemListItem.Items.Clear();
            cboItemListItem.Items.Add(new ASVComboValue("-1", "[Please Select]"));
            cboItemListItem.Items.Add(new ASVComboValue("", "[All Items]"));
            cboItemListItem.SelectedIndex = 0;

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var tribes = cm.GetTribes(0);

            if (tribes.Count() > 0)
            {

                List<string> playerItems = new List<string>();

                foreach (var tribe in tribes)
                {
                    bool addTribe = true;
                    if (Program.ProgramConfig.HideNoBody)
                    {

                        addTribe = tribe.Players.Count > 0 && !tribe.Players.All(p => (p.Latitude == 0 && p.Longitude == 0));
                    }

                    if (addTribe)
                    {
                        ASVComboValue valuePair = new ASVComboValue(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }

                    //add items regardless, different search type and want to see them all in this case

                    if (tribe.Structures != null && tribe.Structures.Count > 0)
                    {
                        tribe.Structures.ToList().ForEach(s =>
                        {
                            if (s.Inventory.Items.Count > 0)
                            {
                                var matchedItems = s.Inventory.Items.Where(i => !playerItems.Contains(i.ClassName)).Select(c => c.ClassName).Distinct().ToList();
                                if (matchedItems != null && matchedItems.Count > 0) playerItems.AddRange(matchedItems);
                            }
                        });
                    }

                    if (tribe.Tames != null && tribe.Tames.Count > 0)
                    {
                        tribe.Tames.ToList().ForEach(s =>
                        {
                            if (s.Inventory.Items.Count > 0)
                            {
                                var matchedItems = s.Inventory.Items.Where(i => !playerItems.Contains(i.ClassName)).Select(c => c.ClassName).Distinct().ToList();

                                if (matchedItems != null && matchedItems.Count > 0) playerItems.AddRange(matchedItems);
                            }

                        });
                    }
                }

                if (playerItems != null && playerItems.Count > 0)
                {
                    List<ASVComboValue> comboItems = new List<ASVComboValue>();
                    playerItems.ForEach(i =>
                    {
                        string displayName = i;

                        var itemMap = Program.ProgramConfig.ItemMap.FirstOrDefault(m => m.ClassName == i);
                        if (itemMap != null) displayName = itemMap.DisplayName;
                        comboItems.Add(new ASVComboValue(i, displayName));

                    });

                    cboItemListItem.Items.AddRange(comboItems.OrderBy(o => o.Value).ToArray());
                }

            }

            if (newItems.Count > 0)
            {
                cboItemListTribe.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboItemListTribe.Items.Add(newItem);
                }

                cboItemListTribe.EndUpdate();
            }

            cboItemListTribe.SelectedIndex = 0;






        }

        private void RefreshTamedTribes()
        {
            if (cm == null) return;

            cboTameTribes.Items.Clear();
            cboTameTribes.Items.Add(new ASVComboValue("0", "[All Tribes]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var allTribes = cm.GetTribes(0);
            if (allTribes != null && allTribes.Count() > 0)
            {
                foreach (var tribe in allTribes)
                {
                    bool addItem = true;

                    if (Program.ProgramConfig.HideNoTames)
                    {
                        addItem = (
                                    tribe.Tames != null
                                  );
                    }

                    if (addItem)
                    {
                        if (tribe.TribeName == null || tribe.TribeName.Length == 0) tribe.TribeName = "[N/A]";
                        ASVComboValue valuePair = new ASVComboValue(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }


            if (newItems.Count > 0)
            {
                cboTameTribes.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTameTribes.Items.Add(newItem);
                }

                cboTameTribes.EndUpdate();
            }
            cboTameTribes.SelectedIndex = 0;
        }


        private void RefreshCryoTribes()
        {

        }


        private void RefreshStructureTribes()
        {
            if (cm == null) return;

            cboStructureTribe.Items.Clear();
            cboStructureTribe.Items.Add(new ASVComboValue("0", "[All Tribes]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var allTribes = cm.GetTribes(0);
            if (allTribes.Count() > 0)
            {
                foreach (var tribe in allTribes)
                {
                    bool addItem = true;
                    if (Program.ProgramConfig.HideNoStructures)
                    {

                        addItem = (
                                    (tribe.Structures != null && tribe.Structures.Count > 0)
                                    ||
                                    (tribe.Tames != null && tribe.Tames.LongCount(w => (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")) > 0)
                                );

                    }

                    if (addItem)
                    {
                        if (tribe.TribeName == null || tribe.TribeName.Length == 0) tribe.TribeName = "[N/A]";
                        ASVComboValue valuePair = new ASVComboValue(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }
            if (newItems.Count > 0)
            {
                cboStructureStructure.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboStructureTribe.Items.Add(newItem);
                }

                cboStructureStructure.EndUpdate();
            }
            cboStructureTribe.SelectedIndex = 0;
        }


        private void RefreshDroppedPlayers()
        {
            if (cm == null) return;

            cboDroppedPlayer.Items.Clear();
            cboDroppedPlayer.Items.Add(new ASVComboValue("-1", "[None Player]"));
            cboDroppedPlayer.Items.Add(new ASVComboValue("0", "[All Players]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var allPlayers = cm.GetPlayers(0, 0);
            if (allPlayers.Count() > 0)
            {
                foreach (var player in allPlayers)
                {
                    ASVComboValue valuePair = new ASVComboValue(player.Id.ToString(), player.CharacterName != null && player.CharacterName.Length > 0 ? player.CharacterName : player.Name);
                    newItems.Add(valuePair);
                }
            }

            if (newItems.Count > 0)
            {
                cboDroppedPlayer.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboDroppedPlayer.Items.Add(newItem);
                }

                cboDroppedPlayer.EndUpdate();
            }
            cboDroppedPlayer.SelectedIndex = 0;
        }

        public void RefreshDroppedItems()
        {
            if (cm == null) return;

            cboDroppedItem.Items.Clear();

            List<ASVComboValue> newItems = new List<ASVComboValue>();
            cboDroppedItem.Items.Add(new ASVComboValue() { Key = "0", Value = "[Dropped Items]" });
            cboDroppedItem.Items.Add(new ASVComboValue() { Key = "-1", Value = "[Death Cache]" });

            long playerId = 0;
            if (cboDroppedPlayer.SelectedItem != null)
            {
                var selectedValue = (ASVComboValue)cboDroppedPlayer.SelectedItem;
                long.TryParse(selectedValue.Key, out playerId);
            }

            var droppedItems = cm.GetDroppedItems(playerId, "");
            if (droppedItems != null && droppedItems.Count() > 0)
            {
                //player
                ASVComboValue comboValue = (ASVComboValue)cboDroppedPlayer.SelectedItem;
                int.TryParse(comboValue.Key, out int selectedPlayerId);


                var droppedItemTypes = droppedItems.GroupBy(g => g.ClassName)
                                                         .Select(s => s.Key);


                if (droppedItemTypes != null && droppedItemTypes.Count() > 0)
                {

                    foreach (var className in droppedItemTypes)
                    {
                        var itemName = className;
                        var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == className).FirstOrDefault();

                        ASVComboValue classNameItem = new ASVComboValue(className, "");

                        if (itemMap != null && itemMap.DisplayName.Length > 0)
                        {
                            itemName = itemMap.DisplayName;
                            classNameItem.Value = itemName;

                        }

                        if (itemName == null || itemName.Length == 0) itemName = className;

                        newItems.Add(new ASVComboValue() { Key = className, Value = itemName });
                    }


                }



            }

            if (newItems.Count > 0)
            {
                cboDroppedItem.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboDroppedItem.Items.Add(newItem);
                }

                cboDroppedItem.EndUpdate();
            }
            cboDroppedItem.SelectedIndex = 0;
        }


        private void RefreshPlayerList()
        {
            if (cm == null) return;
            if (cboTribes.SelectedItem == null) return;

            btnCopyCommandPlayer.Enabled = false;

            ASVComboValue comboValue = (ASVComboValue)cboTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboPlayers.Items.Clear();
            cboPlayers.Items.Add(new ASVComboValue("0", "[All Players]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var tribes = cm.GetTribes(selectedTribeId);
            foreach (var tribe in tribes)
            {
                foreach (var player in tribe.Players)
                {
                    bool addPlayer = true;
                    if (Program.ProgramConfig.HideNoBody)
                    {
                        addPlayer = !(player.Latitude == 0 && player.Longitude == 0);
                    }

                    if (addPlayer)
                    {
                        ASVComboValue valuePair = new ASVComboValue(player.Id.ToString(), player.CharacterName != null && player.CharacterName.Length > 0 ? player.CharacterName : player.Name);
                        newItems.Add(valuePair);
                    }
                }
            }

            if (newItems.Count > 0)
            {
                cboPlayers.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboPlayers.Items.Add(newItem);
                }
                cboPlayers.EndUpdate();


            }
            cboPlayers.SelectedIndex = 0;
        }


        private void RefreshCryoPlayerList()
        {



        }



        private void RefreshTamePlayerList()
        {
            if (cm == null) return;
            if (cboTameTribes.SelectedItem == null) return;

            ASVComboValue comboValue = (ASVComboValue)cboTameTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboTamePlayers.Items.Clear();
            cboTamePlayers.Items.Add(new ASVComboValue("0", "[All Players]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();


            var tribes = cm.GetTribes(0);
            foreach (var tribe in tribes)
            {
                foreach (var player in tribe.Players)
                {

                    bool addItem = true;

                    if (Program.ProgramConfig.HideNoTames)
                    {
                        addItem = tribe.Tames != null && tribe.Tames.Count > 0;
                    }

                    if (addItem)
                    {
                        ASVComboValue valuePair = new ASVComboValue(player.Id.ToString(), player.CharacterName);
                        newItems.Add(valuePair);

                    }
                }
            }



            if (newItems.Count > 0)
            {
                cboTamePlayers.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTamePlayers.Items.Add(newItem);
                }
                cboTamePlayers.EndUpdate();

            }

            if (cboTamePlayers.Items.Count > 0)
            {
                cboTamePlayers.SelectedIndex = 0;
            }


        }

        private void RefreshStructurePlayerList()
        {
            if (cm == null) return;
            if (cboStructureTribe.SelectedItem == null) return;

            ASVComboValue comboValue = (ASVComboValue)cboStructureTribe.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboStructurePlayer.Items.Clear();
            cboStructurePlayer.Items.Add(new ASVComboValue("0", "[All Players]"));

            List<ASVComboValue> newItems = new List<ASVComboValue>();

            var tribes = cm.GetTribes(0);
            foreach (var tribe in tribes)
            {

                foreach (var player in tribe.Players)
                {
                    bool addItem = true;

                    if (Program.ProgramConfig.HideNoStructures)
                    {
                        addItem = (
                                    (tribe.Structures != null && tribe.Structures.Count > 0)
                                    ||
                                    (
                                        tribe.Tames != null
                                        && tribe.Tames.LongCount(w => (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")) > 0
                                    )

                                   );
                    }

                    if (addItem)
                    {

                        ASVComboValue valuePair = new ASVComboValue(player.Id.ToString(), player.CharacterName);
                        newItems.Add(valuePair);

                    }
                }

            }




            if (newItems.Count > 0)
            {
                cboStructurePlayer.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboStructurePlayer.Items.Add(newItem);
                }
                cboStructurePlayer.EndUpdate();

            }

            cboStructurePlayer.SelectedIndex = 0;

        }

        private void LoadPlayerDetail()
        {
            if (cm == null) return;
            if (cboTribes.SelectedItem == null) return;
            if (cboPlayers.SelectedItem == null) return;


            btnPlayerInventory.Enabled = false;

            //tribe
            ASVComboValue comboValue = (ASVComboValue)cboTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            //player
            comboValue = (ASVComboValue)cboPlayers.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);

            lvwPlayers.Items.Clear();
            lvwPlayers.Refresh();
            lvwPlayers.BeginUpdate();

            //Name, sex, lvl, lat, lon, hp, stam, melee, weight, speed, food,water, oxy, last on
            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
            var tribes = cm.GetTribes(selectedTribeId);

            foreach (var tribe in tribes)
            {
                var tribePlayers = tribe.Players.Where(p => p.Id == selectedPlayerId || selectedPlayerId == 0).ToList();

                foreach (var player in tribePlayers)
                {
                    bool addPlayer = true;
                    if (Program.ProgramConfig.HideNoBody)
                    {
                        addPlayer = !(player.Longitude.GetValueOrDefault(0) == 0 && player.Latitude.GetValueOrDefault(0) == 0);
                    }

                    if (addPlayer)
                    {
                        ListViewItem newItem = new ListViewItem(player.CharacterName);
                        newItem.SubItems.Add(tribe.TribeName);

                        newItem.SubItems.Add(player.Gender.ToString());
                        newItem.SubItems.Add(player.Level.ToString());

                        if (!(player.Longitude == 0 && player.Latitude == 0))
                        {
                            newItem.SubItems.Add(player.Latitude.Value.ToString("0.00"));
                            newItem.SubItems.Add(player.Longitude.Value.ToString("0.00"));

                        }
                        else
                        {
                            newItem.SubItems.Add("n/a");
                            newItem.SubItems.Add("n/a");
                        }

                        //0=health
                        //1=stamina
                        //2=torpor
                        //3=oxygen
                        //4=food
                        //5=water
                        //6=temperature
                        //7=weight
                        //8=melee damage
                        //9=movement speed
                        //10=fortitude
                        //11=crafting speed

                        newItem.SubItems.Add(player.Stats.GetValue(0).ToString()); //hp
                        newItem.SubItems.Add(player.Stats.GetValue(1).ToString()); //stam
                        newItem.SubItems.Add(player.Stats.GetValue(8).ToString()); //melee
                        newItem.SubItems.Add(player.Stats.GetValue(7).ToString()); //weight
                        newItem.SubItems.Add(player.Stats.GetValue(9).ToString()); //speed
                        newItem.SubItems.Add(player.Stats.GetValue(4).ToString()); //food
                        newItem.SubItems.Add(player.Stats.GetValue(5).ToString()); //water
                        newItem.SubItems.Add(player.Stats.GetValue(3).ToString()); //oxygen
                        newItem.SubItems.Add(player.Stats.GetValue(11).ToString());//crafting
                        newItem.SubItems.Add(player.Stats.GetValue(10).ToString());//fortitude


                        newItem.SubItems.Add((!player.LastActiveDateTime.HasValue || player.LastActiveDateTime.Value == DateTime.MinValue) ? "n/a" : player.LastActiveDateTime.Value.ToString("dd MMM yy HH:mm:ss"));
                        newItem.SubItems.Add(player.Name);
                        newItem.SubItems.Add(player.SteamId);
                        newItem.Tag = player;


                        listItems.Add(newItem);
                    }
                }

            }


            lvwPlayers.Items.AddRange(listItems.ToArray());

            if (SortingColumn_Players != null)
            {
                lvwPlayers.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_Players.Index, SortingColumn_Players.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwPlayers.Sort();
            }

            lvwPlayers.EndUpdate();
            lblPlayerTotal.Text = $"Count: {lvwPlayers.Items.Count}";
            DrawMap(0, 0);

        }




        private void BtnDownload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var downloadedFile = Download();
            if (File.Exists(downloadedFile))
            {
                ARKViewer.Program.ProgramConfig.SelectedFile = downloadedFile;
                LoadContent(downloadedFile);
            }

            this.Cursor = Cursors.Default;
        }


        private bool DeletePlayerFtp(ContentPlayer player)
        {

            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return false;

            this.Cursor = Cursors.WaitCursor;
            bool returnVal = true;


            string profilePath = selectedServer.SaveGamePath.Substring(0, selectedServer.SaveGamePath.LastIndexOf("/"));
            string playerProfileFilename = $"{player.SteamId}.arkprofile";
            string ftpFilePath = $"{profilePath}/{playerProfileFilename}";
            string serverUsername = selectedServer.Username;
            string serverPassword = selectedServer.Password;

            switch (selectedServer.Mode)
            {
                case 0:
                    //ftp
                    FtpClient ftpClient = new FtpClient(selectedServer.Address);

                    try
                    {
                        ftpClient.Credentials.UserName = selectedServer.Username;
                        ftpClient.Credentials.Password = selectedServer.Password;
                        ftpClient.Port = selectedServer.Port;

                        ftpClient.DeleteFile(ftpFilePath);

                    }
                    catch
                    {
                        returnVal = false;
                    }
                    finally
                    {
                        ftpClient = null;
                    }


                    break;
                case 1:
                    //sftp
                    SftpClient sftpClient = new SftpClient(selectedServer.Address, selectedServer.Port, serverUsername, serverPassword);
                    try
                    {
                        sftpClient.Connect();

                        sftpClient.DeleteFile(ftpFilePath);

                    }
                    catch
                    {
                        returnVal = false;
                    }
                    finally
                    {
                        sftpClient.Dispose();
                    }

                    break;
            }



            return returnVal;
        }






        private List<Tuple<string, DateTime>> GetFtpFileList(Uri address, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(address);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            List<Tuple<string, DateTime>> fileList = new List<Tuple<string, DateTime>>();
            string[] list = null;


            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var line in list)
            {
                if (!line.Contains("<DIR>"))
                {
                    //file
                    string fileLine = line.Substring(0, line.LastIndexOf(" "));

                    int.TryParse(fileLine.Substring(0, 2), out int dateMonth);
                    int.TryParse(fileLine.Substring(3, 2), out int dateDay);
                    int.TryParse(fileLine.Substring(6, 2), out int dateYear);

                    int.TryParse(fileLine.Substring(10, 2), out int dateHour);
                    int.TryParse(fileLine.Substring(13, 2), out int dateMin);
                    if (fileLine.ToLower().Contains("pm"))
                    {
                        dateHour += 12;
                        if (dateHour > 23)
                        {
                            dateHour = 0;
                        }
                    }

                    if (dateYear.ToString().Length == 2) dateYear = dateYear + 2000;


                    DateTime fileDateTime = DateTime.Now;
                    try
                    {

                        string[] FtpDateFormats = { "yyyyMMddHHmmss", "yyyyMMddHHmmss'.'f", "yyyyMMddHHmmss'.'ff", "yyyyMMddHHmmss'.'fff", "MMM dd  yyyy", "MMM  d  yyyy", "MMM dd HH:mm", "MMM  d HH:mm" };

                        DateTime.TryParseExact(fileLine, FtpDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime ftpFileTime);


                        fileDateTime = new DateTime(dateYear, dateMonth, dateDay, dateHour, dateMin, 0);
                    }
                    catch
                    {
                        //try other formats
                        fileDateTime = DateTime.Now;
                    }


                    string fileName = line.Substring(line.LastIndexOf(" ") + 1);
                    fileList.Add(new Tuple<string, DateTime>(fileName, fileDateTime));
                }
            }

            return fileList;
        }

        private void DrawMap(decimal selectedX, decimal selectedY)
        {
            if (cm == null || MapViewer == null || MapViewer.IsDisposed)
            {
                return;
            }

            lblStatus.Text = "Updating selections...";
            lblStatus.Refresh();


            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":
                    string wildClass = "";
                    if (cboWildClass.SelectedItem != null)
                    {
                        ASVCreatureSummary selectedValue = (ASVCreatureSummary)cboWildClass.SelectedItem;
                        wildClass = selectedValue.ClassName;
                    }

                    MapViewer.DrawMapImageWild(wildClass, (int)udWildMin.Value, (int)udWildMax.Value, (float)udWildLat.Value, (float)udWildLon.Value, (float)udWildRadius.Value, selectedY, selectedX);

                    break;
                case "tpgTamed":

                    string tameClass = "";
                    if (cboTameClass.SelectedItem != null)
                    {
                        ASVCreatureSummary selectedValue = (ASVCreatureSummary)cboTameClass.SelectedItem;
                        tameClass = selectedValue.ClassName;
                    }

                    long tribeId = 0;
                    if (cboTameTribes.SelectedItem != null)
                    {
                        ASVComboValue selectedTribe = (ASVComboValue)cboTameTribes.SelectedItem;
                        long.TryParse(selectedTribe.Key, out tribeId);
                    }

                    long playerId = 0;
                    if (cboTamePlayers.SelectedItem != null)
                    {
                        ASVComboValue selectedPlayer = (ASVComboValue)cboTamePlayers.SelectedItem;
                        long.TryParse(selectedPlayer.Key, out playerId);
                    }

                    MapViewer.DrawMapImageTamed(tameClass, chkCryo.Checked, tribeId, playerId, selectedY, selectedX);


                    break;
                case "tpgStructures":
                    //map out player structures
                    string structureClass = "";
                    if (cboStructureStructure.SelectedItem != null)
                    {
                        ASVComboValue selectedStructure = (ASVComboValue)cboStructureStructure.SelectedItem;
                        structureClass = selectedStructure.Key;
                    }
                    long structureTribe = 0;
                    if (cboStructureTribe.SelectedItem != null)
                    {
                        ASVComboValue selectedTribe = (ASVComboValue)cboStructureTribe.SelectedItem;
                        long.TryParse(selectedTribe.Key, out structureTribe);
                    }

                    long structurePlayer = 0;
                    if (cboStructurePlayer.SelectedItem != null)
                    {
                        ASVComboValue selectedPlayer = (ASVComboValue)cboStructurePlayer.SelectedItem;
                        long.TryParse(selectedPlayer.Key, out tribeId);
                    }

                    MapViewer.DrawMapImagePlayerStructures(structureClass, structureTribe, structurePlayer, selectedY, selectedX);

                    break;
                case "tpgPlayers":
                    //players
                    long playerTribe = 0;
                    if (cboTribes.SelectedItem != null)
                    {
                        ASVComboValue selectedTribe = (ASVComboValue)cboTribes.SelectedItem;
                        long.TryParse(selectedTribe.Key, out playerTribe);
                    }

                    long currentId = 0;
                    if (cboPlayers.SelectedItem != null)
                    {
                        ASVComboValue selectedPlayer = (ASVComboValue)cboPlayers.SelectedItem;
                        long.TryParse(selectedPlayer.Key, out currentId);
                    }

                    MapViewer.DrawMapImagePlayers(playerTribe, currentId, selectedY, selectedX);

                    break;
                case "tpgDroppedItems":

                    long droppedPlayerId = 0;
                    if (cboDroppedPlayer.SelectedItem != null)
                    {
                        ASVComboValue selectedPlayer = (ASVComboValue)cboDroppedPlayer.SelectedItem;
                        long.TryParse(selectedPlayer.Key, out droppedPlayerId);
                    }
                    string droppedClass = "";
                    if (cboDroppedItem.SelectedItem != null)
                    {
                        ASVComboValue droppedValue = (ASVComboValue)cboDroppedItem.SelectedItem;
                        droppedClass = droppedValue.Key;
                    }

                    if (droppedClass == "-1")
                    {

                        MapViewer.DrawMapImageDropBags(droppedPlayerId, selectedY, selectedX);
                    }
                    else
                    {
                        if (droppedClass == "0") droppedClass = "";

                        MapViewer.DrawMapImageDroppedItems(droppedPlayerId, droppedClass, selectedY, selectedX);
                    }


                    break;
                case "tpgTribes":
                    long summaryTribeId = 0;

                    if (lvwTribes.SelectedItems.Count > 0)
                    {
                        ListViewItem selectedItem = lvwTribes.SelectedItems[0];
                        ContentTribe selectedTribe = (ContentTribe)selectedItem.Tag;
                        summaryTribeId = selectedTribe.TribeId;
                    }
                    MapViewer.DrawMapImageTribes(summaryTribeId, chkTribeStructures.Checked, chkTribePlayers.Checked, chkTribeTames.Checked, selectedY, selectedX);

                    break;
                case "tpgItemList":

                    long itemTribeId = 0;
                    if (cboItemListTribe.SelectedItem != null)
                    {
                        ASVComboValue selectedTribe = (ASVComboValue)cboItemListTribe.SelectedItem;
                        long.TryParse(selectedTribe.Key, out itemTribeId);
                    }

                    string itemClass = "";
                    if (cboItemListItem.SelectedItem != null)
                    {
                        ASVComboValue itemValue = (ASVComboValue)cboItemListItem.SelectedItem;
                        itemClass = itemValue.Key;
                    }

                    MapViewer.DrawMapImageItems(itemTribeId, itemClass, selectedY, selectedX);

                    break;

                default:

                    break;
            }

            lblStatus.Text = "Map display updated.";
            lblStatus.Refresh();

        }


        private void LoadDroppedItemDetail()
        {
            if (cm == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating dropped item data.";
            lblStatus.Refresh();

            lvwDroppedItems.BeginUpdate();
            lvwDroppedItems.Items.Clear();

            //player
            ASVComboValue comboValue = (ASVComboValue)cboDroppedPlayer.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);

            string selectedClass = "NONE";
            comboValue = (ASVComboValue)cboDroppedItem.SelectedItem;
            selectedClass = comboValue.Key;

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();


            //tribe name
            string tribeName = "n/a";
            string playerName = "n/a";
            if (selectedPlayerId > 0)
            {
                var tribe = cm.GetPlayerTribe(selectedPlayerId);
                tribeName = tribe.TribeName;
                playerName = tribe.Players.First(p => p.Id == selectedPlayerId).CharacterName;
            }

            if (selectedClass == "DeathItemCache_PlayerDeath_C" || selectedClass == "-1")
            {

                var deadBags = cm.GetDeathCacheBags(selectedPlayerId);
                Parallel.ForEach(deadBags, playerCache =>
                {
                    string itemName = "Player Cache";

                    //get tribe/player
                    var playerTribe = cm.GetPlayerTribe(playerCache.DroppedByPlayerId);
                    if (playerTribe != null)
                    {
                        var player = playerTribe.Players.First(p => p.Id == playerCache.DroppedByPlayerId);
                        tribeName = playerTribe.TribeName;
                        playerName = player.CharacterName;
                    }

                    ListViewItem newItem = new ListViewItem(itemName);
                    newItem.Tag = playerCache;
                    newItem.SubItems.Add(playerCache.DroppedByName);
                    newItem.SubItems.Add((playerCache.Latitude.GetValueOrDefault(0) == 0 && playerCache.Longitude.GetValueOrDefault(0) == 0) ? "n/a" : playerCache.Latitude.Value.ToString("0.00"));
                    newItem.SubItems.Add((playerCache.Latitude.GetValueOrDefault(0) == 0 && playerCache.Longitude.GetValueOrDefault(0) == 0) ? "n/a" : playerCache.Longitude.Value.ToString("0.00"));
                    newItem.SubItems.Add(tribeName);
                    newItem.SubItems.Add(playerName);

                    listItems.Add(newItem);

                });

            }
            else
            {

                var droppedItems = cm.GetDroppedItems(selectedPlayerId, selectedClass == "0" ? "" : selectedClass).ToList();

                if (droppedItems != null)
                {

                    Parallel.ForEach(droppedItems, droppedItem =>
                    {
                        string itemName = droppedItem.ClassName;
                        ItemClassMap itemMap = Program.ProgramConfig.ItemMap.Where(m => m.ClassName == droppedItem.ClassName).FirstOrDefault();
                        if (itemMap != null)
                        {
                            itemName = itemMap.DisplayName;
                        }

                        //get tribe/player
                        var playerTribe = cm.GetPlayerTribe(droppedItem.DroppedByPlayerId);
                        if (playerTribe != null)
                        {
                            var player = playerTribe.Players.First(p => p.Id == droppedItem.DroppedByPlayerId);
                            tribeName = playerTribe.TribeName;
                            playerName = player.CharacterName;
                        }

                        ListViewItem newItem = new ListViewItem(itemName);
                        newItem.Tag = droppedItem;
                        newItem.SubItems.Add(droppedItem.DroppedByName);
                        newItem.SubItems.Add((droppedItem.Latitude.GetValueOrDefault(0) == 0 && droppedItem.Longitude.GetValueOrDefault(0) == 0) ? "n/a" : droppedItem.Latitude.Value.ToString("0.00"));
                        newItem.SubItems.Add((droppedItem.Latitude.GetValueOrDefault(0) == 0 && droppedItem.Longitude.GetValueOrDefault(0) == 0) ? "n/a" : droppedItem.Longitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(tribeName);
                        newItem.SubItems.Add(playerName);

                        listItems.Add(newItem);

                    });

                }

            }

            lvwDroppedItems.Items.AddRange(listItems.ToArray());


            if (SortingColumn_Drops != null)
            {
                lvwDroppedItems.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_Drops.Index, SortingColumn_Drops.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwDroppedItems.Sort();
            }

            lvwDroppedItems.EndUpdate();
            lblStatus.Text = "Dropped item data populated.";
            lblStatus.Refresh();

            lblCountDropped.Text = $"Count: {lvwDroppedItems.Items.Count}";

            if (tabFeatures.SelectedTab.Name == "tpgDroppedItems")
            {
                DrawMap(0, 0);
            }


            this.Cursor = Cursors.Default;
        }


        private void LoadTameDetail()
        {
            if (cm == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating tame data.";
            lblStatus.Refresh();

            decimal selectedX = 0.0m;
            decimal selectedY = 0.0m;

            if (cboTameClass.SelectedItem != null)
            {
                ASVCreatureSummary selectedSummary = (ASVCreatureSummary)cboTameClass.SelectedItem;

                long selectedId = 0;
                if (lvwTameDetail.SelectedItems.Count > 0)
                {
                    long.TryParse(lvwTameDetail.SelectedItems[0].Tag.ToString(), out selectedId);
                }
                lvwTameDetail.BeginUpdate();
                lvwTameDetail.Items.Clear();

                string className = selectedSummary.ClassName;

                //tribe
                int selectedTribeId = 0;
                int selectedPlayerId = 0;

                if (cboTameTribes.SelectedItem != null)
                {
                    ASVComboValue comboValue = (ASVComboValue)cboTameTribes.SelectedItem;
                    int.TryParse(comboValue.Key, out selectedTribeId);

                }

                //player
                if (cboTamePlayers.SelectedItem != null)
                {
                    ASVComboValue comboValue = (ASVComboValue)cboTamePlayers.SelectedItem;
                    int.TryParse(comboValue.Key, out selectedPlayerId);
                }

                var tribes = cm.GetTribes(selectedTribeId);
                var detailList = tribes.SelectMany(t => t.Tames
                    .Where(x =>
                        (x.ClassName == className || className == "")
                        & !(x.ClassName == "MotorRaft_BP_C" || x.ClassName == "Raft_BP_C")
                        && (chkCryo.Checked || x.IsCryo == false)
                        && (chkCryo.Checked || x.IsVivarium == false)
                    )).ToList();

                //change into a strongly typed list for use in parallel
                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(detailList, detail =>
                {
                    var dinoMap = ARKViewer.Program.ProgramConfig.DinoMap.Where(dino => dino.ClassName == detail.ClassName).FirstOrDefault();

                    string creatureClassName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;
                    string creatureName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;

                    if (detail.Name != null)
                    {
                        creatureName = detail.Name;
                    }

                    if (creatureName.ToLower().Contains("queen"))
                    {
                        detail.Gender = "Female";
                    }

                    ListViewItem item = new ListViewItem(creatureClassName);
                    item.Tag = detail;
                    item.UseItemStyleForSubItems = false;

                    item.SubItems.Add(creatureName);
                    item.SubItems.Add(detail.Gender.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(detail.Level.ToString());
                    item.SubItems.Add(((decimal)detail.Latitude).ToString("0.00"));
                    item.SubItems.Add(((decimal)detail.Longitude).ToString("0.00"));
                    if (optStatsTamed.Checked)
                    {
                        item.SubItems.Add(detail.TamedStats[0].ToString());
                        item.SubItems.Add(detail.TamedStats[1].ToString());
                        item.SubItems.Add(detail.TamedStats[8].ToString());
                        item.SubItems.Add(detail.TamedStats[7].ToString());
                        item.SubItems.Add(detail.TamedStats[9].ToString());
                        item.SubItems.Add(detail.TamedStats[4].ToString());
                        item.SubItems.Add(detail.TamedStats[3].ToString());
                        item.SubItems.Add(detail.TamedStats[11].ToString());

                    }
                    else
                    {
                        item.SubItems.Add(detail.BaseStats[0].ToString());
                        item.SubItems.Add(detail.BaseStats[1].ToString());
                        item.SubItems.Add(detail.BaseStats[8].ToString());
                        item.SubItems.Add(detail.BaseStats[7].ToString());
                        item.SubItems.Add(detail.BaseStats[9].ToString());
                        item.SubItems.Add(detail.BaseStats[4].ToString());
                        item.SubItems.Add(detail.BaseStats[3].ToString());
                        item.SubItems.Add(detail.BaseStats[11].ToString());

                    }

                    item.SubItems.Add(detail.TamedOnServerName);

                    string tamerName = detail.TamerName != null ? detail.TamerName : "";
                    string imprinterName = detail.ImprinterName;
                    if (tamerName.Length == 0)
                    {
                        if (detail.ImprintedPlayerId != 0)
                        {
                            //var tamer = cm.GetPlayers(0, detail.ImprintedPlayerId).FirstOrDefault<ContentPlayer>();
                            //if(tamer!=null) tamerName = tamer.CharacterName;
                        }
                        else
                        {
                            tamerName = detail.TribeName;
                        }
                    }



                    item.SubItems.Add(tamerName);
                    item.SubItems.Add(detail.ImprinterName);
                    item.SubItems.Add((detail.ImprintQuality * 100).ToString("f0"));

                    bool isStored = detail.IsCryo | detail.IsVivarium;

                    item.SubItems.Add(isStored.ToString());

                    if (detail.IsCryo)
                    {
                        item.BackColor = Color.LightSkyBlue;
                        item.SubItems[1].BackColor = Color.LightSkyBlue;
                        item.SubItems[2].BackColor = Color.LightSkyBlue;
                        item.SubItems[3].BackColor = Color.LightSkyBlue;
                        item.SubItems[4].BackColor = Color.LightSkyBlue;
                        item.SubItems[5].BackColor = Color.LightSkyBlue;
                        item.SubItems[6].BackColor = Color.LightSkyBlue;
                        item.SubItems[7].BackColor = Color.LightSkyBlue;
                        item.SubItems[8].BackColor = Color.LightSkyBlue;
                        item.SubItems[9].BackColor = Color.LightSkyBlue;
                        item.SubItems[10].BackColor = Color.LightSkyBlue;
                        item.SubItems[11].BackColor = Color.LightSkyBlue;
                        item.SubItems[12].BackColor = Color.LightSkyBlue;
                        item.SubItems[13].BackColor = Color.LightSkyBlue;
                        item.SubItems[14].BackColor = Color.LightSkyBlue;
                        item.SubItems[15].BackColor = Color.LightSkyBlue;
                        item.SubItems[16].BackColor = Color.LightSkyBlue;
                        item.SubItems[17].BackColor = Color.LightSkyBlue;
                        item.SubItems[18].BackColor = Color.LightSkyBlue;
                    }
                    else if (detail.IsVivarium)
                    {
                        item.BackColor = Color.LightGreen;
                        item.SubItems[1].BackColor = Color.LightGreen;
                        item.SubItems[2].BackColor = Color.LightGreen;
                        item.SubItems[3].BackColor = Color.LightGreen;
                        item.SubItems[4].BackColor = Color.LightGreen;
                        item.SubItems[5].BackColor = Color.LightGreen;
                        item.SubItems[6].BackColor = Color.LightGreen;
                        item.SubItems[7].BackColor = Color.LightGreen;
                        item.SubItems[8].BackColor = Color.LightGreen;
                        item.SubItems[9].BackColor = Color.LightGreen;
                        item.SubItems[10].BackColor = Color.LightGreen;
                        item.SubItems[11].BackColor = Color.LightGreen;
                        item.SubItems[12].BackColor = Color.LightGreen;
                        item.SubItems[13].BackColor = Color.LightGreen;
                        item.SubItems[14].BackColor = Color.LightGreen;
                        item.SubItems[15].BackColor = Color.LightGreen;
                        item.SubItems[16].BackColor = Color.LightGreen;
                        item.SubItems[17].BackColor = Color.LightGreen;
                        item.SubItems[18].BackColor = Color.LightGreen;
                    }


                    //Colours
                    int colourCheck = (int)detail.Colors[0];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[0].ToString()); //14
                    ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[0]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[20].BackColor = selectedColor.Color;
                        item.SubItems[20].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[1];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[1].ToString()); //15
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[1]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[21].BackColor = selectedColor.Color;
                        item.SubItems[21].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[2];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[2].ToString()); //16
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[2]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[22].BackColor = selectedColor.Color;
                        item.SubItems[22].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[3];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[3].ToString()); //17
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[3]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[23].BackColor = selectedColor.Color;
                        item.SubItems[23].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[4];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[4].ToString()); //18
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[4]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[24].BackColor = selectedColor.Color;
                        item.SubItems[24].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[5];
                    item.SubItems.Add(colourCheck == 0 ? "n/a" : detail.Colors[5].ToString()); //19
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[5]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[25].BackColor = selectedColor.Color;
                        item.SubItems[25].ForeColor = selectedColor.Color;
                    }


                    //mutations
                    item.SubItems.Add(detail.RandomMutationsFemale.ToString());
                    item.SubItems.Add(detail.RandomMutationsMale.ToString());

                    item.SubItems.Add(detail.Id.ToString());


                    if (detail.Id == selectedId)
                    {

                        item.Selected = true;
                        selectedX = (decimal)detail.Longitude;
                        selectedY = (decimal)detail.Latitude;
                    }

                    listItems.Add(item);
                });

                lvwTameDetail.Items.AddRange(listItems.ToArray());

                if (SortingColumn_DetailTame != null)
                {
                    lvwTameDetail.ListViewItemSorter =
                        new ListViewComparer(SortingColumn_DetailTame.Index, SortingColumn_DetailTame.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                    // Sort.
                    lvwTameDetail.Sort();
                }
                else
                {

                    SortingColumn_DetailTame = lvwTameDetail.Columns[0]; ;
                    SortingColumn_DetailTame.Text = "> " + SortingColumn_DetailTame.Text;

                    lvwTameDetail.ListViewItemSorter =
                        new ListViewComparer(0, SortOrder.Ascending);

                    // Sort.
                    lvwTameDetail.Sort();
                }


                lvwTameDetail.EndUpdate();

                lblStatus.Text = "Tame data populated.";
                lblStatus.Refresh();
                lblTameTotal.Text = $"Count: {lvwTameDetail.Items.Count}";


                /*
                Bitmap originalImage = DrawMap(selectedX, selectedY);

                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                */


                if (tabFeatures.SelectedTab.Name == "tpgTamed")
                {
                    DrawMap(selectedX, selectedY);
                }


            }



            this.Cursor = Cursors.Default;

        }

        private void LoadWildDetail()
        {
            if (cm == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating creature data.";
            lblStatus.Refresh();

            decimal selectedX = 0.0m;
            decimal selectedY = 0.0m;

            if (cboWildClass.SelectedItem != null)
            {
                ASVCreatureSummary selectedSummary = (ASVCreatureSummary)cboWildClass.SelectedItem;

                long selectedId = 0;
                if (lvwWildDetail.SelectedItems.Count > 0)
                {
                    long.TryParse(lvwWildDetail.SelectedItems[0].Tag.ToString(), out selectedId);
                }
                lvwWildDetail.BeginUpdate();
                lvwWildDetail.Items.Clear();

                string className = selectedSummary.ClassName;

                int minLevel = (int)udWildMin.Value;
                int maxLevel = (int)udWildMax.Value;
                float selectedLat = (float)udWildLat.Value;
                float selectedLon = (float)udWildLon.Value;
                float selectedRad = (float)udWildRadius.Value;

                var detailList = cm.GetWildCreatures(minLevel, maxLevel, selectedLat, selectedLon, selectedRad, className).OrderByDescending(c => c.BaseLevel).ToList();
                if (cboWildResource.SelectedIndex != 0)
                {
                    //limit by resource production
                    ASVComboValue selectedResourceValue = (ASVComboValue)cboWildResource.SelectedItem;
                    string selectedResourceClass = selectedResourceValue.Key;
                    detailList.RemoveAll(d => d.ProductionResources == null || !d.ProductionResources.Any(r => r == selectedResourceClass));
                }

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

                Parallel.ForEach(detailList, detail =>
                {
                    var dinoMap = ARKViewer.Program.ProgramConfig.DinoMap.Where(dino => dino.ClassName == detail.ClassName).FirstOrDefault();
                    string creatureName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;
                    ListViewItem item = new ListViewItem(creatureName);//lvwWildDetail.Items.Add(creatureName);
                    item.Tag = detail;
                    item.UseItemStyleForSubItems = false;

                    if (creatureName.ToLower().Contains("queen"))
                    {
                        detail.Gender = "Female";
                    }


                    item.SubItems.Add(detail.Gender.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(((decimal)detail.Latitude).ToString("0.00"));
                    item.SubItems.Add(((decimal)detail.Longitude).ToString("0.00"));

                    item.SubItems.Add(detail.BaseStats[0].ToString());
                    item.SubItems.Add(detail.BaseStats[1].ToString());
                    item.SubItems.Add(detail.BaseStats[8].ToString());
                    item.SubItems.Add(detail.BaseStats[7].ToString());
                    item.SubItems.Add(detail.BaseStats[9].ToString());
                    item.SubItems.Add(detail.BaseStats[4].ToString());
                    item.SubItems.Add(detail.BaseStats[3].ToString());
                    item.SubItems.Add(detail.BaseStats[11].ToString());



                    int colourCheck = (int)detail.Colors[0];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[0].ToString()); //14
                    ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[0]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[14].BackColor = selectedColor.Color;
                        item.SubItems[14].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[1];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[1].ToString()); //15
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[1]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[15].BackColor = selectedColor.Color;
                        item.SubItems[15].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[2];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[2].ToString()); //16
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[2]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[16].BackColor = selectedColor.Color;
                        item.SubItems[16].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[3];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[3].ToString()); //17
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[3]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[17].BackColor = selectedColor.Color;
                        item.SubItems[17].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[4];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[4].ToString()); //18
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[4]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[18].BackColor = selectedColor.Color;
                        item.SubItems[18].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[5];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[5].ToString()); //19
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[5]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[19].BackColor = selectedColor.Color;
                        item.SubItems[19].ForeColor = selectedColor.Color;
                    }

                    item.SubItems.Add(detail.Id.ToString());

                    if (detail.Id == selectedId)
                    {

                        item.Selected = true;
                        selectedX = (decimal)Math.Round(detail.Longitude.Value, 2);
                        selectedY = (decimal)Math.Round(detail.Latitude.Value, 2);
                    }

                    listItems.Add(item);
                });

                lvwWildDetail.Items.AddRange(listItems.ToArray());

                // Create a comparer.
                if (SortingColumn_DetailWild != null)
                {
                    lvwWildDetail.ListViewItemSorter =
                        new ListViewComparer(SortingColumn_DetailWild.Index, SortingColumn_DetailWild.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                    // Sort.
                    lvwWildDetail.Sort();
                }
                else
                {

                    SortingColumn_DetailWild = lvwWildDetail.Columns[3]; ;
                    SortingColumn_DetailWild.Text = "< " + SortingColumn_DetailWild.Text;

                    lvwWildDetail.ListViewItemSorter =
                        new ListViewComparer(3, SortOrder.Descending);

                    // Sort.
                    lvwWildDetail.Sort();

                }

                lvwWildDetail.EndUpdate();

                lblSelectedWildTotal.Text = "Count: " + lvwWildDetail.Items.Count.ToString();

                lblStatus.Text = "Creature data populated.";
                lblStatus.Refresh();



                /*
                Bitmap originalImage = DrawMap(selectedX, selectedY);

                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                */

                if (tabFeatures.SelectedTab.Name == "tpgWild")
                {
                    DrawMap(selectedX, selectedY);

                }


            }

            this.Cursor = Cursors.Default;

        }

        private void LvwWildDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            btnCopyCommandWild.Enabled = lvwWildDetail.SelectedItems.Count > 0;

            if (lvwWildDetail.SelectedItems.Count > 0)
            {

                var selectedItem = lvwWildDetail.SelectedItems[0];
                decimal.TryParse(selectedItem.SubItems[5].Text, out decimal selectedX);
                decimal.TryParse(selectedItem.SubItems[4].Text, out decimal selectedY);


                DrawMap(selectedX, selectedY);



            }
            this.Cursor = Cursors.Default;
        }



        private void RefreshTamedSummary()
        {

            if (cm == null)
            {
                return;
            }


            lblStatus.Text = "Populating tamed creature summary...";
            lblStatus.Refresh();



            int classIndex = 0;
            string selectedClass = "";
            if (cboTameClass.SelectedItem != null)
            {
                ASVCreatureSummary selectedDino = (ASVCreatureSummary)cboTameClass.SelectedItem;
                selectedClass = selectedDino.ClassName;
            }


            List<int> playerRestrictions = new List<int>();
            List<string> tribeRestrictions = new List<string>();

            if (ARKViewer.Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp)
            {
                //check for server restritions
                ServerConfiguration currentConfig = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault<ServerConfiguration>();
                if (currentConfig != null)
                {
                    if (currentConfig.RestrictedTribes != null)
                    {
                        tribeRestrictions.AddRange(currentConfig.RestrictedTribes);
                    }

                    if (currentConfig.RestrictedPlayers != null)
                    {
                        playerRestrictions.AddRange(currentConfig.RestrictedPlayers);
                    }
                }
            }


            //MessageBox.Show("Listing tamed creatures.");
            var tamedSummary = cm.GetTamedCreatures("", 0, 0, chkCryo.Checked)
                                .Where(t => !(t.ClassName == "MotorRaft_BP_C" || t.ClassName == "Raft_BP_C"))
                                .GroupBy(c => c.ClassName)
                                .Select(g => new { ClassName = g.Key, Name = ARKViewer.Program.ProgramConfig.DinoMap.Count(d => d.ClassName == g.Key) == 0 ? g.Key : ARKViewer.Program.ProgramConfig.DinoMap.Where(d => d.ClassName == g.Key).FirstOrDefault().FriendlyName, Count = g.Count(), Min = g.Min(l => l.Level), Max = g.Max(l => l.Level) })
                                .OrderBy(o => o.Name);

            cboTameClass.Items.Clear();
            if (tamedSummary != null && tamedSummary.Count() > 0)
            {
                cboTameClass.Items.Add(new ASVCreatureSummary() { ClassName = "", Name = "[All Creatures]", Count = tamedSummary.Sum(s => s.Count) });

                foreach (var summary in tamedSummary)
                {
                    ASVCreatureSummary newSummary = new ASVCreatureSummary()
                    {
                        ClassName = summary.ClassName,
                        Name = summary.Name,
                        Count = summary.Count,
                        MinLevel = summary.Min,
                        MaxLevel = summary.Max,
                        MaxLength = 100
                    };
                    int newIndex = cboTameClass.Items.Add(newSummary);
                    if (selectedClass == summary.ClassName)
                    {
                        classIndex = newIndex;
                    }
                }

            }


            lblTameTotal.Text = "Count: 0";

            if (cboTameClass.Items.Count > 0) cboTameClass.SelectedIndex = classIndex;

            lblStatus.Text = "Tamed creatures populated.";
            lblStatus.Refresh();

        }


        private void RefreshCryoSummary()
        {

        }


        private void RefreshWildSummary()
        {

            if (cm == null)
            {
                return;
            }

            lblStatus.Text = "Populating wild creature summary...";
            lblStatus.Refresh();


            int classIndex = 0;
            string selectedClass = "";
            if (cboWildClass.SelectedItem != null)
            {
                ASVCreatureSummary selectedDino = (ASVCreatureSummary)cboWildClass.SelectedItem;
                selectedClass = selectedDino.ClassName;
            }


            //wild side
            int minLevel = (int)udWildMin.Value;
            int maxLevel = (int)udWildMax.Value;
            float selectedLat = (float)udWildLat.Value;
            float selectedLon = (float)udWildLon.Value;
            float selectedRad = (float)udWildRadius.Value;

            var wildDinos = cm.GetWildCreatures(minLevel, maxLevel, selectedLat, selectedLon, selectedRad, "");

            cboWildClass.Items.Clear();
            int newIndex = 0;

            //add NONE
            ASVCreatureSummary noneSummary = new ASVCreatureSummary()
            {
                ClassName = "-1",
                Name = "[Please Select]",
                Count = 0,
                MinLevel = 0,
                MaxLevel = 0,
                MaxLength = 100
            };

            cboWildResource.Items.Clear();
            cboWildResource.Items.Add(new ASVComboValue("", "[Any Resource]"));
            cboWildResource.SelectedIndex = 0;

            if (wildDinos != null)
            {
                List<ASVComboValue> productionComboValues = new List<ASVComboValue>();

                var productionResources = wildDinos.Where(x => x.ProductionResources != null).SelectMany(d => d.ProductionResources).Distinct().ToList();
                if (productionResources != null && productionResources.Count > 0)
                {
                    foreach (var resourceClass in productionResources)
                    {
                        string displayName = resourceClass;
                        var itemMap = Program.ProgramConfig.ItemMap.FirstOrDefault(i => i.ClassName == resourceClass);
                        if (itemMap != null && itemMap.DisplayName.Length > 0) displayName = itemMap.DisplayName;

                        productionComboValues.Add(new ASVComboValue(resourceClass, displayName));
                    }
                }

                if (productionComboValues != null && productionComboValues.Count > 0)
                {
                    cboWildResource.Items.AddRange(productionComboValues.OrderBy(o => o.Value).ToArray());
                }

                int summaryCount = 0;
                int summaryMin = 0;
                int summaryMax = 150;

                var wildSummary = wildDinos
                                .GroupBy(c => c.ClassName)
                                .Select(g => new { ClassName = g.Key, Name = ARKViewer.Program.ProgramConfig.DinoMap.Count(d => d.ClassName == g.Key) == 0 ? g.Key : ARKViewer.Program.ProgramConfig.DinoMap.Where(d => d.ClassName == g.Key).FirstOrDefault().FriendlyName, Count = g.Count(), Min = g.Min(l => l.BaseLevel), Max = g.Max(l => l.BaseLevel) })
                                .OrderBy(o => o.Name);

                if (wildSummary != null && wildSummary.LongCount() > 0)
                {
                    summaryCount = wildSummary.Sum(s => s.Count);
                    summaryMin = wildSummary.Min(s => s.Min);
                    summaryMax = wildSummary.Max(s => s.Max);
                }

                noneSummary.Count = summaryCount;
                noneSummary.MinLevel = summaryMin;
                noneSummary.MaxLevel = summaryMax;
                newIndex = cboWildClass.Items.Add(noneSummary);

                //add "All" summary
                int minLevelDefault = summaryMin;
                int maxLevelDefault = summaryMax;

                ASVCreatureSummary allSummary = new ASVCreatureSummary()
                {
                    ClassName = "",
                    Name = "[All Creatures]",
                    Count = wildSummary.Sum(s => s.Count),
                    MinLevel = minLevelDefault,
                    MaxLevel = maxLevelDefault,
                    MaxLength = 100
                };

                newIndex = cboWildClass.Items.Add(allSummary);


                foreach (var summary in wildSummary.OrderBy(o => o.Name))
                {

                    ASVCreatureSummary newSummary = new ASVCreatureSummary()
                    {
                        ClassName = summary.ClassName,
                        Name = summary.Name,
                        Count = summary.Count,
                        MinLevel = summary.Min,
                        MaxLevel = summary.Max,
                        MaxLength = 100
                    };


                    newIndex = cboWildClass.Items.Add(newSummary);
                    if (selectedClass == summary.ClassName)
                    {
                        classIndex = newIndex;
                    }
                }

                lblWildTotal.Text = "Total: " + wildSummary.Sum(w => w.Count).ToString();
            }


            lblStatus.Text = "Wild creatures populated.";
            lblStatus.Refresh();


            if (cboWildClass.Items.Count > 0) cboWildClass.SelectedIndex = classIndex;

        }


        private void CboWildClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWildClass.SelectedIndex != 1 && cboWildResource.SelectedIndex > 0) cboWildResource.SelectedIndex = 0;
            LoadWildDetail();
        }

        private void lvwWildDetail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwWildDetail.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_DetailWild == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_DetailWild)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_DetailWild.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_DetailWild.Text = SortingColumn_DetailWild.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_DetailWild = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_DetailWild.Text = "> " + SortingColumn_DetailWild.Text;
            }
            else
            {
                SortingColumn_DetailWild.Text = "< " + SortingColumn_DetailWild.Text;
            }

            // Create a comparer.
            lvwWildDetail.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwWildDetail.Sort();
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {

            frmSettings settings = new frmSettings(cm);
            settings.Owner = this;
            while (settings.ShowDialog() == DialogResult.OK)
            {
                settings.SavedConfig.Save();
                ARKViewer.Program.ProgramConfig = settings.SavedConfig;

                
                if (!File.Exists(settings.SavedConfig.SelectedFile))
                {
                    if (settings.SavedConfig.Mode == ViewerModes.Mode_Ftp)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        settings.SavedConfig.SelectedFile = Download();

                        this.Cursor = Cursors.Default;

                    }
                }

                if (File.Exists(settings.SavedConfig.SelectedFile))
                {
                    this.Cursor = Cursors.WaitCursor;

                    UpdateProgress("Loading content pack..");

                    LoadContent(settings.SavedConfig.SelectedFile);

                    this.Cursor = Cursors.Default;

                    break;
                }
            }

        }


        /**** FTP Server ****/
        public string Download()
        {
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return "";

            switch (selectedServer.Mode)
            {
                case 0:
                    //ftp
                    return DownloadFtp();

                case 1:
                    //sftp
                    return DownloadSFtp();

            }

            return "";

        }

        private string DownloadSFtp()
        {
            string downloadFilename = "";
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return downloadFilename;

            string ftpServerUrl = $"{selectedServer.Address}";
            string serverUsername = selectedServer.Username;
            string serverPassword = selectedServer.Password;
            string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            if (Program.ProgramConfig.FtpDownloadMode == 1)
            {
                //clear any previous .arktribe, .arkprofile files
                var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                foreach (var profileFile in profileFiles)
                {
                    File.Delete(profileFile);
                }

                var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                foreach (var tribeFile in tribeFiles)
                {
                    File.Delete(tribeFile);
                }

            }

            string mapFilename = ARKViewer.Program.ProgramConfig.SelectedFile;

            try
            {
                using (var sftp = new SftpClient(selectedServer.Address, selectedServer.Port, selectedServer.Username, selectedServer.Password))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(selectedServer.SaveGamePath).Where(f => f.IsRegularFile);
                    foreach (var serverFile in files)
                    {


                        if (Path.GetExtension(serverFile.Name).StartsWith(".ark"))
                        {

                            string localFilename = Path.Combine(downloadPath, serverFile.Name);


                            if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 1)
                            {
                                File.Delete(localFilename);
                            }

                            bool shouldDownload = true;

                            if (serverFile.Name.EndsWith(".ark"))
                            {
                                downloadFilename = localFilename;

                                if (serverFile.Name.ToLower() != selectedServer.Map.ToLower())
                                {
                                    shouldDownload = false;
                                }
                                else
                                {
                                    if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                    {
                                        shouldDownload = false;
                                    }
                                }
                            }
                            else
                            {
                                if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                {
                                    shouldDownload = false;
                                }
                            }

                            if (shouldDownload)
                            {
                                //delete local if any
                                if (File.Exists(localFilename))
                                {
                                    File.Delete(localFilename);
                                }

                                using (FileStream outputStream = new FileStream(localFilename, FileMode.CreateNew))
                                {
                                    sftp.DownloadFile(serverFile.FullName, outputStream);
                                    outputStream.Flush();
                                }
                                DateTime saveTime = serverFile.LastWriteTimeUtc;
                                File.SetLastWriteTimeUtc(localFilename, saveTime);

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to download latest game data.\n\n{ex.Message.ToString()}", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            return downloadFilename;
        }

        private string DownloadFtp()
        {
            string downloadedFilename = "";
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return downloadedFilename;

            selectedServer.Address = selectedServer.Address.Trim();
            selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim();
            if (!selectedServer.SaveGamePath.EndsWith("/"))
            {
                selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim() + "/";
            }


            using (FtpClient ftpClient = new FtpClient(selectedServer.Address))
            {

                ftpClient.Credentials.UserName = selectedServer.Username;
                ftpClient.Credentials.Password = selectedServer.Password;
                ftpClient.Port = selectedServer.Port;

                string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }


                // try remove existing local copies                

                if (Program.ProgramConfig.FtpDownloadMode == 1)
                {
                    //clean download
                    // ... arkprofile(s)
                    var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                    foreach (var profileFilename in profileFiles)
                    {
                        try
                        {
                            File.Delete(profileFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }

                    // ... arktribe(s)
                    var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                    foreach (var tribeFilename in tribeFiles)
                    {
                        try
                        {
                            File.Delete(tribeFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }
                }

                //try catch

                try
                {
                    ftpClient.Connect();
                    var serverFiles = ftpClient.GetListing(selectedServer.SaveGamePath);
                    string localFilename = "";

                    //get correct casing for the selected map file
                    var serverSaveFile = serverFiles.Where(f => f.Name.ToLower() == selectedServer.Map.ToLower()).FirstOrDefault();
                    if (serverSaveFile != null)
                    {
                        localFilename = Path.Combine(downloadPath, serverSaveFile.Name);
                        downloadedFilename = localFilename;
                        bool shouldDownload = true;


                        if (File.Exists(localFilename) && serverSaveFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                        {
                            if (Program.ProgramConfig.FtpDownloadMode == 0)
                            {
                                shouldDownload = false;
                            }

                        }

                        if (shouldDownload)
                        {
                            using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                            {
                                ftpClient.Download(outputStream, serverSaveFile.FullName);
                                outputStream.Flush();
                            }
                            File.SetLastWriteTimeUtc(localFilename, serverSaveFile.Modified.ToUniversalTime());
                        }



                        //get .arktribe files
                        var serverTribeFiles = serverFiles.Where(f => f.Name.EndsWith(".arktribe"));
                        if (serverTribeFiles != null && serverTribeFiles.Count() > 0)
                        {
                            foreach (var serverTribeFile in serverTribeFiles)
                            {
                                localFilename = Path.Combine(downloadPath, serverTribeFile.Name);
                                shouldDownload = true;
                                if (File.Exists(localFilename) && serverTribeFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                                {
                                    if (Program.ProgramConfig.FtpDownloadMode == 0)
                                    {
                                        shouldDownload = false;
                                    }

                                }


                                if (shouldDownload)
                                {
                                    using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                    {
                                        ftpClient.Download(outputStream, serverTribeFile.FullName);
                                        outputStream.Flush();
                                    }
                                    File.SetLastWriteTimeUtc(localFilename, serverTribeFile.Modified.ToUniversalTime());
                                }

                            }

                        }


                        //get .arkprofile files
                        var serverProfileFiles = serverFiles.Where(f => f.Name.EndsWith(".arkprofile"));
                        if (serverProfileFiles != null && serverProfileFiles.Count() > 0)
                        {
                            foreach (var serverProfileFile in serverProfileFiles)
                            {

                                localFilename = Path.Combine(downloadPath, serverProfileFile.Name);
                                shouldDownload = true;
                                if (File.Exists(localFilename) && serverProfileFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                                {
                                    if (Program.ProgramConfig.FtpDownloadMode == 0)
                                    {
                                        shouldDownload = false;
                                    }

                                }
                                if (shouldDownload)
                                {
                                    using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                    {
                                        ftpClient.Download(outputStream, serverProfileFile.FullName);
                                        outputStream.Flush();
                                    }
                                    File.SetLastWriteTimeUtc(localFilename, serverProfileFile.Modified.ToUniversalTime());
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to download latest game data.\n\n{ex.Message.ToString()}", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return downloadedFilename;

        }

        private void cboSelected_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            ASVCreatureSummary dinoSummary = (ASVCreatureSummary)cboWildClass.Items[e.Index];
            string dinoName = dinoSummary.Name;
            string dinoCount = $"Count: {dinoSummary.Count}";
            string minLevel = $"Min: {dinoSummary.MinLevel}";
            string maxLevel = $"Max: {dinoSummary.MaxLevel}";

            Rectangle r1 = e.Bounds;
            r1.Width = r1.Width;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(dinoName, e.Font, sb, r1);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom)
            // End Using

            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width - 200;
            r2.Width = r2.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(dinoCount, e.Font, sb, r2);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r2.Right, 0, r2.Right, r2.Bottom)
            // End Using

            Rectangle r3 = e.Bounds;
            r3.X = e.Bounds.Width - 120;
            r3.Width = r3.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(minLevel, e.Font, sb, r3);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r3.Right, 0, r3.Right, r3.Bottom)
            // End Using

            Rectangle r4 = e.Bounds;
            r4.X = (int)(e.Bounds.Width - 65);
            r4.Width = r4.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(maxLevel, e.Font, sb, r4);
            }
        }

        private void RefreshMap(bool downloadData = false)
        {
            this.Cursor = Cursors.WaitCursor;
            long downloadStartTicks = 0;
            if (ARKViewer.Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp && downloadData)
            {
                UpdateProgress("Downloading new ftp file data...");

                downloadStartTicks = DateTime.Now.Ticks;
                var downloadedFile = Download();
                long downloadEndTicks = DateTime.Now.Ticks;
                if (File.Exists(downloadedFile))
                {
                    Program.ProgramConfig.SelectedFile = downloadedFile;
                }

                UpdateProgress($"Downloaded from server in {TimeSpan.FromTicks(downloadEndTicks - downloadStartTicks).TotalSeconds.ToString("f1")} seconds. Loading content pack...");
            }
            else
            {
                UpdateProgress($"Loading content pack...");
            }


            long startContentTicks = DateTime.Now.Ticks;
            if (downloadStartTicks != (long)0)
            {
                startContentTicks = downloadStartTicks;
            }

            LoadContent(Program.ProgramConfig.SelectedFile);
            long endContentTicks = DateTime.Now.Ticks;

            if (cm == null || cm.ContentDate == null || cm.ContentDate.Equals(new DateTime()))
            {
                //unable to load pack
                UpdateProgress("Content failed to load.  Please check settings or refresh download to try again.");
            }
            else
            {
                UpdateProgress($"Content loaded and refreshed in {TimeSpan.FromTicks(endContentTicks - startContentTicks).TotalSeconds.ToString("f1")} seconds.");
            }

            this.Cursor = Cursors.Default;
        }

        private void frmViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
            ARKViewer.Program.ProgramConfig.Save();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            RefreshMap(true);
            btnRefresh.Enabled = true;
        }

        private void cboTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPlayerList();
        }

        private void cboPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayerDetail();
        }

        private void lvwPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal selectedX = 0;
            decimal selectedY = 0;


            if (lvwPlayers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwPlayers.SelectedItems[0];
                ContentPlayer selectedPlayer = (ContentPlayer)selectedItem.Tag;
                selectedX = (decimal)selectedPlayer.Longitude.GetValueOrDefault(0);
                selectedY = (decimal)selectedPlayer.Latitude.GetValueOrDefault(0);
            }

            DrawMap(selectedX, selectedY);


        }

        private void lvwPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwPlayers.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Players == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Players)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Players.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Players.Text = SortingColumn_Players.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Players = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Players.Text = "> " + SortingColumn_Players.Text;
            }
            else
            {
                SortingColumn_Players.Text = "< " + SortingColumn_Players.Text;
            }

            // Create a comparer.
            lvwPlayers.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwPlayers.Sort();
        }

        private void btnPlayerTames_Click(object sender, EventArgs e)
        {



        }

        private void btnPlayerInventory_Click(object sender, EventArgs e)
        {
            if (lvwPlayers.SelectedItems.Count == 0) return;


            ContentPlayer selectedPlayer = (ContentPlayer)lvwPlayers.SelectedItems[0].Tag;

            frmPlayerInventoryViewer playerViewer = new frmPlayerInventoryViewer(cm, selectedPlayer);
            playerViewer.Owner = this;
            playerViewer.ShowDialog();

        }

        private void btnDinoAncestors_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Dino ancestor explorer coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListViewItem selectedItem = lvwTameDetail.SelectedItems[0];
            ContentTamedCreature selectedTame = (ContentTamedCreature)selectedItem.Tag;
            using (frmBreedingLines ancestors = new frmBreedingLines(selectedTame, cm))
            {
                ancestors.ShowDialog();
            }

        }

        private void btnDinoInventory_Click(object sender, EventArgs e)
        {
            if (lvwTameDetail.SelectedItems.Count == 0) return;

            ContentTamedCreature selectedCreature = (ContentTamedCreature)lvwTameDetail.SelectedItems[0].Tag;
            var tribe = cm.GetTribes(selectedCreature.TargetingTeam).First();
            frmDinoInventoryViewer inventoryViewer = new frmDinoInventoryViewer(selectedCreature, selectedCreature.Inventory.Items);
            inventoryViewer.Owner = this;
            inventoryViewer.ShowDialog();
        }

        private void btnPlayerTribeLog_Click(object sender, EventArgs e)
        {

            if (lvwPlayers.SelectedItems.Count == 0) return;

            ContentPlayer selectedPlayer = (ContentPlayer)lvwPlayers.SelectedItems[0].Tag;
            var tribe = cm.GetPlayerTribe(selectedPlayer.Id);
            frmTribeLog logViewer = new frmTribeLog(tribe);
            logViewer.Owner = this;
            logViewer.ShowDialog();
        }

        private void cboStructureTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStructurePlayerList();
        }

        private void cboStructurePlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayerStructureDetail();
        }

        private void RefreshStructureSummary()
        {
            if (cm == null) return;
            if (cboStructureTribe.SelectedItem == null) return;


            string selectedClass = "NONE";
            if (cboStructureStructure.SelectedItem != null)
            {
                ASVComboValue selectedValue = (ASVComboValue)cboStructureStructure.SelectedItem;
                selectedClass = selectedValue.Key;
            }

            cboStructureStructure.Items.Clear();
            cboStructureStructure.Items.Add(new ASVComboValue() { Key = "NONE", Value = "[None]" });
            cboStructureStructure.Items.Add(new ASVComboValue() { Key = "", Value = "[All Structures]" });

            //tribe
            ASVComboValue comboValue = (ASVComboValue)cboStructureTribe.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            //player
            comboValue = (ASVComboValue)cboStructurePlayer.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);


            var playerStructureTypes = cm.GetPlayerStructures(selectedTribeId, selectedPlayerId, "", false)
                                                        .Where(s =>
                                                            Program.ProgramConfig.StructureExclusions == null
                                                            || (Program.ProgramConfig.StructureExclusions != null & !Program.ProgramConfig.StructureExclusions.Contains(s.ClassName))
                                                        ).GroupBy(g => g.ClassName)
                                                       .Select(s => s.Key);

            List<ASVComboValue> newItems = new List<ASVComboValue>();


            if (playerStructureTypes != null && playerStructureTypes.Count() > 0)
            {

                foreach (var className in playerStructureTypes)
                {
                    var structureName = className;
                    var itemMap = Program.ProgramConfig.StructureMap.Where(i => i.ClassName == className).FirstOrDefault();

                    ASVComboValue classNameItem = new ASVComboValue(className, "");

                    if (itemMap != null && itemMap.FriendlyName.Length > 0)
                    {
                        structureName = itemMap.FriendlyName;
                        classNameItem.Value = structureName;

                    }


                    if (structureName == null || structureName.Length == 0) structureName = className;

                    newItems.Add(new ASVComboValue() { Key = className, Value = structureName });
                }


            }


            int selectedIndex = 1;
            if (newItems.Count > 0)
            {
                cboStructureStructure.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    int newIndex = cboStructureStructure.Items.Add(newItem);
                    if (newItem.Key == selectedClass)
                    {
                        selectedIndex = newIndex;
                    }
                }
                cboStructureStructure.EndUpdate();

            }

            if (tabFeatures.SelectedTab.Name == "tpgStructures")
            {
                cboStructureStructure.SelectedIndex = selectedIndex;
            }
            else
            {
                cboStructureStructure.SelectedIndex = 0;
            }


        }

        private void RefreshTribeSummary()
        {
            if (cm == null) return;

            lvwTribes.Items.Clear();
            var allTribes = cm.GetTribes(0);
            if (allTribes != null && allTribes.Count > 0)
            {
                //tribe id, tribe name, players, tames, structures, last active
                foreach (var tribe in allTribes)
                {
                    ListViewItem newItem = lvwTribes.Items.Add(tribe.TribeId.ToString());
                    newItem.Tag = tribe;
                    newItem.SubItems.Add(tribe.TribeName);
                    newItem.SubItems.Add(tribe.Players.Count.ToString());
                    newItem.SubItems.Add(tribe.Tames.Count.ToString());
                    newItem.SubItems.Add(tribe.Structures.Count.ToString());
                    newItem.SubItems.Add(tribe.LastActive.Equals(new DateTime()) ? "" : tribe.LastActive.ToString());
                }
            }
        }

        private void LoadPlayerStructureDetail()
        {

            if (cm == null) return;
            if (cboStructureTribe.SelectedItem == null) return;
            if (cboStructurePlayer.SelectedItem == null) return;

            this.Cursor = Cursors.WaitCursor;

            btnStructureInventory.Enabled = false;
            btnCopyCommandStructure.Enabled = false;
            lblStatus.Text = "Updating player structure selection.";
            lblStatus.Refresh();

            //tribe
            long selectedTribeId = 0;
            ASVComboValue comboValue = (ASVComboValue)cboStructureTribe.SelectedItem;
            if (comboValue != null) long.TryParse(comboValue.Key, out selectedTribeId);

            //player
            long selectedPlayerId = 0;
            comboValue = (ASVComboValue)cboStructurePlayer.SelectedItem;
            if (comboValue != null) long.TryParse(comboValue.Key, out selectedPlayerId);

            if (selectedPlayerId > 0 && selectedTribeId == 0)
            {
                var tribe = cm.GetPlayerTribe(selectedPlayerId);
                if (tribe != null) selectedTribeId = tribe.TribeId;
            }

            //structure
            string selectedClass = "NONE";
            comboValue = (ASVComboValue)cboStructureStructure.SelectedItem;
            if (comboValue != null) selectedClass = comboValue.Key;


            var playerStructures = cm.GetPlayerStructures(selectedTribeId, selectedPlayerId, selectedClass, false)
                .Where(s => (!Program.ProgramConfig.StructureExclusions.Contains(s.ClassName))).ToList();

            lblStructureTotal.Text = $"Count: {playerStructures.Count()}";
            lblStructureTotal.Refresh();

            lvwStructureLocations.Items.Clear();
            lvwStructureLocations.Refresh();
            lvwStructureLocations.BeginUpdate();

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

            var tribes = cm.GetTribes(selectedTribeId);
            foreach (var tribe in tribes)
            {
                var filterStructures = tribe.Structures.Where(s => s.ClassName == selectedClass || selectedClass == "");
                Parallel.ForEach(filterStructures, playerStructure =>
                {

                    if (!(playerStructure.Latitude.GetValueOrDefault(0) == 0 && playerStructure.Longitude.GetValueOrDefault(0) == 0))
                    {
                        var tribeName = tribe.TribeName;

                        var itemName = playerStructure.ClassName;
                        var itemMap = ARKViewer.Program.ProgramConfig.StructureMap.Where(i => i.ClassName == playerStructure.ClassName).FirstOrDefault();
                        if (itemMap != null && itemMap.FriendlyName.Length > 0)
                        {
                            itemName = itemMap.FriendlyName;
                        }

                        ListViewItem newItem = new ListViewItem(tribeName);
                        newItem.SubItems.Add(itemName);


                        newItem.SubItems.Add(playerStructure.Latitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(playerStructure.Longitude.Value.ToString("0.00"));

                        newItem.Tag = playerStructure;

                        listItems.Add(newItem);
                    }


                });

            }

            lvwStructureLocations.Items.AddRange(listItems.ToArray());

            if (SortingColumn_Structures != null)
            {
                lvwStructureLocations.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_Structures.Index, SortingColumn_Structures.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwStructureLocations.Sort();
            }

            lvwStructureLocations.EndUpdate();
            lblStatus.Text = "Player structure selection updated.";
            lblStatus.Refresh();

            DrawMap(0, 0);

            this.Cursor = Cursors.Default;
        }

        private void lvwStructureLocations_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwStructureLocations.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Structures == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Structures)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Structures.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Structures.Text = SortingColumn_Structures.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Structures = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Structures.Text = "> " + SortingColumn_Structures.Text;
            }
            else
            {
                SortingColumn_Structures.Text = "< " + SortingColumn_Structures.Text;
            }

            // Create a comparer.
            lvwStructureLocations.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwStructureLocations.Sort();
        }

        private void tabFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            tabFeatures.Refresh();
            this.Cursor = Cursors.WaitCursor;
            DrawMap(0, 0);
            this.Cursor = Cursors.Default;
        }

        private void lvwStructureLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            btnCopyCommandStructure.Enabled = lvwStructureLocations.SelectedItems.Count > 0;
            btnStructureInventory.Enabled = false;


            if (lvwStructureLocations.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];
                ContentStructure selectedStructure = (ContentStructure)selectedItem.Tag;

                DrawMap((decimal)selectedStructure.Longitude.GetValueOrDefault(0), (decimal)selectedStructure.Latitude.GetValueOrDefault(0));

                //var inventory = cm.GetInventory(selectedStructure.InventoryId.GetValueOrDefault(0));
                btnStructureInventory.Enabled = selectedStructure.Inventory.Items.Count > 0;

            }

        }

        private void cboStructureStructure_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadPlayerStructureDetail();
        }

        private void btnStructureExclusionFilter_Click(object sender, EventArgs e)
        {
            if (cm == null) return;

            var structureList = cm.GetPlayerStructures(0, 0, "", true);
            if (structureList != null && structureList.Count > 0)
            {
                frmStructureExclusionFilter exclusionEditor = new frmStructureExclusionFilter(structureList);
                exclusionEditor.Owner = this;
                if (exclusionEditor.ShowDialog() == DialogResult.OK)
                {
                    RefreshStructureSummary();
                }

            }
            else
            {
                MessageBox.Show("Structure exclusions can only be set when a map with structures has been loaded.", "No Structures", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

        }

        private void cboConsoleCommandsPlayerTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandPlayer.Enabled = cboConsoleCommandsPlayerTribe.SelectedIndex >= 0 && lvwPlayers.SelectedItems.Count > 0;
        }

        private void btnCopyCommandPlayer_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsPlayerTribe.SelectedItem == null) return;

            var commandText = cboConsoleCommandsPlayerTribe.SelectedItem.ToString();
            string commandList = "";


            if (commandText.Contains("<FileCsvList>"))
            {
                string fileList = "";
                commandList = commandText;

                foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                {
                    ContentTribe selectedTribe = (ContentTribe)selectedItem.Tag;
                    if (fileList.Length > 0)
                    {
                        fileList = fileList + " ";
                    }
                    fileList = fileList + selectedTribe.TribeId.ToString() + ".arktribe";
                }

                commandList = commandList.Replace("<FileCsvList>", fileList);
            }
            else
            {

                foreach (ListViewItem selectedItem in lvwPlayers.SelectedItems)
                {
                    ContentPlayer selectedPlayer = (ContentPlayer)selectedItem.Tag;

                    long selectedPlayerId = selectedPlayer.Id;
                    string selectedSteamId = selectedPlayer.SteamId;

                    var tribe = cm.GetPlayerTribe(selectedPlayer.Id);
                    long selectedTribeId = tribe.TribeId;

                    commandText = cboConsoleCommandsPlayerTribe.SelectedItem.ToString();

                    commandText = commandText.Replace("<PlayerID>", selectedPlayerId.ToString("f0"));
                    commandText = commandText.Replace("<TribeID>", selectedTribeId.ToString("f0"));
                    commandText = commandText.Replace("<SteamID>", selectedSteamId);
                    commandText = commandText.Replace("<PlayerName>", selectedPlayer.Name);
                    commandText = commandText.Replace("<CharacterName>", selectedPlayer.CharacterName);
                    if (tribe != null)
                    {
                        commandText = commandText.Replace("<TribeName>", tribe.TribeName);
                    }

                    commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedPlayer.X:0.00}"));
                    commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedPlayer.Y:0.00}"));
                    commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedPlayer.Z + 250:0.00}"));

                    switch (Program.ProgramConfig.CommandPrefix)
                    {
                        case 1:
                            commandText = $"admincheat {commandText}";

                            break;
                        case 2:
                            commandText = $"cheat {commandText}";
                            break;
                    }

                    commandText = commandText.Trim();

                    if (commandList.Length > 0)
                    {
                        commandList += $"|{commandText}";
                    }
                    else
                    {
                        commandList = commandText;
                    }
                }



            }

            Clipboard.SetText(commandList);

            lblStatus.Text = $"Command copied:  {commandList}";
            lblStatus.Refresh();
        }

        private void btnCopyCommandStructure_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsStructure.SelectedItem == null) return;
            if (lvwStructureLocations.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];

            var commandText = cboConsoleCommandsStructure.SelectedItem.ToString();
            if (commandText != null)
            {
                ContentStructure selectedStructure = (ContentStructure)selectedItem.Tag;

                if (selectedStructure.AbandonedTeam != 0)
                {
                    commandText = commandText.Replace("<TribeID>", selectedStructure.AbandonedTeam.ToString("f0"));
                }
                else
                {
                    commandText = commandText.Replace("<TribeID>", selectedStructure.TargetingTeam.ToString("f0"));
                }

                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedStructure.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedStructure.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedStructure.Z + 250:0.00}"));



                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);
                lblStatus.Text = $"Command copied:  {commandText}";
                lblStatus.Refresh();

            }
        }

        private void cboConsoleCommandsStructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandStructure.Enabled = cboConsoleCommandsStructure.SelectedIndex >= 0 && lvwStructureLocations.SelectedItems.Count > 0;
        }

        private void cboTameTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTamePlayerList();
        }

        private void cboTamePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void cboTameClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void lvwTameDetail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwTameDetail.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_DetailTame == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_DetailTame)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_DetailTame.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_DetailTame.Text = SortingColumn_DetailTame.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_DetailTame = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_DetailTame.Text = "> " + SortingColumn_DetailTame.Text;
            }
            else
            {
                SortingColumn_DetailTame.Text = "< " + SortingColumn_DetailTame.Text;
            }

            // Create a comparer.
            lvwTameDetail.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwTameDetail.Sort();
        }

        private void optStatsBase_CheckedChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void lvwTameDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            btnCopyCommandTamed.Enabled = lvwTameDetail.SelectedItems.Count > 0;
            btnDinoInventory.Enabled = lvwTameDetail.SelectedItems.Count > 0;
            btnDinoAncestors.Enabled = lvwTameDetail.SelectedItems.Count > 0;

            if (lvwTameDetail.SelectedItems.Count > 0)
            {

                var selectedItem = lvwTameDetail.SelectedItems[0];
                ContentTamedCreature selectedTame = (ContentTamedCreature)selectedItem.Tag;
                btnDinoInventory.Enabled = selectedTame.Inventory.Items.Count > 0;

                DrawMap((decimal)selectedTame.Longitude.GetValueOrDefault(0), (decimal)selectedTame.Latitude.GetValueOrDefault(0));

            }
            this.Cursor = Cursors.Default;
        }

        private void picMap_MouseClick(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            PictureBox clickedPic = (PictureBox)sender;

            double zoomLevel = (double)clickedPic.Height / (double)clickedPic.Image.Height;


            double clickY = e.Location.Y / (zoomLevel);
            double clickX = e.Location.X / (zoomLevel);

            double latitude = clickY / 10.25;
            double longitude = clickX / 10.25;

            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    if (lvwWildDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwWildDetail.Items)
                        {
                            if (item.SubItems[4].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[4].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[5].Text);

                                double latDistance = Math.Abs(itemLat - latitude);
                                double lonDistance = Math.Abs(itemLon - longitude);


                                if (latDistance <= 0.5 && lonDistance <= 0.5)
                                {
                                    lvwWildDetail.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }


                    break;
                case "tpgTamed":

                    if (lvwTameDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwTameDetail.Items)
                        {
                            if (item.SubItems[5].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[5].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[6].Text);

                                double latDistance = itemLat - latitude;
                                double lonDistance = itemLon - longitude;


                                if ((latDistance >= 0 && latDistance <= 0.5) && (lonDistance >= 0 && lonDistance <= 0.5))
                                {
                                    lvwTameDetail.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }

                    break;
                case "tpgStructures":
                    if (lvwStructureLocations.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwStructureLocations.Items)
                        {
                            if (item.SubItems[3].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[3].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[4].Text);

                                double latDistance = itemLat - latitude;
                                double lonDistance = itemLon - longitude;


                                if ((latDistance >= 0 && latDistance <= 0.5) && (lonDistance >= 0 && lonDistance <= 0.5))
                                {
                                    lvwStructureLocations.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }


                    break;
                case "tpgPlayers":
                    if (lvwPlayers.Items.Count > 0)
                    {

                        //get nearest 
                        foreach (ListViewItem item in lvwPlayers.Items)
                        {
                            if (item.SubItems[4].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[4].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[5].Text);

                                double latDistance = Math.Abs(itemLat - latitude);
                                double lonDistance = Math.Abs(itemLon - longitude);

                                if (latDistance <= 0.75 && lonDistance <= 0.75)
                                {
                                    lvwPlayers.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }

                        }
                    }

                    break;

                default:
                    break;
            }


            this.Cursor = Cursors.Default;
        }

        private void picMap_Click(object sender, EventArgs e)
        {

        }

        private void cboCryoTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCryoPlayerList();
        }


        private void btnCopyCommandWild_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsWild.SelectedItem == null) return;
            if (lvwWildDetail.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwWildDetail.SelectedItems[0];
            ContentWildCreature selectedCreature = (ContentWildCreature)selectedItem.Tag;
            var commandText = cboConsoleCommandsWild.SelectedItem.ToString();
            if (commandText != null)
            {

                commandText = commandText.Replace("<ClassName>", selectedCreature.ClassName);
                commandText = commandText.Replace("<Level>", selectedCreature.BaseLevel.ToString("f0"));
                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedCreature.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedCreature.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedCreature.Z + 250:0.00}"));

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied:  {commandText}";
                lblStatus.Refresh();

            }
        }

        private void btnCopyCommandTamed_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsTamed.SelectedItem == null) return;
            if (lvwTameDetail.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwTameDetail.SelectedItems[0];

            var commandText = cboConsoleCommandsTamed.SelectedItem.ToString();
            if (commandText != null)
            {

                ContentTamedCreature selectedCreature = (ContentTamedCreature)selectedItem.Tag;
                commandText = commandText.Replace("<ClassName>", selectedCreature.ClassName);
                commandText = commandText.Replace("<Level>", (selectedCreature.BaseLevel / 1.5).ToString("f0"));

                if (selectedCreature.AbandonedTeam != 0)
                {
                    commandText = commandText.Replace("<TribeID>", selectedCreature.AbandonedTeam.ToString("f0"));
                }
                else
                {
                    commandText = commandText.Replace("<TribeID>", selectedCreature.TargetingTeam.ToString("f0"));
                }

                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedCreature.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedCreature.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedCreature.Z + 250:0.00}"));

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = commandText.Replace("<DoTame>", "admincheat DoTame");
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = commandText.Replace("<DoTame>", "cheat DoTame");
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied:  {commandText}";
                lblStatus.Refresh();

            }
        }


        private void lvwPlayers_Click(object sender, EventArgs e)
        {

        }

        private void lvwPlayers_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = true;
                mnuContext_SteamId.Visible = true;
                mnuContext_TribeId.Visible = false;
            }
            else
            {
                if (isLoading) return;

                //picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
                btnPlayerInventory.Enabled = lvwPlayers.SelectedItems.Count == 1;
                btnPlayerTribeLog.Enabled = lvwPlayers.SelectedItems.Count == 1;
                btnCopyCommandPlayer.Enabled = lvwPlayers.SelectedItems.Count > 0 && cboConsoleCommandsPlayerTribe.SelectedIndex >= 0;
                btnDeletePlayer.Enabled = lvwPlayers.SelectedItems.Count == 1;
            }
        }

        private void lvwStructureLocations_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }
        }

        private void mnuContext_PlayerId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":

                    break;

                case "tpgStructures":

                    break;
                case "tpgPlayers":
                    if (lvwPlayers.SelectedItems.Count > 0)
                    {
                        ContentPlayer player = (ContentPlayer)lvwPlayers.SelectedItems[0].Tag;
                        Clipboard.SetText(player.Id.ToString("f0"));
                        MessageBox.Show("Player ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
            }
        }

        private void mnuContext_SteamId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":

                    break;

                case "tpgStructures":

                    break;
                case "tpgPlayers":
                    if (lvwPlayers.SelectedItems.Count > 0)
                    {
                        ContentPlayer player = (ContentPlayer)lvwPlayers.SelectedItems[0].Tag;
                        Clipboard.SetText(player.SteamId.ToString());
                        MessageBox.Show("Steam ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
            }
        }

        private void mnuContext_TribeId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":
                    if (lvwTameDetail.SelectedItems.Count > 0)
                    {
                        ContentTamedCreature creature = (ContentTamedCreature)lvwTameDetail.SelectedItems[0].Tag;
                        Clipboard.SetText(creature.TargetingTeam.ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;

                case "tpgStructures":
                    if (lvwStructureLocations.SelectedItems.Count > 0)
                    {
                        ContentStructure structure = (ContentStructure)lvwStructureLocations.SelectedItems[0].Tag;
                        Clipboard.SetText(structure.TargetingTeam.ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "tpgTribes":
                    if (lvwTribes.SelectedItems.Count > 0)
                    {
                        ContentTribe selectedTribe = (ContentTribe)lvwTribes.SelectedItems[0].Tag;

                        Clipboard.SetText(selectedTribe.TribeId.ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
                case "tpgPlayers":

                    break;
            }
        }

        private void chkCryo_CheckedChanged(object sender, EventArgs e)
        {
            chkCryo.BackgroundImage = chkCryo.Checked ? ARKViewer.Properties.Resources.button_cryoon : ARKViewer.Properties.Resources.button_cryooff;
            Program.ProgramConfig.StoredTames = chkCryo.Checked;

            LoadTameDetail();
        }

        private void cboDroppedTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDroppedPlayers();
        }

        private void cboDroppedPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDroppedItems();
        }

        private void cboDroppedItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDroppedItemDetail();
        }

        private void lvwDroppedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandDropped.Enabled = lvwDroppedItems.SelectedItems.Count > 0;

            if (lvwDroppedItems.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwDroppedItems.SelectedItems[0];

                decimal selectedX = 0;
                decimal selectedY = 0;

                ContentDroppedItem droppedItem = (ContentDroppedItem)selectedItem.Tag;

                selectedX = (decimal)droppedItem.Longitude.GetValueOrDefault(0);
                selectedY = (decimal)droppedItem.Latitude.GetValueOrDefault(0);

                btnDropInventory.Enabled = droppedItem.Inventory.Items.Count > 0;

                DrawMap(selectedX, selectedY);
            }

        }

        private void btnCopyCommandDropped_Click(object sender, EventArgs e)
        {
            if (cboCopyCommandDropped.SelectedItem == null) return;

            var commandText = cboCopyCommandDropped.SelectedItem.ToString();
            if (commandText != null)
            {

                ListViewItem selectedItem = lvwDroppedItems.SelectedItems[0];
                ContentDroppedItem droppedItem = (ContentDroppedItem)selectedItem.Tag;
                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{droppedItem.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{droppedItem.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{droppedItem.Z + 100:0.00}"));

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied:  {commandText}";
                lblStatus.Refresh();

            }
        }

        private void udWildMax_ValueChanged(object sender, EventArgs e)
        {
            if (udWildMin.Value > udWildMax.Value) udWildMin.Value = udWildMax.Value;
            udWildMin.Maximum = udWildMax.Value;
            RefreshWildSummary();
        }

        private void udWildMin_ValueChanged(object sender, EventArgs e)
        {
            if (udWildMax.Value < udWildMin.Value) udWildMax.Value = udWildMin.Value;
            udWildMax.Minimum = udWildMin.Value;
            RefreshWildSummary();

        }

        private void lvwTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawMap(0, 0);

        }

        private void btnTribeLog_Click(object sender, EventArgs e)
        {
            if (cm == null) return;
            if (lvwTribes.SelectedItems.Count == 0) return;
            ContentTribe selectedTribe = (ContentTribe)lvwTribes.SelectedItems[0].Tag;

            var tribe = cm.GetTribes(selectedTribe.TribeId).FirstOrDefault<ContentTribe>();
            if (tribe != null)
            {
                frmTribeLog logViewer = new frmTribeLog(tribe);
                logViewer.Owner = this;
                logViewer.ShowDialog();

            }
        }

        private void lvwTribes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwTribes.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Tribes == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Tribes)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Tribes.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Tribes.Text = SortingColumn_Tribes.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Tribes = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Tribes.Text = "> " + SortingColumn_Tribes.Text;
            }
            else
            {
                SortingColumn_Tribes.Text = "< " + SortingColumn_Tribes.Text;
            }

            // Create a comparer.
            lvwTribes.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwTribes.Sort();
        }

        private void chkTribePlayers_CheckedChanged(object sender, EventArgs e)
        {
            if (cm == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                DrawMap(0, 0);
            }
        }

        private void chkTribeTames_CheckedChanged(object sender, EventArgs e)
        {
            if (cm == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                DrawMap(0, 0);
            }
        }

        private void chkTribeStructures_CheckedChanged(object sender, EventArgs e)
        {
            if (cm == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                DrawMap(0, 0);
            }
        }

        private void btnTribeCopyCommand_Click(object sender, EventArgs e)
        {
            if (cboTribeCopyCommand.SelectedItem == null) return;
            if (lvwTribes.SelectedItems.Count == 0) return;

            string commandList = "";
            var commandText = cboTribeCopyCommand.SelectedItem.ToString();

            if (commandText != null)
            {

                if (commandText.Contains("<FileCsvList>"))
                {
                    commandList = commandText;
                    string fileList = "";

                    foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                    {
                        ContentTribe selectedTribe = (ContentTribe)selectedItem.Tag;
                        if (fileList.Length > 0)
                        {
                            fileList = fileList + " ";
                        }
                        fileList = fileList + selectedTribe.TribeId.ToString() + ".arktribe";
                    }

                    commandList = commandList.Replace("<FileCsvList>", fileList);

                }
                else
                {
                    foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                    {
                        ContentTribe selectedTribe = (ContentTribe)selectedItem.Tag;

                        commandText = cboTribeCopyCommand.SelectedItem.ToString();
                        commandText = commandText.Replace("<TribeID>", selectedTribe.TribeId.ToString("f0"));
                        commandText = commandText.Replace("<TribeName>", selectedTribe.TribeName);

                        switch (Program.ProgramConfig.CommandPrefix)
                        {
                            case 1:
                                commandText = $"admincheat {commandText}";

                                break;
                            case 2:
                                commandText = $"cheat {commandText}";
                                break;
                        }

                        commandText = commandText.Trim();

                        if (commandList.Length > 0)
                        {
                            commandList += $"|{commandText}";
                        }
                        else
                        {
                            commandList = commandText;
                        }

                    }

                }

                Clipboard.SetText(commandList);

                lblStatus.Text = $"Command copied:  {commandList}";
                lblStatus.Refresh();
            }
        }

        private void lvwTribes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }
            else
            {
                if (cm == null) return;

                btnTribeLog.Enabled = false;
                btnTribeCopyCommand.Enabled = lvwTribes.SelectedItems.Count > 0;


                if (lvwTribes.SelectedItems.Count != 1) return;

                ContentTribe selectedTribe = (ContentTribe)lvwTribes.SelectedItems[0].Tag;
                btnTribeLog.Enabled = selectedTribe.Logs.Length > 0;


            }
        }

        private void udWildLat_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void udWildLon_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void udWildRadius_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void pnlMap_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnStructureInventory_Click(object sender, EventArgs e)
        {
            if (lvwStructureLocations.SelectedItems.Count == 0) return;


            ContentStructure selectedStructure = (ContentStructure)lvwStructureLocations.SelectedItems[0].Tag;

            frmStructureInventoryViewer inventoryViewer = new frmStructureInventoryViewer(selectedStructure, selectedStructure.Inventory.Items);
            inventoryViewer.Owner = this;
            inventoryViewer.ShowDialog();
        }

        private void mnuContext_ExportData_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JavaScript Object Notation|*.json|Comma Seperated Values|*.csv";
            saveDialog.Title = "Export Data";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFilename = saveDialog.FileName;

                switch (tabFeatures.SelectedTab.Name)
                {
                    case "tpgWild":

                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwWildDetail.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwWildDetail.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwWildDetail.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwWildDetail.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwWildDetail.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwWildDetail.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwWildDetail.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwWildDetail.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwWildDetail.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwWildDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpgTamed":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwTameDetail.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwTameDetail.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwTameDetail.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTameDetail.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwTameDetail.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwTameDetail.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwTameDetail.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwTameDetail.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwTameDetail.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;

                    case "tpgStructures":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwStructureLocations.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwStructureLocations.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwStructureLocations.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwStructureLocations.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwStructureLocations.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpgTribes":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwStructureLocations.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTribes.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwTribes.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwTribes.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwTribes.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwTribes.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwTribes.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;

                    case "tpgPlayers":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwPlayers.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwPlayers.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwPlayers.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    long.TryParse(item.SubItems[header.Index].Text, out long intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTribes.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwPlayers.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwPlayers.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwPlayers.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwPlayers.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwPlayers.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpdDroppedItems":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwDroppedItems.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwDroppedItems.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwDroppedItems.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (double.TryParse(item.SubItems[header.Index].Text, out _))
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwDroppedItems.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwDroppedItems.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwDroppedItems.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwDroppedItems.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwDroppedItems.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwDroppedItems.Columns.Count; colIndex++)
                                        {

                                            if (double.TryParse(item.SubItems[colIndex].Text, out _))
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                }

            }


        }


        private void lvwWildDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = false;
            }
        }

        private void lvwTameDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }
        }

        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            if (lvwPlayers.SelectedItems.Count == 0) return;

            ContentPlayer selectedPlayer = (ContentPlayer)lvwPlayers.SelectedItems[0].Tag;

            bool shouldRemove = true;

            if (selectedPlayer.LastActiveDateTime.HasValue & !selectedPlayer.LastActiveDateTime.Equals(new DateTime()))
            {
                if (((TimeSpan)(DateTime.Today - selectedPlayer.LastActiveDateTime)).TotalDays <= 14)
                {
                    if (MessageBox.Show("The selected player has been active in the last 14 days.\n\nAre you sure you want to remove their .arkprofile?", "Remove Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        shouldRemove = false;
                    }
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to remove the selected player profile?", "Remove Player Profile?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        shouldRemove = false;
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to remove the selected player?", "Remove Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    shouldRemove = false;
                }
            }


            //remove local
            if (shouldRemove)
            {
                string profilePathLocal = Path.GetDirectoryName(Program.ProgramConfig.SelectedFile);

                if (Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp)
                {
                    profilePathLocal = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ARKViewer.Program.ProgramConfig.SelectedServer);

                    //also remove from server if it still exists
                    DeletePlayerFtp(selectedPlayer);
                }

                string profileFileName = Directory.GetFiles(profilePathLocal, $"{selectedPlayer.SteamId}.arkprofile").FirstOrDefault();
                if (profileFileName != null)
                {
                    try
                    {
                        File.Delete(profileFileName);
                        if (MessageBox.Show("Player profile data removed.\n\nPress OK to reload save data.", "Player Removed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            LoadPlayerDetail();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Failed to remove player profile data.", "Removal Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }

        }

        private void btnDropInventory_Click(object sender, EventArgs e)
        {
            if (lvwDroppedItems.SelectedItems.Count == 0) return;
            switch (lvwDroppedItems.SelectedItems[0].Tag)
            {
                case ContentDroppedItem droppedItem:
                    if (droppedItem.IsDeathCache)
                    {
                        frmDeathCacheViewer inventoryView = new frmDeathCacheViewer(droppedItem, droppedItem.Inventory.Items);
                        inventoryView.Owner = this;
                        inventoryView.ShowDialog();
                    }

                    break;
                default:

                    break;
            }
        }


        private void lvwDroppedItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwDroppedItems.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Drops == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Drops)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Drops.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Drops.Text = SortingColumn_Drops.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Drops = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Drops.Text = "> " + SortingColumn_Drops.Text;
            }
            else
            {
                SortingColumn_Drops.Text = "< " + SortingColumn_Drops.Text;
            }

            // Create a comparer.
            lvwDroppedItems.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwDroppedItems.Sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tpgWild_Click(object sender, EventArgs e)
        {

        }

        private void btnViewMap_Click(object sender, EventArgs e)
        {
            ShowMapViewer();
        }

        private void ShowMapViewer()
        {
            if (MapViewer == null || MapViewer.IsDisposed)
            {
                MapViewer = frmMapView.GetForm(cm);
                //MapViewer.Owner = this;

                MapViewer.OnMapClicked += MapViewer_OnMapClicked;

                DrawMap(0, 0);
            }
            MapViewer.Show();
            MapViewer.BringToFront();
        }

        private void MapViewer_OnMapClicked(decimal latitutde, decimal longitude)
        {
            AttemptReverseMapSelection(latitutde, longitude);
        }

        private void AttemptReverseMapSelection(decimal latitude, decimal longitude)
        {

            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    if (lvwWildDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwWildDetail.Items)
                        {
                            ContentWildCreature wild = (ContentWildCreature)item.Tag;

                            decimal latDistance = Math.Abs((decimal)wild.Latitude.GetValueOrDefault(0) - latitude);
                            decimal lonDistance = Math.Abs((decimal)wild.Longitude.GetValueOrDefault(0) - longitude);

                            if (latDistance <= (decimal)0.5 && lonDistance <= (decimal)0.5)
                            {
                                lvwWildDetail.SelectedItems.Clear();
                                item.Selected = true;
                                item.EnsureVisible();
                                break;
                            }

                        }

                    }


                    break;
                case "tpgTamed":

                    if (lvwTameDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwTameDetail.Items)
                        {
                            ContentTamedCreature tame = (ContentTamedCreature)item.Tag;

                            decimal latDistance = Math.Abs((decimal)tame.Latitude.GetValueOrDefault(0) - latitude);
                            decimal lonDistance = Math.Abs((decimal)tame.Longitude.GetValueOrDefault(0) - longitude);

                            if (latDistance <= (decimal)0.5 && lonDistance <= (decimal)0.5)
                            {
                                lvwTameDetail.SelectedItems.Clear();
                                item.Selected = true;
                                item.EnsureVisible();
                                break;
                            }
                        }
                    }

                    break;
                case "tpgStructures":
                    if (lvwStructureLocations.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwStructureLocations.Items)
                        {
                            ContentStructure structure = (ContentStructure)item.Tag;

                            decimal latDistance = Math.Abs((decimal)structure.Latitude.GetValueOrDefault(0) - latitude);
                            decimal lonDistance = Math.Abs((decimal)structure.Longitude.GetValueOrDefault(0) - longitude);

                            if (latDistance <= (decimal)0.5 && lonDistance <= (decimal)0.5)
                            {
                                lvwStructureLocations.SelectedItems.Clear();
                                item.Selected = true;
                                item.EnsureVisible();
                                break;
                            }

                        }


                    }


                    break;
                case "tpgPlayers":
                    if (lvwPlayers.Items.Count > 0)
                    {

                        //get nearest 
                        foreach (ListViewItem item in lvwPlayers.Items)
                        {
                            ContentPlayer player = (ContentPlayer)item.Tag;

                            decimal latDistance = Math.Abs((decimal)player.Latitude.GetValueOrDefault(0) - latitude);
                            decimal lonDistance = Math.Abs((decimal)player.Longitude.GetValueOrDefault(0) - longitude);

                            if (latDistance <= (decimal)0.5 && lonDistance <= (decimal)0.5)
                            {
                                lvwStructureLocations.SelectedItems.Clear();
                                item.Selected = true;
                                item.EnsureVisible();
                                break;
                            }

                        }
                    }

                    break;
                case "tpgItemList":

                    if (lvwItemList.Items.Count > 0)
                    {

                        //get nearest 
                        foreach (ListViewItem item in lvwItemList.Items)
                        {
                            ASVFoundItem foundItem = (ASVFoundItem)item.Tag;

                            decimal latDistance = Math.Abs((decimal)foundItem.Latitude - latitude);
                            decimal lonDistance = Math.Abs((decimal)foundItem.Longitude - longitude);

                            if (latDistance <= (decimal)0.5 && lonDistance <= (decimal)0.5)
                            {
                                lvwItemList.SelectedItems.Clear();
                                item.Selected = true;
                                item.EnsureVisible();
                                break;
                            }

                        }
                    }

                    break;
                default:
                    break;
            }


        }

        private void frmViewer_Enter(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void frmViewer_Load(object sender, EventArgs e)
        {

        }

        private void frmViewer_LocationChanged(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void udWildMin_Enter(object sender, EventArgs e)
        {
            udWildMin.Select(0, udWildMin.Value.ToString().Length);
        }

        private void udWildMax_Enter(object sender, EventArgs e)
        {
            udWildMax.Select(0, udWildMax.Value.ToString().Length);
        }

        private void udWildLat_Enter(object sender, EventArgs e)
        {
            udWildLat.Select(0, udWildLat.Value.ToString().Length);
        }

        private void udWildLon_Enter(object sender, EventArgs e)
        {
            udWildLon.Select(0, udWildLon.Value.ToString().Length);
        }

        private void udWildRadius_Enter(object sender, EventArgs e)
        {
            udWildRadius.Select(0, udWildRadius.Value.ToString().Length);
        }

        private void udWildRadius_MouseClick(object sender, MouseEventArgs e)
        {
            udWildRadius.Select(0, udWildRadius.Value.ToString().Length);
        }

        private void udWildMin_MouseClick(object sender, MouseEventArgs e)
        {
            udWildMin.Select(0, udWildMin.Value.ToString().Length);
        }

        private void udWildMax_MouseClick(object sender, MouseEventArgs e)
        {
            udWildMax.Select(0, udWildMax.Value.ToString().Length);
        }

        private void udWildLat_MouseClick(object sender, MouseEventArgs e)
        {
            udWildLat.Select(0, udWildLat.Value.ToString().Length);
        }

        private void udWildLon_MouseClick(object sender, MouseEventArgs e)
        {
            udWildLon.Select(0, udWildLon.Value.ToString().Length);
        }

        private void cboWildResource_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboWildClass.Items.Count > 1 && cboWildResource.SelectedIndex > 0)
            {
                if (cboWildClass.SelectedIndex != 1)
                {
                    cboWildClass.SelectedIndex = 1;
                }
                else
                {
                    LoadWildDetail();
                }
            }
            else
            {
                LoadWildDetail();
            }
        }

        private void cboItemListTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItemListDetail();
        }

        private void cboItemListItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItemListDetail();
        }

        private void LoadItemListDetail()
        {
            if (cm == null) return;
            if (cboItemListTribe.SelectedItem == null) return;
            if (cboItemListItem.SelectedItem == null) return;

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Searching inventories....";
            lblStatus.Refresh();

            lvwItemList.BeginUpdate();
            lvwItemList.Items.Clear();

            int.TryParse(((ASVComboValue)cboItemListTribe.SelectedItem).Key, out int selectedTribeId);
            string selectedItemClass = ((ASVComboValue)cboItemListItem.SelectedItem).Key;

            List<ListViewItem> newItems = new List<ListViewItem>();
            var foundItems = cm.GetItems(selectedTribeId, selectedItemClass);
            if (foundItems != null && foundItems.Count > 0)
            {
                foreach (var foundItem in foundItems)
                {
                    ListViewItem newItem = new ListViewItem(foundItem.TribeName);
                    newItem.SubItems.Add(foundItem.ContainerName);
                    newItem.SubItems.Add(foundItem.DisplayName);
                    newItem.SubItems.Add(foundItem.Quantity.ToString());
                    newItem.SubItems.Add(foundItem.Latitude.ToString("f2"));
                    newItem.SubItems.Add(foundItem.Longitude.ToString("f2"));
                    newItem.Tag = foundItem;
                    newItems.Add(newItem);
                }
            }

            if (newItems.Count > 0) lvwItemList.Items.AddRange(newItems.ToArray());

            lvwItemList.EndUpdate();


            lblStatus.Text = "Search results populated.";
            lblStatus.Refresh();

            lblItemListCount.Text = $"Count: {lvwItemList.Items.Count}";

            if (tabFeatures.SelectedTab.Name == "tpgItemList")
            {
                DrawMap(0, 0);
            }

            if (SortingColumn_ItemList != null)
            {
                lvwItemList.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_ItemList.Index, SortingColumn_ItemList.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwItemList.Sort();
            }

            this.Cursor = Cursors.Default;


        }

        private void lvwItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnItemListCommand.Enabled = lvwItemList.SelectedItems.Count == 1;

            if (lvwItemList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwItemList.SelectedItems[0];

                decimal selectedX = 0;
                decimal selectedY = 0;

                ASVFoundItem foundItem = (ASVFoundItem)selectedItem.Tag;

                selectedX = foundItem.Longitude;
                selectedY = foundItem.Latitude;

                DrawMap(selectedX, selectedY);
            }
        }

        private void btnItemListCommand_Click(object sender, EventArgs e)
        {
            if (cboItemListCommand.SelectedItem == null) return;

            var commandText = cboItemListCommand.SelectedItem.ToString();
            if (commandText != null)
            {

                ListViewItem selectedItem = lvwItemList.SelectedItems[0];
                ASVFoundItem droppedItem = (ASVFoundItem)selectedItem.Tag;
                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{droppedItem.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{droppedItem.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{droppedItem.Z + 100:0.00}"));

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied:  {commandText}";
                lblStatus.Refresh();

            }
        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            picIcon.Image.Save(@"C:\temp\ASV.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void cboSelectedMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cboSelectedMap.SelectedItem == null) return;

            cboSelectedMap.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            ASVComboValue selectedItem = (ASVComboValue)cboSelectedMap.SelectedItem;

            Program.ProgramConfig.SelectedFile = selectedItem.Key;
            if (Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp) Program.ProgramConfig.SelectedServer = selectedItem.Value;

            RefreshMap();

            cboSelectedMap.Enabled = true;
            this.Cursor = Cursors.Default;

        }
    }
}
