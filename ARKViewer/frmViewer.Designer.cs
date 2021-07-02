namespace ARKViewer
{
    partial class frmViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewer));
            this.lvwWildDetail = new System.Windows.Forms.ListView();
            this.lvwWildDetail_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Base = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_HP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Craft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContext_PlayerId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_SteamId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_TribeId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWildTotal = new System.Windows.Forms.Label();
            this.lblMapDate = new System.Windows.Forms.Label();
            this.cboWildClass = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnPlayerInventory = new System.Windows.Forms.Button();
            this.btnPlayerTribeLog = new System.Windows.Forms.Button();
            this.btnStructureExclusionFilter = new System.Windows.Forms.Button();
            this.btnCopyCommandPlayer = new System.Windows.Forms.Button();
            this.btnCopyCommandStructure = new System.Windows.Forms.Button();
            this.btnDinoAncestors = new System.Windows.Forms.Button();
            this.btnDinoInventory = new System.Windows.Forms.Button();
            this.btnCopyCommandWild = new System.Windows.Forms.Button();
            this.btnCopyCommandTamed = new System.Windows.Forms.Button();
            this.chkCryo = new System.Windows.Forms.CheckBox();
            this.btnCopyCommandDropped = new System.Windows.Forms.Button();
            this.btnTribeCopyCommand = new System.Windows.Forms.Button();
            this.btnTribeLog = new System.Windows.Forms.Button();
            this.btnStructureInventory = new System.Windows.Forms.Button();
            this.btnDeletePlayer = new System.Windows.Forms.Button();
            this.btnDropInventory = new System.Windows.Forms.Button();
            this.btnViewMap = new System.Windows.Forms.Button();
            this.tabFeatures = new System.Windows.Forms.TabControl();
            this.tpgWild = new System.Windows.Forms.TabPage();
            this.cboWildResource = new System.Windows.Forms.ComboBox();
            this.lblResource = new System.Windows.Forms.Label();
            this.lblWildRadius = new System.Windows.Forms.Label();
            this.udWildRadius = new System.Windows.Forms.NumericUpDown();
            this.lblWildLon = new System.Windows.Forms.Label();
            this.udWildLon = new System.Windows.Forms.NumericUpDown();
            this.lblWildLat = new System.Windows.Forms.Label();
            this.udWildLat = new System.Windows.Forms.NumericUpDown();
            this.lblWildMin = new System.Windows.Forms.Label();
            this.lblWildMax = new System.Windows.Forms.Label();
            this.udWildMin = new System.Windows.Forms.NumericUpDown();
            this.udWildMax = new System.Windows.Forms.NumericUpDown();
            this.lblWildCommand = new System.Windows.Forms.Label();
            this.cboConsoleCommandsWild = new System.Windows.Forms.ComboBox();
            this.lblSelectedWildTotal = new System.Windows.Forms.Label();
            this.lblWildClass = new System.Windows.Forms.Label();
            this.tpgTamed = new System.Windows.Forms.TabPage();
            this.lblTamedCommand = new System.Windows.Forms.Label();
            this.cboConsoleCommandsTamed = new System.Windows.Forms.ComboBox();
            this.cboTameTribes = new System.Windows.Forms.ComboBox();
            this.cboTamePlayers = new System.Windows.Forms.ComboBox();
            this.lblTameCreature = new System.Windows.Forms.Label();
            this.lblTamePlayer = new System.Windows.Forms.Label();
            this.lblTameTribe = new System.Windows.Forms.Label();
            this.lvwTameDetail = new System.Windows.Forms.ListView();
            this.lvwTameDetail_Creature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Base = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_HP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Craft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Server = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Tamer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Imprinter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Imprint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Cryo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_MutationsFemale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_MutationsMale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTameTotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.optStatsTamed = new System.Windows.Forms.RadioButton();
            this.optStatsBase = new System.Windows.Forms.RadioButton();
            this.cboTameClass = new System.Windows.Forms.ComboBox();
            this.tpgStructures = new System.Windows.Forms.TabPage();
            this.lblStructureTotal = new System.Windows.Forms.Label();
            this.lblCommandStructure = new System.Windows.Forms.Label();
            this.cboConsoleCommandsStructure = new System.Windows.Forms.ComboBox();
            this.lblStructureStructure = new System.Windows.Forms.Label();
            this.cboStructureStructure = new System.Windows.Forms.ComboBox();
            this.lblStructurePlayer = new System.Windows.Forms.Label();
            this.lblStructureTribe = new System.Windows.Forms.Label();
            this.cboStructureTribe = new System.Windows.Forms.ComboBox();
            this.cboStructurePlayer = new System.Windows.Forms.ComboBox();
            this.lvwStructureLocations = new System.Windows.Forms.ListView();
            this.lvwStructureLocations_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Structure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgTribes = new System.Windows.Forms.TabPage();
            this.chkTribeStructures = new System.Windows.Forms.CheckBox();
            this.chkTribeTames = new System.Windows.Forms.CheckBox();
            this.chkTribePlayers = new System.Windows.Forms.CheckBox();
            this.lblTribeCopyCommand = new System.Windows.Forms.Label();
            this.cboTribeCopyCommand = new System.Windows.Forms.ComboBox();
            this.lvwTribes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgPlayers = new System.Windows.Forms.TabPage();
            this.btmMissionScoreboard = new System.Windows.Forms.Button();
            this.lblPlayerTotal = new System.Windows.Forms.Label();
            this.lblCommandPlayer = new System.Windows.Forms.Label();
            this.cboConsoleCommandsPlayerTribe = new System.Windows.Forms.ComboBox();
            this.lblPlayersPlayer = new System.Windows.Forms.Label();
            this.lblPlayersTribe = new System.Windows.Forms.Label();
            this.cboTribes = new System.Windows.Forms.ComboBox();
            this.cboPlayers = new System.Windows.Forms.ComboBox();
            this.lvwPlayers = new System.Windows.Forms.ListView();
            this.lvwPlayers_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Hp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Water = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Crafting = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Fortitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_LastOnline = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_SteamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_SteamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgDroppedItems = new System.Windows.Forms.TabPage();
            this.chkDroppedBlueprints = new System.Windows.Forms.CheckBox();
            this.cboDroppedItem = new System.Windows.Forms.ComboBox();
            this.lblDroppedPlayer = new System.Windows.Forms.Label();
            this.cboDroppedPlayer = new System.Windows.Forms.ComboBox();
            this.lblCopyCommandDropped = new System.Windows.Forms.Label();
            this.cboCopyCommandDropped = new System.Windows.Forms.ComboBox();
            this.lblCountDropped = new System.Windows.Forms.Label();
            this.lblDroppedItem = new System.Windows.Forms.Label();
            this.lvwDroppedItems = new System.Windows.Forms.ListView();
            this.lvwDroppedItems_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Bp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_DroppedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Player = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgItemList = new System.Windows.Forms.TabPage();
            this.chkItemSearchBlueprints = new System.Windows.Forms.CheckBox();
            this.cboItemListItem = new System.Windows.Forms.ComboBox();
            this.lblItemListTribe = new System.Windows.Forms.Label();
            this.cboItemListTribe = new System.Windows.Forms.ComboBox();
            this.btnItemListCommand = new System.Windows.Forms.Button();
            this.lblItemListCommand = new System.Windows.Forms.Label();
            this.cboItemListCommand = new System.Windows.Forms.ComboBox();
            this.lblItemListCount = new System.Windows.Forms.Label();
            this.lblItemListItem = new System.Windows.Forms.Label();
            this.lvwItemList = new System.Windows.Forms.ListView();
            this.lvwItemList_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_Container = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_BP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwItemList_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblStatus = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblMapTypeName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblMap = new System.Windows.Forms.Label();
            this.cboSelectedMap = new System.Windows.Forms.ComboBox();
            this.cboTamedResource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuContext.SuspendLayout();
            this.tabFeatures.SuspendLayout();
            this.tpgWild.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWildRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMax)).BeginInit();
            this.tpgTamed.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tpgStructures.SuspendLayout();
            this.tpgTribes.SuspendLayout();
            this.tpgPlayers.SuspendLayout();
            this.tpgDroppedItems.SuspendLayout();
            this.tpgItemList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwWildDetail
            // 
            this.lvwWildDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwWildDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwWildDetail_Name,
            this.lvwWildDetail_Sex,
            this.lvwWildDetail_Base,
            this.lvwWildDetail_Level,
            this.lvwWildDetail_Lat,
            this.lvwWildDetail_Lon,
            this.lvwWildDetail_HP,
            this.lvwWildDetail_Stam,
            this.lvwWildDetail_Melee,
            this.lvwWildDetail_Weight,
            this.lvwWildDetail_Speed,
            this.lvwWildDetail_Food,
            this.lvwWildDetail_Oxygen,
            this.lvwWildDetail_Craft,
            this.lvwWildDetail_Colour1,
            this.lvwWildDetail_Colour2,
            this.lvwWildDetail_Colour3,
            this.lvwWildDetail_Colour4,
            this.lvwWildDetail_Colour5,
            this.lvwWildDetail_Colour6,
            this.lvwWildDetail_Id});
            this.lvwWildDetail.ContextMenuStrip = this.mnuContext;
            this.lvwWildDetail.FullRowSelect = true;
            this.lvwWildDetail.HideSelection = false;
            this.lvwWildDetail.Location = new System.Drawing.Point(11, 77);
            this.lvwWildDetail.MultiSelect = false;
            this.lvwWildDetail.Name = "lvwWildDetail";
            this.lvwWildDetail.Size = new System.Drawing.Size(786, 336);
            this.lvwWildDetail.TabIndex = 14;
            this.lvwWildDetail.UseCompatibleStateImageBehavior = false;
            this.lvwWildDetail.View = System.Windows.Forms.View.Details;
            this.lvwWildDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwWildDetail_ColumnClick);
            this.lvwWildDetail.SelectedIndexChanged += new System.EventHandler(this.LvwWildDetail_SelectedIndexChanged);
            this.lvwWildDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwWildDetail_MouseClick);
            // 
            // lvwWildDetail_Name
            // 
            this.lvwWildDetail_Name.Text = "Creature";
            this.lvwWildDetail_Name.Width = 142;
            // 
            // lvwWildDetail_Sex
            // 
            this.lvwWildDetail_Sex.Text = "Sex";
            this.lvwWildDetail_Sex.Width = 52;
            // 
            // lvwWildDetail_Base
            // 
            this.lvwWildDetail_Base.Text = "Base";
            this.lvwWildDetail_Base.Width = 0;
            // 
            // lvwWildDetail_Level
            // 
            this.lvwWildDetail_Level.Text = "Lvl";
            this.lvwWildDetail_Level.Width = 40;
            // 
            // lvwWildDetail_Lat
            // 
            this.lvwWildDetail_Lat.Text = "Lat";
            this.lvwWildDetail_Lat.Width = 51;
            // 
            // lvwWildDetail_Lon
            // 
            this.lvwWildDetail_Lon.Text = "Lon";
            this.lvwWildDetail_Lon.Width = 40;
            // 
            // lvwWildDetail_HP
            // 
            this.lvwWildDetail_HP.Text = "HP";
            this.lvwWildDetail_HP.Width = 45;
            // 
            // lvwWildDetail_Stam
            // 
            this.lvwWildDetail_Stam.Text = "Stam";
            this.lvwWildDetail_Stam.Width = 45;
            // 
            // lvwWildDetail_Melee
            // 
            this.lvwWildDetail_Melee.Text = "Melee";
            this.lvwWildDetail_Melee.Width = 48;
            // 
            // lvwWildDetail_Weight
            // 
            this.lvwWildDetail_Weight.Text = "Weight";
            this.lvwWildDetail_Weight.Width = 55;
            // 
            // lvwWildDetail_Speed
            // 
            this.lvwWildDetail_Speed.Text = "Speed";
            this.lvwWildDetail_Speed.Width = 50;
            // 
            // lvwWildDetail_Food
            // 
            this.lvwWildDetail_Food.Text = "Food";
            this.lvwWildDetail_Food.Width = 47;
            // 
            // lvwWildDetail_Oxygen
            // 
            this.lvwWildDetail_Oxygen.Text = "Oxygen";
            this.lvwWildDetail_Oxygen.Width = 53;
            // 
            // lvwWildDetail_Craft
            // 
            this.lvwWildDetail_Craft.Text = "Craft";
            this.lvwWildDetail_Craft.Width = 50;
            // 
            // lvwWildDetail_Colour1
            // 
            this.lvwWildDetail_Colour1.Text = "C0";
            this.lvwWildDetail_Colour1.Width = 35;
            // 
            // lvwWildDetail_Colour2
            // 
            this.lvwWildDetail_Colour2.Text = "C1";
            this.lvwWildDetail_Colour2.Width = 35;
            // 
            // lvwWildDetail_Colour3
            // 
            this.lvwWildDetail_Colour3.Text = "C2";
            this.lvwWildDetail_Colour3.Width = 35;
            // 
            // lvwWildDetail_Colour4
            // 
            this.lvwWildDetail_Colour4.Text = "C3";
            this.lvwWildDetail_Colour4.Width = 35;
            // 
            // lvwWildDetail_Colour5
            // 
            this.lvwWildDetail_Colour5.Text = "C4";
            this.lvwWildDetail_Colour5.Width = 35;
            // 
            // lvwWildDetail_Colour6
            // 
            this.lvwWildDetail_Colour6.Text = "C5";
            this.lvwWildDetail_Colour6.Width = 35;
            // 
            // lvwWildDetail_Id
            // 
            this.lvwWildDetail_Id.Text = "Id";
            this.lvwWildDetail_Id.Width = 0;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContext_PlayerId,
            this.mnuContext_SteamId,
            this.mnuContext_TribeId,
            this.mnuContext_Export});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(153, 92);
            // 
            // mnuContext_PlayerId
            // 
            this.mnuContext_PlayerId.Name = "mnuContext_PlayerId";
            this.mnuContext_PlayerId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_PlayerId.Text = "Copy Player ID";
            this.mnuContext_PlayerId.Click += new System.EventHandler(this.mnuContext_PlayerId_Click);
            // 
            // mnuContext_SteamId
            // 
            this.mnuContext_SteamId.Name = "mnuContext_SteamId";
            this.mnuContext_SteamId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_SteamId.Text = "Copy Steam ID";
            this.mnuContext_SteamId.Click += new System.EventHandler(this.mnuContext_SteamId_Click);
            // 
            // mnuContext_TribeId
            // 
            this.mnuContext_TribeId.Name = "mnuContext_TribeId";
            this.mnuContext_TribeId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_TribeId.Text = "Copy Tribe ID";
            this.mnuContext_TribeId.Click += new System.EventHandler(this.mnuContext_TribeId_Click);
            // 
            // mnuContext_Export
            // 
            this.mnuContext_Export.Name = "mnuContext_Export";
            this.mnuContext_Export.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_Export.Text = "Export Data";
            this.mnuContext_Export.Click += new System.EventHandler(this.mnuContext_ExportData_Click);
            // 
            // lblWildTotal
            // 
            this.lblWildTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWildTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblWildTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildTotal.Location = new System.Drawing.Point(668, 422);
            this.lblWildTotal.Name = "lblWildTotal";
            this.lblWildTotal.Size = new System.Drawing.Size(130, 30);
            this.lblWildTotal.TabIndex = 19;
            this.lblWildTotal.Text = "Total: 0";
            this.lblWildTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMapDate
            // 
            this.lblMapDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapDate.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblMapDate.Location = new System.Drawing.Point(498, 10);
            this.lblMapDate.Name = "lblMapDate";
            this.lblMapDate.Size = new System.Drawing.Size(328, 18);
            this.lblMapDate.TabIndex = 3;
            this.lblMapDate.Text = "No Map Loaded";
            this.lblMapDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboWildClass
            // 
            this.cboWildClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWildClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboWildClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWildClass.FormattingEnabled = true;
            this.cboWildClass.Location = new System.Drawing.Point(82, 45);
            this.cboWildClass.Name = "cboWildClass";
            this.cboWildClass.Size = new System.Drawing.Size(456, 21);
            this.cboWildClass.TabIndex = 13;
            this.cboWildClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboSelected_DrawItem);
            this.cboWildClass.SelectedIndexChanged += new System.EventHandler(this.CboWildClass_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(664, 564);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 50);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.TabStop = false;
            this.toolTip1.SetToolTip(this.btnRefresh, "Refresh loaded map.");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Image = global::ARKViewer.Properties.Resources.button_settings;
            this.btnSettings.Location = new System.Drawing.Point(776, 564);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(50, 50);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSettings, "Show available settings.");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnPlayerInventory
            // 
            this.btnPlayerInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayerInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayerInventory.Enabled = false;
            this.btnPlayerInventory.Image = global::ARKViewer.Properties.Resources.button_family;
            this.btnPlayerInventory.Location = new System.Drawing.Point(436, 412);
            this.btnPlayerInventory.Name = "btnPlayerInventory";
            this.btnPlayerInventory.Size = new System.Drawing.Size(40, 40);
            this.btnPlayerInventory.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnPlayerInventory, "Show player explorer.");
            this.btnPlayerInventory.UseVisualStyleBackColor = true;
            this.btnPlayerInventory.Click += new System.EventHandler(this.btnPlayerInventory_Click);
            // 
            // btnPlayerTribeLog
            // 
            this.btnPlayerTribeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayerTribeLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayerTribeLog.Enabled = false;
            this.btnPlayerTribeLog.Image = global::ARKViewer.Properties.Resources.TribeLogs;
            this.btnPlayerTribeLog.Location = new System.Drawing.Point(393, 412);
            this.btnPlayerTribeLog.Name = "btnPlayerTribeLog";
            this.btnPlayerTribeLog.Size = new System.Drawing.Size(40, 40);
            this.btnPlayerTribeLog.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnPlayerTribeLog, "View tribe log.");
            this.btnPlayerTribeLog.UseVisualStyleBackColor = true;
            this.btnPlayerTribeLog.Click += new System.EventHandler(this.btnPlayerTribeLog_Click);
            // 
            // btnStructureExclusionFilter
            // 
            this.btnStructureExclusionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStructureExclusionFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStructureExclusionFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnStructureExclusionFilter.Image")));
            this.btnStructureExclusionFilter.Location = new System.Drawing.Point(758, 7);
            this.btnStructureExclusionFilter.Name = "btnStructureExclusionFilter";
            this.btnStructureExclusionFilter.Size = new System.Drawing.Size(40, 40);
            this.btnStructureExclusionFilter.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnStructureExclusionFilter, "Structure exclusion list");
            this.btnStructureExclusionFilter.UseVisualStyleBackColor = true;
            this.btnStructureExclusionFilter.Click += new System.EventHandler(this.btnStructureExclusionFilter_Click);
            // 
            // btnCopyCommandPlayer
            // 
            this.btnCopyCommandPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyCommandPlayer.Enabled = false;
            this.btnCopyCommandPlayer.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandPlayer.Location = new System.Drawing.Point(350, 412);
            this.btnCopyCommandPlayer.Name = "btnCopyCommandPlayer";
            this.btnCopyCommandPlayer.Size = new System.Drawing.Size(40, 40);
            this.btnCopyCommandPlayer.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnCopyCommandPlayer, "Copy command to clipboard.");
            this.btnCopyCommandPlayer.UseVisualStyleBackColor = true;
            this.btnCopyCommandPlayer.Click += new System.EventHandler(this.btnCopyCommandPlayer_Click);
            // 
            // btnCopyCommandStructure
            // 
            this.btnCopyCommandStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandStructure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyCommandStructure.Enabled = false;
            this.btnCopyCommandStructure.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandStructure.Location = new System.Drawing.Point(345, 410);
            this.btnCopyCommandStructure.Name = "btnCopyCommandStructure";
            this.btnCopyCommandStructure.Size = new System.Drawing.Size(40, 40);
            this.btnCopyCommandStructure.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnCopyCommandStructure, "Copy command to clipboard.");
            this.btnCopyCommandStructure.UseVisualStyleBackColor = true;
            this.btnCopyCommandStructure.Click += new System.EventHandler(this.btnCopyCommandStructure_Click);
            // 
            // btnDinoAncestors
            // 
            this.btnDinoAncestors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDinoAncestors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDinoAncestors.Enabled = false;
            this.btnDinoAncestors.Image = global::ARKViewer.Properties.Resources.loveicon;
            this.btnDinoAncestors.Location = new System.Drawing.Point(562, 415);
            this.btnDinoAncestors.Name = "btnDinoAncestors";
            this.btnDinoAncestors.Size = new System.Drawing.Size(40, 40);
            this.btnDinoAncestors.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnDinoAncestors, "View breeding lines.");
            this.btnDinoAncestors.UseVisualStyleBackColor = true;
            this.btnDinoAncestors.Click += new System.EventHandler(this.btnDinoAncestors_Click);
            // 
            // btnDinoInventory
            // 
            this.btnDinoInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDinoInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDinoInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnDinoInventory.Image")));
            this.btnDinoInventory.Location = new System.Drawing.Point(606, 415);
            this.btnDinoInventory.Name = "btnDinoInventory";
            this.btnDinoInventory.Size = new System.Drawing.Size(40, 40);
            this.btnDinoInventory.TabIndex = 13;
            this.toolTip1.SetToolTip(this.btnDinoInventory, "View inventory.");
            this.btnDinoInventory.UseVisualStyleBackColor = true;
            this.btnDinoInventory.Click += new System.EventHandler(this.btnDinoInventory_Click);
            // 
            // btnCopyCommandWild
            // 
            this.btnCopyCommandWild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandWild.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyCommandWild.Enabled = false;
            this.btnCopyCommandWild.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandWild.Location = new System.Drawing.Point(350, 417);
            this.btnCopyCommandWild.Name = "btnCopyCommandWild";
            this.btnCopyCommandWild.Size = new System.Drawing.Size(40, 40);
            this.btnCopyCommandWild.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnCopyCommandWild, "Copy command to clipboard.");
            this.btnCopyCommandWild.UseVisualStyleBackColor = true;
            this.btnCopyCommandWild.Click += new System.EventHandler(this.btnCopyCommandWild_Click);
            // 
            // btnCopyCommandTamed
            // 
            this.btnCopyCommandTamed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandTamed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyCommandTamed.Enabled = false;
            this.btnCopyCommandTamed.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandTamed.Location = new System.Drawing.Point(518, 415);
            this.btnCopyCommandTamed.Name = "btnCopyCommandTamed";
            this.btnCopyCommandTamed.Size = new System.Drawing.Size(40, 40);
            this.btnCopyCommandTamed.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btnCopyCommandTamed, "Copy command to clipboard.");
            this.btnCopyCommandTamed.UseVisualStyleBackColor = true;
            this.btnCopyCommandTamed.Click += new System.EventHandler(this.btnCopyCommandTamed_Click);
            // 
            // chkCryo
            // 
            this.chkCryo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCryo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCryo.BackgroundImage = global::ARKViewer.Properties.Resources.button_cryooff;
            this.chkCryo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkCryo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCryo.Location = new System.Drawing.Point(757, 21);
            this.chkCryo.Name = "chkCryo";
            this.chkCryo.Size = new System.Drawing.Size(40, 40);
            this.chkCryo.TabIndex = 6;
            this.toolTip1.SetToolTip(this.chkCryo, "Show / hide stored tames.");
            this.chkCryo.UseVisualStyleBackColor = true;
            this.chkCryo.CheckedChanged += new System.EventHandler(this.chkCryo_CheckedChanged);
            // 
            // btnCopyCommandDropped
            // 
            this.btnCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandDropped.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyCommandDropped.Enabled = false;
            this.btnCopyCommandDropped.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandDropped.Location = new System.Drawing.Point(350, 412);
            this.btnCopyCommandDropped.Name = "btnCopyCommandDropped";
            this.btnCopyCommandDropped.Size = new System.Drawing.Size(40, 40);
            this.btnCopyCommandDropped.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnCopyCommandDropped, "Copy command to clipboard.");
            this.btnCopyCommandDropped.UseVisualStyleBackColor = true;
            this.btnCopyCommandDropped.Click += new System.EventHandler(this.btnCopyCommandDropped_Click);
            // 
            // btnTribeCopyCommand
            // 
            this.btnTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTribeCopyCommand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTribeCopyCommand.Enabled = false;
            this.btnTribeCopyCommand.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnTribeCopyCommand.Location = new System.Drawing.Point(350, 415);
            this.btnTribeCopyCommand.Name = "btnTribeCopyCommand";
            this.btnTribeCopyCommand.Size = new System.Drawing.Size(40, 40);
            this.btnTribeCopyCommand.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnTribeCopyCommand, "Copy command to clipboard.");
            this.btnTribeCopyCommand.UseVisualStyleBackColor = true;
            this.btnTribeCopyCommand.Click += new System.EventHandler(this.btnTribeCopyCommand_Click);
            // 
            // btnTribeLog
            // 
            this.btnTribeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTribeLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTribeLog.Enabled = false;
            this.btnTribeLog.Image = global::ARKViewer.Properties.Resources.TribeLogs;
            this.btnTribeLog.Location = new System.Drawing.Point(394, 415);
            this.btnTribeLog.Name = "btnTribeLog";
            this.btnTribeLog.Size = new System.Drawing.Size(40, 40);
            this.btnTribeLog.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnTribeLog, "View tribe log.");
            this.btnTribeLog.UseVisualStyleBackColor = true;
            this.btnTribeLog.Click += new System.EventHandler(this.btnTribeLog_Click);
            // 
            // btnStructureInventory
            // 
            this.btnStructureInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStructureInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStructureInventory.Enabled = false;
            this.btnStructureInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnStructureInventory.Image")));
            this.btnStructureInventory.Location = new System.Drawing.Point(388, 410);
            this.btnStructureInventory.Name = "btnStructureInventory";
            this.btnStructureInventory.Size = new System.Drawing.Size(40, 40);
            this.btnStructureInventory.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btnStructureInventory, "View inventory.");
            this.btnStructureInventory.UseVisualStyleBackColor = true;
            this.btnStructureInventory.Click += new System.EventHandler(this.btnStructureInventory_Click);
            // 
            // btnDeletePlayer
            // 
            this.btnDeletePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeletePlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeletePlayer.Enabled = false;
            this.btnDeletePlayer.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnDeletePlayer.Location = new System.Drawing.Point(479, 412);
            this.btnDeletePlayer.Name = "btnDeletePlayer";
            this.btnDeletePlayer.Size = new System.Drawing.Size(40, 40);
            this.btnDeletePlayer.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnDeletePlayer, "Remove player.");
            this.btnDeletePlayer.UseVisualStyleBackColor = true;
            this.btnDeletePlayer.Click += new System.EventHandler(this.btnDeletePlayer_Click);
            // 
            // btnDropInventory
            // 
            this.btnDropInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDropInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDropInventory.Enabled = false;
            this.btnDropInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnDropInventory.Image")));
            this.btnDropInventory.Location = new System.Drawing.Point(394, 412);
            this.btnDropInventory.Name = "btnDropInventory";
            this.btnDropInventory.Size = new System.Drawing.Size(40, 40);
            this.btnDropInventory.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnDropInventory, "View inventory.");
            this.btnDropInventory.UseVisualStyleBackColor = true;
            this.btnDropInventory.Click += new System.EventHandler(this.btnDropInventory_Click);
            // 
            // btnViewMap
            // 
            this.btnViewMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewMap.Image = global::ARKViewer.Properties.Resources.ModernXP_73_Globe_icon;
            this.btnViewMap.Location = new System.Drawing.Point(720, 564);
            this.btnViewMap.Name = "btnViewMap";
            this.btnViewMap.Size = new System.Drawing.Size(50, 50);
            this.btnViewMap.TabIndex = 3;
            this.btnViewMap.TabStop = false;
            this.toolTip1.SetToolTip(this.btnViewMap, "Show map image view.");
            this.btnViewMap.UseVisualStyleBackColor = true;
            this.btnViewMap.Click += new System.EventHandler(this.btnViewMap_Click);
            // 
            // tabFeatures
            // 
            this.tabFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFeatures.Controls.Add(this.tpgWild);
            this.tabFeatures.Controls.Add(this.tpgTamed);
            this.tabFeatures.Controls.Add(this.tpgStructures);
            this.tabFeatures.Controls.Add(this.tpgTribes);
            this.tabFeatures.Controls.Add(this.tpgPlayers);
            this.tabFeatures.Controls.Add(this.tpgDroppedItems);
            this.tabFeatures.Controls.Add(this.tpgItemList);
            this.tabFeatures.HotTrack = true;
            this.tabFeatures.Location = new System.Drawing.Point(9, 69);
            this.tabFeatures.Name = "tabFeatures";
            this.tabFeatures.SelectedIndex = 0;
            this.tabFeatures.Size = new System.Drawing.Size(819, 489);
            this.tabFeatures.TabIndex = 0;
            this.tabFeatures.SelectedIndexChanged += new System.EventHandler(this.tabFeatures_SelectedIndexChanged);
            // 
            // tpgWild
            // 
            this.tpgWild.Controls.Add(this.cboWildResource);
            this.tpgWild.Controls.Add(this.lblResource);
            this.tpgWild.Controls.Add(this.lblWildRadius);
            this.tpgWild.Controls.Add(this.udWildRadius);
            this.tpgWild.Controls.Add(this.lblWildLon);
            this.tpgWild.Controls.Add(this.udWildLon);
            this.tpgWild.Controls.Add(this.lblWildLat);
            this.tpgWild.Controls.Add(this.udWildLat);
            this.tpgWild.Controls.Add(this.lblWildMin);
            this.tpgWild.Controls.Add(this.lblWildMax);
            this.tpgWild.Controls.Add(this.udWildMin);
            this.tpgWild.Controls.Add(this.udWildMax);
            this.tpgWild.Controls.Add(this.btnCopyCommandWild);
            this.tpgWild.Controls.Add(this.lblWildCommand);
            this.tpgWild.Controls.Add(this.cboConsoleCommandsWild);
            this.tpgWild.Controls.Add(this.lblSelectedWildTotal);
            this.tpgWild.Controls.Add(this.lblWildClass);
            this.tpgWild.Controls.Add(this.lvwWildDetail);
            this.tpgWild.Controls.Add(this.lblWildTotal);
            this.tpgWild.Controls.Add(this.cboWildClass);
            this.tpgWild.Location = new System.Drawing.Point(4, 22);
            this.tpgWild.Name = "tpgWild";
            this.tpgWild.Padding = new System.Windows.Forms.Padding(3);
            this.tpgWild.Size = new System.Drawing.Size(811, 463);
            this.tpgWild.TabIndex = 0;
            this.tpgWild.Text = "Wild Creatures";
            this.tpgWild.UseVisualStyleBackColor = true;
            // 
            // cboWildResource
            // 
            this.cboWildResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWildResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWildResource.FormattingEnabled = true;
            this.cboWildResource.Location = new System.Drawing.Point(606, 45);
            this.cboWildResource.Name = "cboWildResource";
            this.cboWildResource.Size = new System.Drawing.Size(191, 21);
            this.cboWildResource.TabIndex = 11;
            this.cboWildResource.SelectedIndexChanged += new System.EventHandler(this.cboWildResource_SelectedIndexChanged);
            // 
            // lblResource
            // 
            this.lblResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResource.AutoSize = true;
            this.lblResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResource.Location = new System.Drawing.Point(540, 48);
            this.lblResource.Name = "lblResource";
            this.lblResource.Size = new System.Drawing.Size(65, 13);
            this.lblResource.TabIndex = 10;
            this.lblResource.Text = "Resource:";
            // 
            // lblWildRadius
            // 
            this.lblWildRadius.AutoSize = true;
            this.lblWildRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildRadius.Location = new System.Drawing.Point(420, 17);
            this.lblWildRadius.Name = "lblWildRadius";
            this.lblWildRadius.Size = new System.Drawing.Size(50, 13);
            this.lblWildRadius.TabIndex = 8;
            this.lblWildRadius.Text = "Radius:";
            // 
            // udWildRadius
            // 
            this.udWildRadius.DecimalPlaces = 2;
            this.udWildRadius.Location = new System.Drawing.Point(474, 14);
            this.udWildRadius.Name = "udWildRadius";
            this.udWildRadius.Size = new System.Drawing.Size(64, 20);
            this.udWildRadius.TabIndex = 9;
            this.udWildRadius.Value = new decimal(new int[] {
            10000,
            0,
            0,
            131072});
            this.udWildRadius.ValueChanged += new System.EventHandler(this.udWildRadius_ValueChanged);
            this.udWildRadius.Enter += new System.EventHandler(this.udWildRadius_Enter);
            this.udWildRadius.MouseClick += new System.Windows.Forms.MouseEventHandler(this.udWildRadius_MouseClick);
            // 
            // lblWildLon
            // 
            this.lblWildLon.AutoSize = true;
            this.lblWildLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildLon.Location = new System.Drawing.Point(318, 17);
            this.lblWildLon.Name = "lblWildLon";
            this.lblWildLon.Size = new System.Drawing.Size(32, 13);
            this.lblWildLon.TabIndex = 6;
            this.lblWildLon.Text = "Lon:";
            // 
            // udWildLon
            // 
            this.udWildLon.DecimalPlaces = 2;
            this.udWildLon.Location = new System.Drawing.Point(354, 14);
            this.udWildLon.Name = "udWildLon";
            this.udWildLon.Size = new System.Drawing.Size(64, 20);
            this.udWildLon.TabIndex = 7;
            this.udWildLon.Value = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.udWildLon.ValueChanged += new System.EventHandler(this.udWildLon_ValueChanged);
            this.udWildLon.Enter += new System.EventHandler(this.udWildLon_Enter);
            this.udWildLon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.udWildLon_MouseClick);
            // 
            // lblWildLat
            // 
            this.lblWildLat.AutoSize = true;
            this.lblWildLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildLat.Location = new System.Drawing.Point(215, 17);
            this.lblWildLat.Name = "lblWildLat";
            this.lblWildLat.Size = new System.Drawing.Size(29, 13);
            this.lblWildLat.TabIndex = 4;
            this.lblWildLat.Text = "Lat:";
            // 
            // udWildLat
            // 
            this.udWildLat.DecimalPlaces = 2;
            this.udWildLat.Location = new System.Drawing.Point(251, 14);
            this.udWildLat.Name = "udWildLat";
            this.udWildLat.Size = new System.Drawing.Size(64, 20);
            this.udWildLat.TabIndex = 5;
            this.udWildLat.Value = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.udWildLat.ValueChanged += new System.EventHandler(this.udWildLat_ValueChanged);
            this.udWildLat.Enter += new System.EventHandler(this.udWildLat_Enter);
            this.udWildLat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.udWildLat_MouseClick);
            // 
            // lblWildMin
            // 
            this.lblWildMin.AutoSize = true;
            this.lblWildMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildMin.Location = new System.Drawing.Point(42, 17);
            this.lblWildMin.Name = "lblWildMin";
            this.lblWildMin.Size = new System.Drawing.Size(31, 13);
            this.lblWildMin.TabIndex = 0;
            this.lblWildMin.Text = "Min:";
            // 
            // lblWildMax
            // 
            this.lblWildMax.AutoSize = true;
            this.lblWildMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildMax.Location = new System.Drawing.Point(124, 17);
            this.lblWildMax.Name = "lblWildMax";
            this.lblWildMax.Size = new System.Drawing.Size(34, 13);
            this.lblWildMax.TabIndex = 2;
            this.lblWildMax.Text = "Max:";
            // 
            // udWildMin
            // 
            this.udWildMin.Location = new System.Drawing.Point(82, 14);
            this.udWildMin.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udWildMin.Name = "udWildMin";
            this.udWildMin.Size = new System.Drawing.Size(38, 20);
            this.udWildMin.TabIndex = 1;
            this.udWildMin.ValueChanged += new System.EventHandler(this.udWildMin_ValueChanged);
            this.udWildMin.Enter += new System.EventHandler(this.udWildMin_Enter);
            this.udWildMin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.udWildMin_MouseClick);
            // 
            // udWildMax
            // 
            this.udWildMax.Location = new System.Drawing.Point(164, 14);
            this.udWildMax.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udWildMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWildMax.Name = "udWildMax";
            this.udWildMax.Size = new System.Drawing.Size(48, 20);
            this.udWildMax.TabIndex = 3;
            this.udWildMax.Value = new decimal(new int[] {
            190,
            0,
            0,
            0});
            this.udWildMax.ValueChanged += new System.EventHandler(this.udWildMax_ValueChanged);
            this.udWildMax.Enter += new System.EventHandler(this.udWildMax_Enter);
            this.udWildMax.MouseClick += new System.Windows.Forms.MouseEventHandler(this.udWildMax_MouseClick);
            // 
            // lblWildCommand
            // 
            this.lblWildCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWildCommand.AutoSize = true;
            this.lblWildCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildCommand.Location = new System.Drawing.Point(14, 429);
            this.lblWildCommand.Name = "lblWildCommand";
            this.lblWildCommand.Size = new System.Drawing.Size(65, 13);
            this.lblWildCommand.TabIndex = 15;
            this.lblWildCommand.Text = "Command:";
            // 
            // cboConsoleCommandsWild
            // 
            this.cboConsoleCommandsWild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsWild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsWild.FormattingEnabled = true;
            this.cboConsoleCommandsWild.Items.AddRange(new object[] {
            "DestroyAll <ClassName>",
            "GMSummon \"<ClassName>\" <Level> ",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsWild.Location = new System.Drawing.Point(82, 426);
            this.cboConsoleCommandsWild.Name = "cboConsoleCommandsWild";
            this.cboConsoleCommandsWild.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsWild.TabIndex = 16;
            // 
            // lblSelectedWildTotal
            // 
            this.lblSelectedWildTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedWildTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblSelectedWildTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedWildTotal.Location = new System.Drawing.Point(541, 422);
            this.lblSelectedWildTotal.Name = "lblSelectedWildTotal";
            this.lblSelectedWildTotal.Size = new System.Drawing.Size(123, 30);
            this.lblSelectedWildTotal.TabIndex = 18;
            this.lblSelectedWildTotal.Text = "Count: 0";
            this.lblSelectedWildTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWildClass
            // 
            this.lblWildClass.AutoSize = true;
            this.lblWildClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildClass.Location = new System.Drawing.Point(14, 48);
            this.lblWildClass.Name = "lblWildClass";
            this.lblWildClass.Size = new System.Drawing.Size(59, 13);
            this.lblWildClass.TabIndex = 12;
            this.lblWildClass.Text = "Creature:";
            // 
            // tpgTamed
            // 
            this.tpgTamed.Controls.Add(this.cboTamedResource);
            this.tpgTamed.Controls.Add(this.label1);
            this.tpgTamed.Controls.Add(this.chkCryo);
            this.tpgTamed.Controls.Add(this.btnCopyCommandTamed);
            this.tpgTamed.Controls.Add(this.lblTamedCommand);
            this.tpgTamed.Controls.Add(this.cboConsoleCommandsTamed);
            this.tpgTamed.Controls.Add(this.cboTameTribes);
            this.tpgTamed.Controls.Add(this.cboTamePlayers);
            this.tpgTamed.Controls.Add(this.lblTameCreature);
            this.tpgTamed.Controls.Add(this.lblTamePlayer);
            this.tpgTamed.Controls.Add(this.lblTameTribe);
            this.tpgTamed.Controls.Add(this.btnDinoAncestors);
            this.tpgTamed.Controls.Add(this.btnDinoInventory);
            this.tpgTamed.Controls.Add(this.lvwTameDetail);
            this.tpgTamed.Controls.Add(this.lblTameTotal);
            this.tpgTamed.Controls.Add(this.panel3);
            this.tpgTamed.Controls.Add(this.cboTameClass);
            this.tpgTamed.Location = new System.Drawing.Point(4, 22);
            this.tpgTamed.Name = "tpgTamed";
            this.tpgTamed.Size = new System.Drawing.Size(811, 463);
            this.tpgTamed.TabIndex = 3;
            this.tpgTamed.Text = "Tamed Creatures";
            this.tpgTamed.UseVisualStyleBackColor = true;
            // 
            // lblTamedCommand
            // 
            this.lblTamedCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTamedCommand.AutoSize = true;
            this.lblTamedCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamedCommand.Location = new System.Drawing.Point(192, 427);
            this.lblTamedCommand.Name = "lblTamedCommand";
            this.lblTamedCommand.Size = new System.Drawing.Size(65, 13);
            this.lblTamedCommand.TabIndex = 9;
            this.lblTamedCommand.Text = "Command:";
            // 
            // cboConsoleCommandsTamed
            // 
            this.cboConsoleCommandsTamed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsTamed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsTamed.FormattingEnabled = true;
            this.cboConsoleCommandsTamed.Items.AddRange(new object[] {
            "DestroyTribeIdDinos <TribeID>",
            "GMSummon \"<ClassName>\"  <Level> | <DoTame>",
            "GMSummon \"<ClassName>\"  <Level>",
            "TakeTribe <TribeID>",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsTamed.Location = new System.Drawing.Point(260, 424);
            this.cboConsoleCommandsTamed.Name = "cboConsoleCommandsTamed";
            this.cboConsoleCommandsTamed.Size = new System.Drawing.Size(248, 21);
            this.cboConsoleCommandsTamed.TabIndex = 10;
            // 
            // cboTameTribes
            // 
            this.cboTameTribes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTameTribes.FormattingEnabled = true;
            this.cboTameTribes.Location = new System.Drawing.Point(76, 16);
            this.cboTameTribes.Name = "cboTameTribes";
            this.cboTameTribes.Size = new System.Drawing.Size(199, 21);
            this.cboTameTribes.TabIndex = 1;
            this.cboTameTribes.SelectedIndexChanged += new System.EventHandler(this.cboTameTribes_SelectedIndexChanged);
            // 
            // cboTamePlayers
            // 
            this.cboTamePlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTamePlayers.FormattingEnabled = true;
            this.cboTamePlayers.Location = new System.Drawing.Point(76, 43);
            this.cboTamePlayers.Name = "cboTamePlayers";
            this.cboTamePlayers.Size = new System.Drawing.Size(199, 21);
            this.cboTamePlayers.TabIndex = 3;
            this.cboTamePlayers.SelectedIndexChanged += new System.EventHandler(this.cboTamePlayers_SelectedIndexChanged);
            // 
            // lblTameCreature
            // 
            this.lblTameCreature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTameCreature.AutoSize = true;
            this.lblTameCreature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameCreature.Location = new System.Drawing.Point(442, 18);
            this.lblTameCreature.Name = "lblTameCreature";
            this.lblTameCreature.Size = new System.Drawing.Size(59, 13);
            this.lblTameCreature.TabIndex = 4;
            this.lblTameCreature.Text = "Creature:";
            // 
            // lblTamePlayer
            // 
            this.lblTamePlayer.AutoSize = true;
            this.lblTamePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamePlayer.Location = new System.Drawing.Point(18, 45);
            this.lblTamePlayer.Name = "lblTamePlayer";
            this.lblTamePlayer.Size = new System.Drawing.Size(46, 13);
            this.lblTamePlayer.TabIndex = 2;
            this.lblTamePlayer.Text = "Player:";
            // 
            // lblTameTribe
            // 
            this.lblTameTribe.AutoSize = true;
            this.lblTameTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameTribe.Location = new System.Drawing.Point(18, 18);
            this.lblTameTribe.Name = "lblTameTribe";
            this.lblTameTribe.Size = new System.Drawing.Size(40, 13);
            this.lblTameTribe.TabIndex = 0;
            this.lblTameTribe.Text = "Tribe:";
            // 
            // lvwTameDetail
            // 
            this.lvwTameDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTameDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwTameDetail_Creature,
            this.lvwTameDetail_Name,
            this.lvwTameDetail_Sex,
            this.lvwTameDetail_Base,
            this.lvwTameDetail_Level,
            this.lvwTameDetail_Lat,
            this.lvwTameDetail_Lon,
            this.lvwTameDetail_HP,
            this.lvwTameDetail_Stam,
            this.lvwTameDetail_Melee,
            this.lvwTameDetail_Weight,
            this.lvwTameDetail_Speed,
            this.lvwTameDetail_Food,
            this.lvwTameDetail_Oxygen,
            this.lvwTameDetail_Craft,
            this.lvwTameDetail_Server,
            this.lvwTameDetail_Tamer,
            this.lvwTameDetail_Imprinter,
            this.lvwTameDetail_Imprint,
            this.lvwTameDetail_Cryo,
            this.lvwTameDetail_Colour1,
            this.lvwTameDetail_Colour2,
            this.lvwTameDetail_Colour3,
            this.lvwTameDetail_Colour4,
            this.lvwTameDetail_Colour5,
            this.lvwTameDetail_Colour6,
            this.lvwTameDetail_MutationsFemale,
            this.lvwTameDetail_MutationsMale,
            this.lvwTameDetail_Id});
            this.lvwTameDetail.ContextMenuStrip = this.mnuContext;
            this.lvwTameDetail.FullRowSelect = true;
            this.lvwTameDetail.HideSelection = false;
            this.lvwTameDetail.Location = new System.Drawing.Point(11, 79);
            this.lvwTameDetail.MultiSelect = false;
            this.lvwTameDetail.Name = "lvwTameDetail";
            this.lvwTameDetail.Size = new System.Drawing.Size(786, 325);
            this.lvwTameDetail.TabIndex = 7;
            this.lvwTameDetail.UseCompatibleStateImageBehavior = false;
            this.lvwTameDetail.View = System.Windows.Forms.View.Details;
            this.lvwTameDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTameDetail_ColumnClick);
            this.lvwTameDetail.SelectedIndexChanged += new System.EventHandler(this.lvwTameDetail_SelectedIndexChanged);
            this.lvwTameDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwTameDetail_MouseClick);
            // 
            // lvwTameDetail_Creature
            // 
            this.lvwTameDetail_Creature.Text = "Creature";
            this.lvwTameDetail_Creature.Width = 168;
            // 
            // lvwTameDetail_Name
            // 
            this.lvwTameDetail_Name.Text = "Name";
            this.lvwTameDetail_Name.Width = 150;
            // 
            // lvwTameDetail_Sex
            // 
            this.lvwTameDetail_Sex.Text = "Sex";
            // 
            // lvwTameDetail_Base
            // 
            this.lvwTameDetail_Base.Text = "Base";
            this.lvwTameDetail_Base.Width = 50;
            // 
            // lvwTameDetail_Level
            // 
            this.lvwTameDetail_Level.Text = "Lvl";
            this.lvwTameDetail_Level.Width = 41;
            // 
            // lvwTameDetail_Lat
            // 
            this.lvwTameDetail_Lat.Text = "Lat";
            this.lvwTameDetail_Lat.Width = 58;
            // 
            // lvwTameDetail_Lon
            // 
            this.lvwTameDetail_Lon.Text = "Lon";
            this.lvwTameDetail_Lon.Width = 57;
            // 
            // lvwTameDetail_HP
            // 
            this.lvwTameDetail_HP.Text = "HP";
            this.lvwTameDetail_HP.Width = 45;
            // 
            // lvwTameDetail_Stam
            // 
            this.lvwTameDetail_Stam.Text = "Stam";
            this.lvwTameDetail_Stam.Width = 45;
            // 
            // lvwTameDetail_Melee
            // 
            this.lvwTameDetail_Melee.Text = "Melee";
            this.lvwTameDetail_Melee.Width = 48;
            // 
            // lvwTameDetail_Weight
            // 
            this.lvwTameDetail_Weight.Text = "Weight";
            this.lvwTameDetail_Weight.Width = 55;
            // 
            // lvwTameDetail_Speed
            // 
            this.lvwTameDetail_Speed.Text = "Speed";
            this.lvwTameDetail_Speed.Width = 50;
            // 
            // lvwTameDetail_Food
            // 
            this.lvwTameDetail_Food.Text = "Food";
            this.lvwTameDetail_Food.Width = 47;
            // 
            // lvwTameDetail_Oxygen
            // 
            this.lvwTameDetail_Oxygen.Text = "Oxygen";
            this.lvwTameDetail_Oxygen.Width = 53;
            // 
            // lvwTameDetail_Craft
            // 
            this.lvwTameDetail_Craft.Text = "Craft";
            this.lvwTameDetail_Craft.Width = 50;
            // 
            // lvwTameDetail_Server
            // 
            this.lvwTameDetail_Server.Text = "Server";
            this.lvwTameDetail_Server.Width = 150;
            // 
            // lvwTameDetail_Tamer
            // 
            this.lvwTameDetail_Tamer.Text = "Tamer";
            this.lvwTameDetail_Tamer.Width = 105;
            // 
            // lvwTameDetail_Imprinter
            // 
            this.lvwTameDetail_Imprinter.Text = "Imprinter";
            this.lvwTameDetail_Imprinter.Width = 105;
            // 
            // lvwTameDetail_Imprint
            // 
            this.lvwTameDetail_Imprint.Text = "Imprint";
            // 
            // lvwTameDetail_Cryo
            // 
            this.lvwTameDetail_Cryo.Text = "Stored";
            // 
            // lvwTameDetail_Colour1
            // 
            this.lvwTameDetail_Colour1.Text = "C0";
            this.lvwTameDetail_Colour1.Width = 35;
            // 
            // lvwTameDetail_Colour2
            // 
            this.lvwTameDetail_Colour2.Text = "C1";
            this.lvwTameDetail_Colour2.Width = 35;
            // 
            // lvwTameDetail_Colour3
            // 
            this.lvwTameDetail_Colour3.Text = "C2";
            this.lvwTameDetail_Colour3.Width = 35;
            // 
            // lvwTameDetail_Colour4
            // 
            this.lvwTameDetail_Colour4.Text = "C3";
            this.lvwTameDetail_Colour4.Width = 35;
            // 
            // lvwTameDetail_Colour5
            // 
            this.lvwTameDetail_Colour5.Text = "C4";
            this.lvwTameDetail_Colour5.Width = 35;
            // 
            // lvwTameDetail_Colour6
            // 
            this.lvwTameDetail_Colour6.Text = "C5";
            this.lvwTameDetail_Colour6.Width = 35;
            // 
            // lvwTameDetail_MutationsFemale
            // 
            this.lvwTameDetail_MutationsFemale.Text = "Mut (F)";
            // 
            // lvwTameDetail_MutationsMale
            // 
            this.lvwTameDetail_MutationsMale.Text = "Mut (M)";
            // 
            // lvwTameDetail_Id
            // 
            this.lvwTameDetail_Id.Text = "Id";
            this.lvwTameDetail_Id.Width = 0;
            // 
            // lblTameTotal
            // 
            this.lblTameTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTameTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblTameTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameTotal.Location = new System.Drawing.Point(674, 420);
            this.lblTameTotal.Name = "lblTameTotal";
            this.lblTameTotal.Size = new System.Drawing.Size(123, 30);
            this.lblTameTotal.TabIndex = 14;
            this.lblTameTotal.Text = "Count: 0";
            this.lblTameTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblStats);
            this.panel3.Controls.Add(this.optStatsTamed);
            this.panel3.Controls.Add(this.optStatsBase);
            this.panel3.Location = new System.Drawing.Point(11, 414);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 34);
            this.panel3.TabIndex = 8;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(2, 10);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(34, 13);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "Stats:";
            // 
            // optStatsTamed
            // 
            this.optStatsTamed.AutoSize = true;
            this.optStatsTamed.Location = new System.Drawing.Point(97, 8);
            this.optStatsTamed.Name = "optStatsTamed";
            this.optStatsTamed.Size = new System.Drawing.Size(58, 17);
            this.optStatsTamed.TabIndex = 2;
            this.optStatsTamed.Text = "Tamed";
            this.optStatsTamed.UseVisualStyleBackColor = true;
            // 
            // optStatsBase
            // 
            this.optStatsBase.AutoSize = true;
            this.optStatsBase.Checked = true;
            this.optStatsBase.Location = new System.Drawing.Point(42, 8);
            this.optStatsBase.Name = "optStatsBase";
            this.optStatsBase.Size = new System.Drawing.Size(49, 17);
            this.optStatsBase.TabIndex = 1;
            this.optStatsBase.TabStop = true;
            this.optStatsBase.Text = "Base";
            this.optStatsBase.UseVisualStyleBackColor = true;
            this.optStatsBase.CheckedChanged += new System.EventHandler(this.optStatsBase_CheckedChanged);
            // 
            // cboTameClass
            // 
            this.cboTameClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTameClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTameClass.FormattingEnabled = true;
            this.cboTameClass.Location = new System.Drawing.Point(520, 16);
            this.cboTameClass.Name = "cboTameClass";
            this.cboTameClass.Size = new System.Drawing.Size(216, 21);
            this.cboTameClass.TabIndex = 5;
            this.cboTameClass.SelectedIndexChanged += new System.EventHandler(this.cboTameClass_SelectedIndexChanged);
            // 
            // tpgStructures
            // 
            this.tpgStructures.Controls.Add(this.btnStructureInventory);
            this.tpgStructures.Controls.Add(this.lblStructureTotal);
            this.tpgStructures.Controls.Add(this.btnCopyCommandStructure);
            this.tpgStructures.Controls.Add(this.lblCommandStructure);
            this.tpgStructures.Controls.Add(this.cboConsoleCommandsStructure);
            this.tpgStructures.Controls.Add(this.btnStructureExclusionFilter);
            this.tpgStructures.Controls.Add(this.lblStructureStructure);
            this.tpgStructures.Controls.Add(this.cboStructureStructure);
            this.tpgStructures.Controls.Add(this.lblStructurePlayer);
            this.tpgStructures.Controls.Add(this.lblStructureTribe);
            this.tpgStructures.Controls.Add(this.cboStructureTribe);
            this.tpgStructures.Controls.Add(this.cboStructurePlayer);
            this.tpgStructures.Controls.Add(this.lvwStructureLocations);
            this.tpgStructures.Location = new System.Drawing.Point(4, 22);
            this.tpgStructures.Name = "tpgStructures";
            this.tpgStructures.Size = new System.Drawing.Size(811, 463);
            this.tpgStructures.TabIndex = 2;
            this.tpgStructures.Text = "Player Structures";
            this.tpgStructures.UseVisualStyleBackColor = true;
            // 
            // lblStructureTotal
            // 
            this.lblStructureTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStructureTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblStructureTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureTotal.Location = new System.Drawing.Point(675, 417);
            this.lblStructureTotal.Name = "lblStructureTotal";
            this.lblStructureTotal.Size = new System.Drawing.Size(123, 30);
            this.lblStructureTotal.TabIndex = 12;
            this.lblStructureTotal.Text = "Total: 0";
            this.lblStructureTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommandStructure
            // 
            this.lblCommandStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommandStructure.AutoSize = true;
            this.lblCommandStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandStructure.Location = new System.Drawing.Point(9, 421);
            this.lblCommandStructure.Name = "lblCommandStructure";
            this.lblCommandStructure.Size = new System.Drawing.Size(65, 13);
            this.lblCommandStructure.TabIndex = 8;
            this.lblCommandStructure.Text = "Command:";
            // 
            // cboConsoleCommandsStructure
            // 
            this.cboConsoleCommandsStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsStructure.FormattingEnabled = true;
            this.cboConsoleCommandsStructure.Items.AddRange(new object[] {
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "TakeTribe <TribeID>",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsStructure.Location = new System.Drawing.Point(77, 418);
            this.cboConsoleCommandsStructure.Name = "cboConsoleCommandsStructure";
            this.cboConsoleCommandsStructure.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsStructure.TabIndex = 9;
            this.cboConsoleCommandsStructure.SelectedIndexChanged += new System.EventHandler(this.cboConsoleCommandsStructure_SelectedIndexChanged);
            // 
            // lblStructureStructure
            // 
            this.lblStructureStructure.AutoSize = true;
            this.lblStructureStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureStructure.Location = new System.Drawing.Point(483, 18);
            this.lblStructureStructure.Name = "lblStructureStructure";
            this.lblStructureStructure.Size = new System.Drawing.Size(63, 13);
            this.lblStructureStructure.TabIndex = 4;
            this.lblStructureStructure.Text = "Structure:";
            // 
            // cboStructureStructure
            // 
            this.cboStructureStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStructureStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructureStructure.FormattingEnabled = true;
            this.cboStructureStructure.Location = new System.Drawing.Point(553, 16);
            this.cboStructureStructure.Name = "cboStructureStructure";
            this.cboStructureStructure.Size = new System.Drawing.Size(197, 21);
            this.cboStructureStructure.TabIndex = 5;
            this.cboStructureStructure.SelectedIndexChanged += new System.EventHandler(this.cboStructureStructure_SelectedIndexChanged);
            // 
            // lblStructurePlayer
            // 
            this.lblStructurePlayer.AutoSize = true;
            this.lblStructurePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructurePlayer.Location = new System.Drawing.Point(229, 18);
            this.lblStructurePlayer.Name = "lblStructurePlayer";
            this.lblStructurePlayer.Size = new System.Drawing.Size(46, 13);
            this.lblStructurePlayer.TabIndex = 2;
            this.lblStructurePlayer.Text = "Player:";
            // 
            // lblStructureTribe
            // 
            this.lblStructureTribe.AutoSize = true;
            this.lblStructureTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureTribe.Location = new System.Drawing.Point(14, 18);
            this.lblStructureTribe.Name = "lblStructureTribe";
            this.lblStructureTribe.Size = new System.Drawing.Size(40, 13);
            this.lblStructureTribe.TabIndex = 0;
            this.lblStructureTribe.Text = "Tribe:";
            // 
            // cboStructureTribe
            // 
            this.cboStructureTribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructureTribe.FormattingEnabled = true;
            this.cboStructureTribe.Location = new System.Drawing.Point(57, 16);
            this.cboStructureTribe.Name = "cboStructureTribe";
            this.cboStructureTribe.Size = new System.Drawing.Size(166, 21);
            this.cboStructureTribe.TabIndex = 1;
            this.cboStructureTribe.SelectedIndexChanged += new System.EventHandler(this.cboStructureTribe_SelectedIndexChanged);
            // 
            // cboStructurePlayer
            // 
            this.cboStructurePlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructurePlayer.FormattingEnabled = true;
            this.cboStructurePlayer.Location = new System.Drawing.Point(281, 16);
            this.cboStructurePlayer.Name = "cboStructurePlayer";
            this.cboStructurePlayer.Size = new System.Drawing.Size(193, 21);
            this.cboStructurePlayer.TabIndex = 3;
            this.cboStructurePlayer.SelectedIndexChanged += new System.EventHandler(this.cboStructurePlayer_SelectedIndexChanged);
            // 
            // lvwStructureLocations
            // 
            this.lvwStructureLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwStructureLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwStructureLocations_Tribe,
            this.lvwStructureLocations_Structure,
            this.lvwStructureLocations_Lat,
            this.lvwStructureLocations_Lon});
            this.lvwStructureLocations.ContextMenuStrip = this.mnuContext;
            this.lvwStructureLocations.FullRowSelect = true;
            this.lvwStructureLocations.HideSelection = false;
            this.lvwStructureLocations.Location = new System.Drawing.Point(12, 51);
            this.lvwStructureLocations.MultiSelect = false;
            this.lvwStructureLocations.Name = "lvwStructureLocations";
            this.lvwStructureLocations.Size = new System.Drawing.Size(786, 353);
            this.lvwStructureLocations.TabIndex = 7;
            this.lvwStructureLocations.UseCompatibleStateImageBehavior = false;
            this.lvwStructureLocations.View = System.Windows.Forms.View.Details;
            this.lvwStructureLocations.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwStructureLocations_ColumnClick);
            this.lvwStructureLocations.SelectedIndexChanged += new System.EventHandler(this.lvwStructureLocations_SelectedIndexChanged);
            this.lvwStructureLocations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwStructureLocations_MouseClick);
            // 
            // lvwStructureLocations_Tribe
            // 
            this.lvwStructureLocations_Tribe.Text = "Tribe";
            this.lvwStructureLocations_Tribe.Width = 236;
            // 
            // lvwStructureLocations_Structure
            // 
            this.lvwStructureLocations_Structure.Text = "Structure";
            this.lvwStructureLocations_Structure.Width = 219;
            // 
            // lvwStructureLocations_Lat
            // 
            this.lvwStructureLocations_Lat.Text = "Lat";
            this.lvwStructureLocations_Lat.Width = 79;
            // 
            // lvwStructureLocations_Lon
            // 
            this.lvwStructureLocations_Lon.Text = "Lon";
            this.lvwStructureLocations_Lon.Width = 71;
            // 
            // tpgTribes
            // 
            this.tpgTribes.Controls.Add(this.chkTribeStructures);
            this.tpgTribes.Controls.Add(this.chkTribeTames);
            this.tpgTribes.Controls.Add(this.chkTribePlayers);
            this.tpgTribes.Controls.Add(this.btnTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.lblTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.cboTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.btnTribeLog);
            this.tpgTribes.Controls.Add(this.lvwTribes);
            this.tpgTribes.Location = new System.Drawing.Point(4, 22);
            this.tpgTribes.Name = "tpgTribes";
            this.tpgTribes.Size = new System.Drawing.Size(811, 463);
            this.tpgTribes.TabIndex = 5;
            this.tpgTribes.Text = "Tribes";
            this.tpgTribes.UseVisualStyleBackColor = true;
            // 
            // chkTribeStructures
            // 
            this.chkTribeStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribeStructures.BackColor = System.Drawing.Color.PaleGreen;
            this.chkTribeStructures.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribeStructures.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTribeStructures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribeStructures.ForeColor = System.Drawing.Color.ForestGreen;
            this.chkTribeStructures.Location = new System.Drawing.Point(677, 418);
            this.chkTribeStructures.Name = "chkTribeStructures";
            this.chkTribeStructures.Size = new System.Drawing.Size(121, 35);
            this.chkTribeStructures.TabIndex = 7;
            this.chkTribeStructures.Text = "Structure Markers";
            this.chkTribeStructures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkTribeStructures, "Show structure markers.");
            this.chkTribeStructures.UseVisualStyleBackColor = false;
            this.chkTribeStructures.CheckedChanged += new System.EventHandler(this.chkTribeStructures_CheckedChanged);
            // 
            // chkTribeTames
            // 
            this.chkTribeTames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribeTames.BackColor = System.Drawing.Color.Gold;
            this.chkTribeTames.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribeTames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTribeTames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribeTames.ForeColor = System.Drawing.Color.Chocolate;
            this.chkTribeTames.Location = new System.Drawing.Point(566, 418);
            this.chkTribeTames.Name = "chkTribeTames";
            this.chkTribeTames.Size = new System.Drawing.Size(105, 35);
            this.chkTribeTames.TabIndex = 6;
            this.chkTribeTames.Text = "Tame Markers";
            this.chkTribeTames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkTribeTames, "Show tame markers.");
            this.chkTribeTames.UseVisualStyleBackColor = false;
            this.chkTribeTames.CheckedChanged += new System.EventHandler(this.chkTribeTames_CheckedChanged);
            // 
            // chkTribePlayers
            // 
            this.chkTribePlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribePlayers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.chkTribePlayers.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribePlayers.Checked = true;
            this.chkTribePlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTribePlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTribePlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribePlayers.ForeColor = System.Drawing.Color.LightCyan;
            this.chkTribePlayers.Location = new System.Drawing.Point(446, 418);
            this.chkTribePlayers.Name = "chkTribePlayers";
            this.chkTribePlayers.Size = new System.Drawing.Size(114, 35);
            this.chkTribePlayers.TabIndex = 5;
            this.chkTribePlayers.Text = "Player Markers";
            this.chkTribePlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkTribePlayers, "Show player markers.");
            this.chkTribePlayers.UseVisualStyleBackColor = false;
            this.chkTribePlayers.CheckedChanged += new System.EventHandler(this.chkTribePlayers_CheckedChanged);
            // 
            // lblTribeCopyCommand
            // 
            this.lblTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTribeCopyCommand.AutoSize = true;
            this.lblTribeCopyCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTribeCopyCommand.Location = new System.Drawing.Point(14, 427);
            this.lblTribeCopyCommand.Name = "lblTribeCopyCommand";
            this.lblTribeCopyCommand.Size = new System.Drawing.Size(65, 13);
            this.lblTribeCopyCommand.TabIndex = 1;
            this.lblTribeCopyCommand.Text = "Command:";
            // 
            // cboTribeCopyCommand
            // 
            this.cboTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboTribeCopyCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTribeCopyCommand.FormattingEnabled = true;
            this.cboTribeCopyCommand.Items.AddRange(new object[] {
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "RenameTribe \"<TribeName>\" ",
            "TakeTribe <TribeID>",
            "TribeStructureAudit <TribeID>",
            "TribeDinoAudit  <TribeID>",
            "RM <FileCsvList>",
            "DEL <FileCsvList>"});
            this.cboTribeCopyCommand.Location = new System.Drawing.Point(82, 424);
            this.cboTribeCopyCommand.Name = "cboTribeCopyCommand";
            this.cboTribeCopyCommand.Size = new System.Drawing.Size(262, 21);
            this.cboTribeCopyCommand.TabIndex = 2;
            // 
            // lvwTribes
            // 
            this.lvwTribes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTribes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lvwTribes.ContextMenuStrip = this.mnuContext;
            this.lvwTribes.FullRowSelect = true;
            this.lvwTribes.HideSelection = false;
            this.lvwTribes.Location = new System.Drawing.Point(12, 19);
            this.lvwTribes.Name = "lvwTribes";
            this.lvwTribes.Size = new System.Drawing.Size(786, 390);
            this.lvwTribes.TabIndex = 0;
            this.lvwTribes.UseCompatibleStateImageBehavior = false;
            this.lvwTribes.View = System.Windows.Forms.View.Details;
            this.lvwTribes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTribes_ColumnClick);
            this.lvwTribes.SelectedIndexChanged += new System.EventHandler(this.lvwTribes_SelectedIndexChanged);
            this.lvwTribes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwTribes_MouseClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Id";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Name";
            this.columnHeader11.Width = 228;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Players";
            this.columnHeader12.Width = 79;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Tames";
            this.columnHeader13.Width = 81;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Structures";
            this.columnHeader14.Width = 91;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Last Active";
            this.columnHeader15.Width = 127;
            // 
            // tpgPlayers
            // 
            this.tpgPlayers.Controls.Add(this.btmMissionScoreboard);
            this.tpgPlayers.Controls.Add(this.btnDeletePlayer);
            this.tpgPlayers.Controls.Add(this.lblPlayerTotal);
            this.tpgPlayers.Controls.Add(this.btnCopyCommandPlayer);
            this.tpgPlayers.Controls.Add(this.lblCommandPlayer);
            this.tpgPlayers.Controls.Add(this.cboConsoleCommandsPlayerTribe);
            this.tpgPlayers.Controls.Add(this.btnPlayerTribeLog);
            this.tpgPlayers.Controls.Add(this.btnPlayerInventory);
            this.tpgPlayers.Controls.Add(this.lblPlayersPlayer);
            this.tpgPlayers.Controls.Add(this.lblPlayersTribe);
            this.tpgPlayers.Controls.Add(this.cboTribes);
            this.tpgPlayers.Controls.Add(this.cboPlayers);
            this.tpgPlayers.Controls.Add(this.lvwPlayers);
            this.tpgPlayers.Location = new System.Drawing.Point(4, 22);
            this.tpgPlayers.Name = "tpgPlayers";
            this.tpgPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlayers.Size = new System.Drawing.Size(811, 463);
            this.tpgPlayers.TabIndex = 1;
            this.tpgPlayers.Text = "Players";
            this.tpgPlayers.UseVisualStyleBackColor = true;
            // 
            // btmMissionScoreboard
            // 
            this.btmMissionScoreboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btmMissionScoreboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btmMissionScoreboard.Enabled = false;
            this.btmMissionScoreboard.Image = ((System.Drawing.Image)(resources.GetObject("btmMissionScoreboard.Image")));
            this.btmMissionScoreboard.Location = new System.Drawing.Point(758, 8);
            this.btmMissionScoreboard.Name = "btmMissionScoreboard";
            this.btmMissionScoreboard.Size = new System.Drawing.Size(40, 40);
            this.btmMissionScoreboard.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btmMissionScoreboard, "View mission scoreboard");
            this.btmMissionScoreboard.UseVisualStyleBackColor = true;
            this.btmMissionScoreboard.Click += new System.EventHandler(this.btmMissionScoreboard_Click);
            // 
            // lblPlayerTotal
            // 
            this.lblPlayerTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblPlayerTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTotal.Location = new System.Drawing.Point(675, 417);
            this.lblPlayerTotal.Name = "lblPlayerTotal";
            this.lblPlayerTotal.Size = new System.Drawing.Size(123, 30);
            this.lblPlayerTotal.TabIndex = 11;
            this.lblPlayerTotal.Text = "Total: 0";
            this.lblPlayerTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommandPlayer
            // 
            this.lblCommandPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommandPlayer.AutoSize = true;
            this.lblCommandPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandPlayer.Location = new System.Drawing.Point(14, 423);
            this.lblCommandPlayer.Name = "lblCommandPlayer";
            this.lblCommandPlayer.Size = new System.Drawing.Size(65, 13);
            this.lblCommandPlayer.TabIndex = 5;
            this.lblCommandPlayer.Text = "Command:";
            // 
            // cboConsoleCommandsPlayerTribe
            // 
            this.cboConsoleCommandsPlayerTribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsPlayerTribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsPlayerTribe.FormattingEnabled = true;
            this.cboConsoleCommandsPlayerTribe.Items.AddRange(new object[] {
            "AllowPlayerToJoinNoCheck <SteamID>",
            "BanPlayer <SteamID>",
            "ClearPlayerInventory <PlayerID> true true true",
            "DefeatAllBosses <PlayerID> ",
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "DisallowPlayerToJoinNoCheck <SteamID> ",
            "GetPlayerIDForSteamID <SteamID> ",
            "GetSteamIDForPlayerID <PlayerID> ",
            "GiveCreativeModeToPlayer <PlayerID> ",
            "GiveTekengramsTo <PlayerID>",
            "KickPlayer <SteamID> ",
            "KillPlayer <PlayerID>",
            "MaxAscend <PlayerID>  ",
            "RenamePlayer \"<CharacterName>\" ",
            "RenameTribe \"<TribeName>\" ",
            "ServerChatToPlayer <PlayerName>",
            "SetImprintedPlayer \"<CharacterName>\" <PlayerID>",
            "SetPlayerPos  <x> <y> <z>",
            "TakeTribe <TribeID>",
            "TeleportPlayerIDToMe <PlayerID>",
            "TeleportPlayerNameToMe <CharacterName>",
            "TeleportToPlayer <PlayerID>",
            "TeleportToPlayerName <CharacterName",
            "TribeStructureAudit <TribeID>",
            "TribeDinoAudit  <TribeID>",
            "UnbanPlayer <SteamID>",
            "RM <FileCsvList>",
            "DEL <FileCsvList>"});
            this.cboConsoleCommandsPlayerTribe.Location = new System.Drawing.Point(82, 420);
            this.cboConsoleCommandsPlayerTribe.Name = "cboConsoleCommandsPlayerTribe";
            this.cboConsoleCommandsPlayerTribe.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsPlayerTribe.TabIndex = 6;
            this.cboConsoleCommandsPlayerTribe.SelectedIndexChanged += new System.EventHandler(this.cboConsoleCommandsPlayerTribe_SelectedIndexChanged);
            // 
            // lblPlayersPlayer
            // 
            this.lblPlayersPlayer.AutoSize = true;
            this.lblPlayersPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersPlayer.Location = new System.Drawing.Point(347, 18);
            this.lblPlayersPlayer.Name = "lblPlayersPlayer";
            this.lblPlayersPlayer.Size = new System.Drawing.Size(46, 13);
            this.lblPlayersPlayer.TabIndex = 2;
            this.lblPlayersPlayer.Text = "Player:";
            // 
            // lblPlayersTribe
            // 
            this.lblPlayersTribe.AutoSize = true;
            this.lblPlayersTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersTribe.Location = new System.Drawing.Point(14, 18);
            this.lblPlayersTribe.Name = "lblPlayersTribe";
            this.lblPlayersTribe.Size = new System.Drawing.Size(40, 13);
            this.lblPlayersTribe.TabIndex = 0;
            this.lblPlayersTribe.Text = "Tribe:";
            // 
            // cboTribes
            // 
            this.cboTribes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTribes.FormattingEnabled = true;
            this.cboTribes.Location = new System.Drawing.Point(60, 15);
            this.cboTribes.Name = "cboTribes";
            this.cboTribes.Size = new System.Drawing.Size(269, 21);
            this.cboTribes.TabIndex = 1;
            this.cboTribes.SelectedIndexChanged += new System.EventHandler(this.cboTribes_SelectedIndexChanged);
            // 
            // cboPlayers
            // 
            this.cboPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayers.FormattingEnabled = true;
            this.cboPlayers.Location = new System.Drawing.Point(399, 15);
            this.cboPlayers.Name = "cboPlayers";
            this.cboPlayers.Size = new System.Drawing.Size(260, 21);
            this.cboPlayers.TabIndex = 3;
            this.cboPlayers.SelectedIndexChanged += new System.EventHandler(this.cboPlayers_SelectedIndexChanged);
            // 
            // lvwPlayers
            // 
            this.lvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwPlayers_Name,
            this.lvwPlayers_Tribe,
            this.lvwPlayers_Sex,
            this.lvwPlayers_Level,
            this.lvwPlayers_Lat,
            this.lvwPlayers_Lon,
            this.lvwPlayers_Hp,
            this.lvwPlayers_Stam,
            this.lvwPlayers_Melee,
            this.lvwPlayers_Weight,
            this.lvwPlayers_Speed,
            this.lvwPlayers_Food,
            this.lvwPlayers_Water,
            this.lvwPlayers_Oxygen,
            this.lvwPlayers_Crafting,
            this.lvwPlayers_Fortitude,
            this.lvwPlayers_LastOnline,
            this.lvwPlayers_SteamName,
            this.lvwPlayers_SteamId});
            this.lvwPlayers.ContextMenuStrip = this.mnuContext;
            this.lvwPlayers.FullRowSelect = true;
            this.lvwPlayers.HideSelection = false;
            this.lvwPlayers.Location = new System.Drawing.Point(12, 52);
            this.lvwPlayers.Name = "lvwPlayers";
            this.lvwPlayers.Size = new System.Drawing.Size(786, 353);
            this.lvwPlayers.TabIndex = 4;
            this.lvwPlayers.UseCompatibleStateImageBehavior = false;
            this.lvwPlayers.View = System.Windows.Forms.View.Details;
            this.lvwPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPlayers_ColumnClick);
            this.lvwPlayers.SelectedIndexChanged += new System.EventHandler(this.lvwPlayers_SelectedIndexChanged);
            this.lvwPlayers.Click += new System.EventHandler(this.lvwPlayers_Click);
            this.lvwPlayers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwPlayers_MouseClick);
            // 
            // lvwPlayers_Name
            // 
            this.lvwPlayers_Name.Text = "Name";
            this.lvwPlayers_Name.Width = 90;
            // 
            // lvwPlayers_Tribe
            // 
            this.lvwPlayers_Tribe.Text = "Tribe";
            this.lvwPlayers_Tribe.Width = 90;
            // 
            // lvwPlayers_Sex
            // 
            this.lvwPlayers_Sex.Text = "Sex";
            this.lvwPlayers_Sex.Width = 55;
            // 
            // lvwPlayers_Level
            // 
            this.lvwPlayers_Level.Text = "Lvl";
            this.lvwPlayers_Level.Width = 35;
            // 
            // lvwPlayers_Lat
            // 
            this.lvwPlayers_Lat.Text = "Lat";
            this.lvwPlayers_Lat.Width = 45;
            // 
            // lvwPlayers_Lon
            // 
            this.lvwPlayers_Lon.Text = "Lon";
            this.lvwPlayers_Lon.Width = 45;
            // 
            // lvwPlayers_Hp
            // 
            this.lvwPlayers_Hp.Text = "HP";
            this.lvwPlayers_Hp.Width = 45;
            // 
            // lvwPlayers_Stam
            // 
            this.lvwPlayers_Stam.Text = "Stam";
            this.lvwPlayers_Stam.Width = 45;
            // 
            // lvwPlayers_Melee
            // 
            this.lvwPlayers_Melee.Text = "Melee";
            this.lvwPlayers_Melee.Width = 48;
            // 
            // lvwPlayers_Weight
            // 
            this.lvwPlayers_Weight.Text = "Weight";
            this.lvwPlayers_Weight.Width = 55;
            // 
            // lvwPlayers_Speed
            // 
            this.lvwPlayers_Speed.Text = "Speed";
            this.lvwPlayers_Speed.Width = 50;
            // 
            // lvwPlayers_Food
            // 
            this.lvwPlayers_Food.Text = "Food";
            this.lvwPlayers_Food.Width = 47;
            // 
            // lvwPlayers_Water
            // 
            this.lvwPlayers_Water.Text = "Water";
            // 
            // lvwPlayers_Oxygen
            // 
            this.lvwPlayers_Oxygen.Text = "Oxygen";
            this.lvwPlayers_Oxygen.Width = 53;
            // 
            // lvwPlayers_Crafting
            // 
            this.lvwPlayers_Crafting.Text = "Crafting";
            // 
            // lvwPlayers_Fortitude
            // 
            this.lvwPlayers_Fortitude.Text = "Fortitude";
            // 
            // lvwPlayers_LastOnline
            // 
            this.lvwPlayers_LastOnline.Text = "Last Online";
            this.lvwPlayers_LastOnline.Width = 140;
            // 
            // lvwPlayers_SteamName
            // 
            this.lvwPlayers_SteamName.Text = "Steam Name";
            this.lvwPlayers_SteamName.Width = 150;
            // 
            // lvwPlayers_SteamId
            // 
            this.lvwPlayers_SteamId.Text = "SteamId";
            this.lvwPlayers_SteamId.Width = 0;
            // 
            // tpgDroppedItems
            // 
            this.tpgDroppedItems.Controls.Add(this.chkDroppedBlueprints);
            this.tpgDroppedItems.Controls.Add(this.btnDropInventory);
            this.tpgDroppedItems.Controls.Add(this.cboDroppedItem);
            this.tpgDroppedItems.Controls.Add(this.lblDroppedPlayer);
            this.tpgDroppedItems.Controls.Add(this.cboDroppedPlayer);
            this.tpgDroppedItems.Controls.Add(this.btnCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.lblCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.cboCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.lblCountDropped);
            this.tpgDroppedItems.Controls.Add(this.lblDroppedItem);
            this.tpgDroppedItems.Controls.Add(this.lvwDroppedItems);
            this.tpgDroppedItems.Location = new System.Drawing.Point(4, 22);
            this.tpgDroppedItems.Name = "tpgDroppedItems";
            this.tpgDroppedItems.Size = new System.Drawing.Size(811, 463);
            this.tpgDroppedItems.TabIndex = 4;
            this.tpgDroppedItems.Text = "Dropped Items";
            this.tpgDroppedItems.UseVisualStyleBackColor = true;
            // 
            // chkDroppedBlueprints
            // 
            this.chkDroppedBlueprints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDroppedBlueprints.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDroppedBlueprints.BackgroundImage = global::ARKViewer.Properties.Resources.blueprints_unchecked;
            this.chkDroppedBlueprints.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkDroppedBlueprints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDroppedBlueprints.Location = new System.Drawing.Point(758, 5);
            this.chkDroppedBlueprints.Name = "chkDroppedBlueprints";
            this.chkDroppedBlueprints.Size = new System.Drawing.Size(40, 40);
            this.chkDroppedBlueprints.TabIndex = 10;
            this.toolTip1.SetToolTip(this.chkDroppedBlueprints, "Show / hide blueprints.");
            this.chkDroppedBlueprints.UseVisualStyleBackColor = true;
            this.chkDroppedBlueprints.CheckedChanged += new System.EventHandler(this.chkDroppedBlueprints_CheckedChanged);
            // 
            // cboDroppedItem
            // 
            this.cboDroppedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDroppedItem.FormattingEnabled = true;
            this.cboDroppedItem.Location = new System.Drawing.Point(334, 15);
            this.cboDroppedItem.Name = "cboDroppedItem";
            this.cboDroppedItem.Size = new System.Drawing.Size(213, 21);
            this.cboDroppedItem.TabIndex = 3;
            this.cboDroppedItem.SelectedIndexChanged += new System.EventHandler(this.cboDroppedItem_SelectedIndexChanged);
            // 
            // lblDroppedPlayer
            // 
            this.lblDroppedPlayer.AutoSize = true;
            this.lblDroppedPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroppedPlayer.Location = new System.Drawing.Point(17, 18);
            this.lblDroppedPlayer.Name = "lblDroppedPlayer";
            this.lblDroppedPlayer.Size = new System.Drawing.Size(46, 13);
            this.lblDroppedPlayer.TabIndex = 0;
            this.lblDroppedPlayer.Text = "Player:";
            // 
            // cboDroppedPlayer
            // 
            this.cboDroppedPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDroppedPlayer.FormattingEnabled = true;
            this.cboDroppedPlayer.Location = new System.Drawing.Point(69, 16);
            this.cboDroppedPlayer.Name = "cboDroppedPlayer";
            this.cboDroppedPlayer.Size = new System.Drawing.Size(218, 21);
            this.cboDroppedPlayer.TabIndex = 1;
            this.cboDroppedPlayer.SelectedIndexChanged += new System.EventHandler(this.cboDroppedPlayer_SelectedIndexChanged);
            // 
            // lblCopyCommandDropped
            // 
            this.lblCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyCommandDropped.AutoSize = true;
            this.lblCopyCommandDropped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyCommandDropped.Location = new System.Drawing.Point(14, 425);
            this.lblCopyCommandDropped.Name = "lblCopyCommandDropped";
            this.lblCopyCommandDropped.Size = new System.Drawing.Size(65, 13);
            this.lblCopyCommandDropped.TabIndex = 5;
            this.lblCopyCommandDropped.Text = "Command:";
            // 
            // cboCopyCommandDropped
            // 
            this.cboCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboCopyCommandDropped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCopyCommandDropped.FormattingEnabled = true;
            this.cboCopyCommandDropped.Items.AddRange(new object[] {
            "SetPlayerPos  <x> <y> <z>"});
            this.cboCopyCommandDropped.Location = new System.Drawing.Point(82, 422);
            this.cboCopyCommandDropped.Name = "cboCopyCommandDropped";
            this.cboCopyCommandDropped.Size = new System.Drawing.Size(262, 21);
            this.cboCopyCommandDropped.TabIndex = 6;
            // 
            // lblCountDropped
            // 
            this.lblCountDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountDropped.BackColor = System.Drawing.Color.PowderBlue;
            this.lblCountDropped.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDropped.Location = new System.Drawing.Point(675, 417);
            this.lblCountDropped.Name = "lblCountDropped";
            this.lblCountDropped.Size = new System.Drawing.Size(123, 30);
            this.lblCountDropped.TabIndex = 9;
            this.lblCountDropped.Text = "Count: 0";
            this.lblCountDropped.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDroppedItem
            // 
            this.lblDroppedItem.AutoSize = true;
            this.lblDroppedItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroppedItem.Location = new System.Drawing.Point(293, 19);
            this.lblDroppedItem.Name = "lblDroppedItem";
            this.lblDroppedItem.Size = new System.Drawing.Size(35, 13);
            this.lblDroppedItem.TabIndex = 2;
            this.lblDroppedItem.Text = "Item:";
            // 
            // lvwDroppedItems
            // 
            this.lvwDroppedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDroppedItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwDroppedItems_Item,
            this.lvwDroppedItems_Bp,
            this.lvwDroppedItems_DroppedBy,
            this.lvwDroppedItems_Lat,
            this.lvwDroppedItems_Lon,
            this.lvwDroppedItems_Tribe,
            this.lvwDroppedItems_Player});
            this.lvwDroppedItems.FullRowSelect = true;
            this.lvwDroppedItems.HideSelection = false;
            this.lvwDroppedItems.Location = new System.Drawing.Point(12, 52);
            this.lvwDroppedItems.MultiSelect = false;
            this.lvwDroppedItems.Name = "lvwDroppedItems";
            this.lvwDroppedItems.Size = new System.Drawing.Size(786, 353);
            this.lvwDroppedItems.TabIndex = 4;
            this.lvwDroppedItems.UseCompatibleStateImageBehavior = false;
            this.lvwDroppedItems.View = System.Windows.Forms.View.Details;
            this.lvwDroppedItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwDroppedItems_ColumnClick);
            this.lvwDroppedItems.SelectedIndexChanged += new System.EventHandler(this.lvwDroppedItems_SelectedIndexChanged);
            // 
            // lvwDroppedItems_Item
            // 
            this.lvwDroppedItems_Item.Text = "Item";
            this.lvwDroppedItems_Item.Width = 180;
            // 
            // lvwDroppedItems_Bp
            // 
            this.lvwDroppedItems_Bp.Text = "BP";
            // 
            // lvwDroppedItems_DroppedBy
            // 
            this.lvwDroppedItems_DroppedBy.Text = "Dropped By";
            this.lvwDroppedItems_DroppedBy.Width = 169;
            // 
            // lvwDroppedItems_Lat
            // 
            this.lvwDroppedItems_Lat.Text = "Lat";
            // 
            // lvwDroppedItems_Lon
            // 
            this.lvwDroppedItems_Lon.Text = "Lon";
            // 
            // lvwDroppedItems_Tribe
            // 
            this.lvwDroppedItems_Tribe.Text = "Tribe";
            this.lvwDroppedItems_Tribe.Width = 105;
            // 
            // lvwDroppedItems_Player
            // 
            this.lvwDroppedItems_Player.Text = "Player";
            this.lvwDroppedItems_Player.Width = 109;
            // 
            // tpgItemList
            // 
            this.tpgItemList.Controls.Add(this.chkItemSearchBlueprints);
            this.tpgItemList.Controls.Add(this.cboItemListItem);
            this.tpgItemList.Controls.Add(this.lblItemListTribe);
            this.tpgItemList.Controls.Add(this.cboItemListTribe);
            this.tpgItemList.Controls.Add(this.btnItemListCommand);
            this.tpgItemList.Controls.Add(this.lblItemListCommand);
            this.tpgItemList.Controls.Add(this.cboItemListCommand);
            this.tpgItemList.Controls.Add(this.lblItemListCount);
            this.tpgItemList.Controls.Add(this.lblItemListItem);
            this.tpgItemList.Controls.Add(this.lvwItemList);
            this.tpgItemList.Location = new System.Drawing.Point(4, 22);
            this.tpgItemList.Name = "tpgItemList";
            this.tpgItemList.Size = new System.Drawing.Size(811, 463);
            this.tpgItemList.TabIndex = 6;
            this.tpgItemList.Text = "Item Search";
            this.tpgItemList.UseVisualStyleBackColor = true;
            // 
            // chkItemSearchBlueprints
            // 
            this.chkItemSearchBlueprints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkItemSearchBlueprints.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkItemSearchBlueprints.BackgroundImage = global::ARKViewer.Properties.Resources.blueprints_unchecked;
            this.chkItemSearchBlueprints.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkItemSearchBlueprints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkItemSearchBlueprints.Location = new System.Drawing.Point(758, 6);
            this.chkItemSearchBlueprints.Name = "chkItemSearchBlueprints";
            this.chkItemSearchBlueprints.Size = new System.Drawing.Size(40, 40);
            this.chkItemSearchBlueprints.TabIndex = 11;
            this.toolTip1.SetToolTip(this.chkItemSearchBlueprints, "Show / hide blueprints.");
            this.chkItemSearchBlueprints.UseVisualStyleBackColor = true;
            this.chkItemSearchBlueprints.CheckedChanged += new System.EventHandler(this.chkItemSearchBlueprints_CheckedChanged);
            // 
            // cboItemListItem
            // 
            this.cboItemListItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemListItem.FormattingEnabled = true;
            this.cboItemListItem.Location = new System.Drawing.Point(334, 15);
            this.cboItemListItem.Name = "cboItemListItem";
            this.cboItemListItem.Size = new System.Drawing.Size(216, 21);
            this.cboItemListItem.TabIndex = 3;
            this.cboItemListItem.SelectedIndexChanged += new System.EventHandler(this.cboItemListItem_SelectedIndexChanged);
            // 
            // lblItemListTribe
            // 
            this.lblItemListTribe.AutoSize = true;
            this.lblItemListTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemListTribe.Location = new System.Drawing.Point(17, 18);
            this.lblItemListTribe.Name = "lblItemListTribe";
            this.lblItemListTribe.Size = new System.Drawing.Size(40, 13);
            this.lblItemListTribe.TabIndex = 0;
            this.lblItemListTribe.Text = "Tribe:";
            // 
            // cboItemListTribe
            // 
            this.cboItemListTribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemListTribe.FormattingEnabled = true;
            this.cboItemListTribe.Location = new System.Drawing.Point(69, 16);
            this.cboItemListTribe.Name = "cboItemListTribe";
            this.cboItemListTribe.Size = new System.Drawing.Size(218, 21);
            this.cboItemListTribe.TabIndex = 1;
            this.cboItemListTribe.SelectedIndexChanged += new System.EventHandler(this.cboItemListTribe_SelectedIndexChanged);
            // 
            // btnItemListCommand
            // 
            this.btnItemListCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemListCommand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnItemListCommand.Enabled = false;
            this.btnItemListCommand.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnItemListCommand.Location = new System.Drawing.Point(350, 412);
            this.btnItemListCommand.Name = "btnItemListCommand";
            this.btnItemListCommand.Size = new System.Drawing.Size(40, 40);
            this.btnItemListCommand.TabIndex = 7;
            this.btnItemListCommand.UseVisualStyleBackColor = true;
            this.btnItemListCommand.Click += new System.EventHandler(this.btnItemListCommand_Click);
            // 
            // lblItemListCommand
            // 
            this.lblItemListCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemListCommand.AutoSize = true;
            this.lblItemListCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemListCommand.Location = new System.Drawing.Point(14, 425);
            this.lblItemListCommand.Name = "lblItemListCommand";
            this.lblItemListCommand.Size = new System.Drawing.Size(65, 13);
            this.lblItemListCommand.TabIndex = 5;
            this.lblItemListCommand.Text = "Command:";
            // 
            // cboItemListCommand
            // 
            this.cboItemListCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboItemListCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemListCommand.FormattingEnabled = true;
            this.cboItemListCommand.Items.AddRange(new object[] {
            "SetPlayerPos  <x> <y> <z>"});
            this.cboItemListCommand.Location = new System.Drawing.Point(82, 422);
            this.cboItemListCommand.Name = "cboItemListCommand";
            this.cboItemListCommand.Size = new System.Drawing.Size(262, 21);
            this.cboItemListCommand.TabIndex = 6;
            // 
            // lblItemListCount
            // 
            this.lblItemListCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblItemListCount.BackColor = System.Drawing.Color.PowderBlue;
            this.lblItemListCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemListCount.Location = new System.Drawing.Point(675, 417);
            this.lblItemListCount.Name = "lblItemListCount";
            this.lblItemListCount.Size = new System.Drawing.Size(123, 30);
            this.lblItemListCount.TabIndex = 8;
            this.lblItemListCount.Text = "Count: 0";
            this.lblItemListCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItemListItem
            // 
            this.lblItemListItem.AutoSize = true;
            this.lblItemListItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemListItem.Location = new System.Drawing.Point(293, 19);
            this.lblItemListItem.Name = "lblItemListItem";
            this.lblItemListItem.Size = new System.Drawing.Size(35, 13);
            this.lblItemListItem.TabIndex = 2;
            this.lblItemListItem.Text = "Item:";
            // 
            // lvwItemList
            // 
            this.lvwItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwItemList_Tribe,
            this.lvwItemList_Container,
            this.lvwItemList_Item,
            this.lvwItemList_BP,
            this.lvwItemList_Quantity,
            this.lvwItemList_Lat,
            this.lvwItemList_Lon});
            this.lvwItemList.FullRowSelect = true;
            this.lvwItemList.HideSelection = false;
            this.lvwItemList.Location = new System.Drawing.Point(12, 52);
            this.lvwItemList.MultiSelect = false;
            this.lvwItemList.Name = "lvwItemList";
            this.lvwItemList.Size = new System.Drawing.Size(786, 353);
            this.lvwItemList.TabIndex = 4;
            this.lvwItemList.UseCompatibleStateImageBehavior = false;
            this.lvwItemList.View = System.Windows.Forms.View.Details;
            this.lvwItemList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwItemList_ColumnClick);
            this.lvwItemList.SelectedIndexChanged += new System.EventHandler(this.lvwItemList_SelectedIndexChanged);
            // 
            // lvwItemList_Tribe
            // 
            this.lvwItemList_Tribe.Text = "Tribe";
            this.lvwItemList_Tribe.Width = 175;
            // 
            // lvwItemList_Container
            // 
            this.lvwItemList_Container.Text = "Container";
            this.lvwItemList_Container.Width = 175;
            // 
            // lvwItemList_Item
            // 
            this.lvwItemList_Item.Text = "Item";
            this.lvwItemList_Item.Width = 175;
            // 
            // lvwItemList_BP
            // 
            this.lvwItemList_BP.Text = "BP";
            this.lvwItemList_BP.Width = 40;
            // 
            // lvwItemList_Quantity
            // 
            this.lvwItemList_Quantity.Text = "Qty";
            // 
            // lvwItemList_Lat
            // 
            this.lvwItemList_Lat.Text = "Lat";
            // 
            // lvwItemList_Lon
            // 
            this.lvwItemList_Lon.Text = "Lon";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.BackColor = System.Drawing.Color.PowderBlue;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblStatus.Location = new System.Drawing.Point(16, 564);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(644, 50);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Loading...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picIcon
            // 
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(15, 8);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(71, 55);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 24;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(89, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(71, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ASV";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Location = new System.Drawing.Point(92, 45);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(190, 16);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "ARK Savegame Visualiser";
            // 
            // lblMapTypeName
            // 
            this.lblMapTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapTypeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapTypeName.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblMapTypeName.Location = new System.Drawing.Point(322, 31);
            this.lblMapTypeName.Name = "lblMapTypeName";
            this.lblMapTypeName.Size = new System.Drawing.Size(504, 18);
            this.lblMapTypeName.TabIndex = 4;
            this.lblMapTypeName.Text = "Unknown Data";
            this.lblMapTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(155, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(32, 16);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "v0.0";
            // 
            // lblMap
            // 
            this.lblMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMap.AutoSize = true;
            this.lblMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMap.Location = new System.Drawing.Point(530, 68);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(89, 13);
            this.lblMap.TabIndex = 25;
            this.lblMap.Text = "Selected Map:";
            // 
            // cboSelectedMap
            // 
            this.cboSelectedMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSelectedMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectedMap.FormattingEnabled = true;
            this.cboSelectedMap.Location = new System.Drawing.Point(619, 64);
            this.cboSelectedMap.Name = "cboSelectedMap";
            this.cboSelectedMap.Size = new System.Drawing.Size(207, 21);
            this.cboSelectedMap.TabIndex = 26;
            this.cboSelectedMap.SelectedIndexChanged += new System.EventHandler(this.cboSelectedMap_SelectedIndexChanged);
            // 
            // cboTamedResource
            // 
            this.cboTamedResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTamedResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTamedResource.FormattingEnabled = true;
            this.cboTamedResource.Location = new System.Drawing.Point(520, 43);
            this.cboTamedResource.Name = "cboTamedResource";
            this.cboTamedResource.Size = new System.Drawing.Size(216, 21);
            this.cboTamedResource.TabIndex = 16;
            this.cboTamedResource.SelectedIndexChanged += new System.EventHandler(this.cboTamedResource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(442, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Resource:";
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(840, 625);
            this.Controls.Add(this.lblMap);
            this.Controls.Add(this.cboSelectedMap);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblMapTypeName);
            this.Controls.Add(this.lblMapDate);
            this.Controls.Add(this.btnViewMap);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tabFeatures);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 375);
            this.Name = "frmViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARK Savegame Visualiser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewer_FormClosed);
            this.LocationChanged += new System.EventHandler(this.frmViewer_LocationChanged);
            this.Enter += new System.EventHandler(this.frmViewer_Enter);
            this.mnuContext.ResumeLayout(false);
            this.tabFeatures.ResumeLayout(false);
            this.tpgWild.ResumeLayout(false);
            this.tpgWild.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWildRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMax)).EndInit();
            this.tpgTamed.ResumeLayout(false);
            this.tpgTamed.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tpgStructures.ResumeLayout(false);
            this.tpgStructures.PerformLayout();
            this.tpgTribes.ResumeLayout(false);
            this.tpgTribes.PerformLayout();
            this.tpgPlayers.ResumeLayout(false);
            this.tpgPlayers.PerformLayout();
            this.tpgDroppedItems.ResumeLayout(false);
            this.tpgDroppedItems.PerformLayout();
            this.tpgItemList.ResumeLayout(false);
            this.tpgItemList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvwWildDetail;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Name;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Sex;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Base;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_HP;
        private System.Windows.Forms.Label lblWildTotal;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Stam;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Melee;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Weight;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Speed;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Food;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Craft;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Lat;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Lon;
        private System.Windows.Forms.Label lblMapDate;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Level;
        private System.Windows.Forms.ComboBox cboWildClass;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl tabFeatures;
        private System.Windows.Forms.TabPage tpgWild;
        private System.Windows.Forms.TabPage tpgPlayers;
        private System.Windows.Forms.Label lblPlayersPlayer;
        private System.Windows.Forms.Label lblPlayersTribe;
        private System.Windows.Forms.ComboBox cboTribes;
        private System.Windows.Forms.ComboBox cboPlayers;
        private System.Windows.Forms.ListView lvwPlayers;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Name;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Sex;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Level;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Lat;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Lon;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Hp;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Stam;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Melee;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Weight;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Speed;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Food;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwPlayers_LastOnline;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Water;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Tribe;
        private System.Windows.Forms.Button btnPlayerInventory;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Crafting;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Fortitude;
        private System.Windows.Forms.Button btnPlayerTribeLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabPage tpgStructures;
        private System.Windows.Forms.Label lblStructurePlayer;
        private System.Windows.Forms.Label lblStructureTribe;
        private System.Windows.Forms.ComboBox cboStructureTribe;
        private System.Windows.Forms.ComboBox cboStructurePlayer;
        private System.Windows.Forms.ListView lvwStructureLocations;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Tribe;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Structure;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Lat;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Lon;
        private System.Windows.Forms.Label lblStructureStructure;
        private System.Windows.Forms.ComboBox cboStructureStructure;
        private System.Windows.Forms.Button btnStructureExclusionFilter;
        private System.Windows.Forms.Label lblCommandPlayer;
        private System.Windows.Forms.ComboBox cboConsoleCommandsPlayerTribe;
        private System.Windows.Forms.Button btnCopyCommandPlayer;
        private System.Windows.Forms.Button btnCopyCommandStructure;
        private System.Windows.Forms.Label lblCommandStructure;
        private System.Windows.Forms.ComboBox cboConsoleCommandsStructure;
        private System.Windows.Forms.TabPage tpgTamed;
        private System.Windows.Forms.Button btnDinoAncestors;
        private System.Windows.Forms.Button btnDinoInventory;
        private System.Windows.Forms.ListView lvwTameDetail;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Name;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Sex;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Base;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Level;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Lat;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Lon;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_HP;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Stam;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Melee;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Weight;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Speed;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Food;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Craft;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Tamer;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Imprinter;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Imprint;
        private System.Windows.Forms.Label lblTameTotal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.RadioButton optStatsTamed;
        private System.Windows.Forms.RadioButton optStatsBase;
        private System.Windows.Forms.ComboBox cboTameClass;
        private System.Windows.Forms.ComboBox cboTameTribes;
        private System.Windows.Forms.ComboBox cboTamePlayers;
        private System.Windows.Forms.Label lblTameCreature;
        private System.Windows.Forms.Label lblTamePlayer;
        private System.Windows.Forms.Label lblTameTribe;
        private System.Windows.Forms.Label lblStructureTotal;
        private System.Windows.Forms.Label lblPlayerTotal;
        private System.Windows.Forms.Label lblWildClass;
        private System.Windows.Forms.Label lblSelectedWildTotal;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Creature;
        private System.Windows.Forms.Button btnCopyCommandWild;
        private System.Windows.Forms.Label lblWildCommand;
        private System.Windows.Forms.ComboBox cboConsoleCommandsWild;
        private System.Windows.Forms.Button btnCopyCommandTamed;
        private System.Windows.Forms.Label lblTamedCommand;
        private System.Windows.Forms.ComboBox cboConsoleCommandsTamed;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_PlayerId;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_SteamId;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_TribeId;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Cryo;
        private System.Windows.Forms.CheckBox chkCryo;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour1;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour2;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour3;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour4;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour5;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour6;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour1;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour2;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour3;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour4;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour5;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour6;
        private System.Windows.Forms.TabPage tpgDroppedItems;
        private System.Windows.Forms.Button btnCopyCommandDropped;
        private System.Windows.Forms.Label lblCopyCommandDropped;
        private System.Windows.Forms.ComboBox cboCopyCommandDropped;
        private System.Windows.Forms.Label lblCountDropped;
        private System.Windows.Forms.Label lblDroppedItem;
        private System.Windows.Forms.ListView lvwDroppedItems;
        private System.Windows.Forms.ComboBox cboDroppedItem;
        private System.Windows.Forms.Label lblDroppedPlayer;
        private System.Windows.Forms.ComboBox cboDroppedPlayer;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Item;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_DroppedBy;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Lat;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Lon;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Tribe;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Player;
        private System.Windows.Forms.Label lblWildMin;
        private System.Windows.Forms.Label lblWildMax;
        private System.Windows.Forms.NumericUpDown udWildMin;
        private System.Windows.Forms.NumericUpDown udWildMax;
        private System.Windows.Forms.TabPage tpgTribes;
        private System.Windows.Forms.Button btnTribeCopyCommand;
        private System.Windows.Forms.Label lblTribeCopyCommand;
        private System.Windows.Forms.ComboBox cboTribeCopyCommand;
        private System.Windows.Forms.Button btnTribeLog;
        private System.Windows.Forms.ListView lvwTribes;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.CheckBox chkTribeStructures;
        private System.Windows.Forms.CheckBox chkTribeTames;
        private System.Windows.Forms.CheckBox chkTribePlayers;
        private System.Windows.Forms.Label lblWildRadius;
        private System.Windows.Forms.NumericUpDown udWildRadius;
        private System.Windows.Forms.Label lblWildLon;
        private System.Windows.Forms.NumericUpDown udWildLon;
        private System.Windows.Forms.Label lblWildLat;
        private System.Windows.Forms.NumericUpDown udWildLat;
        private System.Windows.Forms.Button btnStructureInventory;
        private System.Windows.Forms.Button btnDeletePlayer;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Export;
        private System.Windows.Forms.ColumnHeader lvwPlayers_SteamName;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_MutationsFemale;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_MutationsMale;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Server;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Id;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Id;
        private System.Windows.Forms.ColumnHeader lvwPlayers_SteamId;
        private System.Windows.Forms.Button btnDropInventory;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Button btnViewMap;
        private System.Windows.Forms.TabPage tpgItemList;
        private System.Windows.Forms.ComboBox cboItemListItem;
        private System.Windows.Forms.Label lblItemListTribe;
        private System.Windows.Forms.ComboBox cboItemListTribe;
        private System.Windows.Forms.Button btnItemListCommand;
        private System.Windows.Forms.Label lblItemListCommand;
        private System.Windows.Forms.ComboBox cboItemListCommand;
        private System.Windows.Forms.Label lblItemListCount;
        private System.Windows.Forms.Label lblItemListItem;
        private System.Windows.Forms.ListView lvwItemList;
        private System.Windows.Forms.ColumnHeader lvwItemList_Tribe;
        private System.Windows.Forms.ColumnHeader lvwItemList_Container;
        private System.Windows.Forms.ColumnHeader lvwItemList_Item;
        private System.Windows.Forms.ColumnHeader lvwItemList_Quantity;
        private System.Windows.Forms.ColumnHeader lvwItemList_Lat;
        private System.Windows.Forms.ColumnHeader lvwItemList_Lon;
        private System.Windows.Forms.Label lblMapTypeName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ComboBox cboWildResource;
        private System.Windows.Forms.Label lblResource;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.ComboBox cboSelectedMap;
        private System.Windows.Forms.Button btmMissionScoreboard;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Bp;
        private System.Windows.Forms.ColumnHeader lvwItemList_BP;
        private System.Windows.Forms.CheckBox chkDroppedBlueprints;
        private System.Windows.Forms.CheckBox chkItemSearchBlueprints;
        private System.Windows.Forms.ComboBox cboTamedResource;
        private System.Windows.Forms.Label label1;
    }
}

