
namespace ARKViewer
{
    partial class frmMapToolboxMarkers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapToolboxMarkers));
            this.lvwMapMarkers = new System.Windows.Forms.ListView();
            this.lvwMapMarkers_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwMapMarkers_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwMapMarkers_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemoveMarker = new System.Windows.Forms.Button();
            this.chkApplyFilterMarkers = new System.Windows.Forms.CheckBox();
            this.btnAddMarker = new System.Windows.Forms.Button();
            this.btnEditMarker = new System.Windows.Forms.Button();
            this.txtMarkerFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lvwMapMarkers
            // 
            this.lvwMapMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMapMarkers.CheckBoxes = true;
            this.lvwMapMarkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwMapMarkers_Name,
            this.lvwMapMarkers_Lat,
            this.lvwMapMarkers_Lon});
            this.lvwMapMarkers.FullRowSelect = true;
            this.lvwMapMarkers.HideSelection = false;
            this.lvwMapMarkers.Location = new System.Drawing.Point(6, 6);
            this.lvwMapMarkers.Name = "lvwMapMarkers";
            this.lvwMapMarkers.Size = new System.Drawing.Size(301, 294);
            this.lvwMapMarkers.TabIndex = 0;
            this.lvwMapMarkers.UseCompatibleStateImageBehavior = false;
            this.lvwMapMarkers.View = System.Windows.Forms.View.Details;
            this.lvwMapMarkers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMapMarkers_ColumnClick);
            this.lvwMapMarkers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwMapMarkers_ItemChecked);
            this.lvwMapMarkers.SelectedIndexChanged += new System.EventHandler(this.lvwMapMarkers_SelectedIndexChanged);
            // 
            // lvwMapMarkers_Name
            // 
            this.lvwMapMarkers_Name.Text = "Name";
            this.lvwMapMarkers_Name.Width = 167;
            // 
            // lvwMapMarkers_Lat
            // 
            this.lvwMapMarkers_Lat.Text = "Lat";
            // 
            // lvwMapMarkers_Lon
            // 
            this.lvwMapMarkers_Lon.Text = "Lon";
            // 
            // btnRemoveMarker
            // 
            this.btnRemoveMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveMarker.Enabled = false;
            this.btnRemoveMarker.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveMarker.Location = new System.Drawing.Point(42, 310);
            this.btnRemoveMarker.Name = "btnRemoveMarker";
            this.btnRemoveMarker.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveMarker.TabIndex = 2;
            this.btnRemoveMarker.UseVisualStyleBackColor = true;
            // 
            // chkApplyFilterMarkers
            // 
            this.chkApplyFilterMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilterMarkers.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilterMarkers.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilterMarkers.Location = new System.Drawing.Point(239, 310);
            this.chkApplyFilterMarkers.Name = "chkApplyFilterMarkers";
            this.chkApplyFilterMarkers.Size = new System.Drawing.Size(30, 30);
            this.chkApplyFilterMarkers.TabIndex = 4;
            this.chkApplyFilterMarkers.UseVisualStyleBackColor = true;
            // 
            // btnAddMarker
            // 
            this.btnAddMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMarker.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddMarker.Location = new System.Drawing.Point(6, 310);
            this.btnAddMarker.Name = "btnAddMarker";
            this.btnAddMarker.Size = new System.Drawing.Size(30, 30);
            this.btnAddMarker.TabIndex = 1;
            this.btnAddMarker.UseVisualStyleBackColor = true;
            this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // btnEditMarker
            // 
            this.btnEditMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditMarker.Enabled = false;
            this.btnEditMarker.Image = ((System.Drawing.Image)(resources.GetObject("btnEditMarker.Image")));
            this.btnEditMarker.Location = new System.Drawing.Point(277, 310);
            this.btnEditMarker.Name = "btnEditMarker";
            this.btnEditMarker.Size = new System.Drawing.Size(30, 30);
            this.btnEditMarker.TabIndex = 5;
            this.btnEditMarker.UseVisualStyleBackColor = true;
            // 
            // txtMarkerFilter
            // 
            this.txtMarkerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarkerFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarkerFilter.Location = new System.Drawing.Point(81, 312);
            this.txtMarkerFilter.Name = "txtMarkerFilter";
            this.txtMarkerFilter.Size = new System.Drawing.Size(152, 22);
            this.txtMarkerFilter.TabIndex = 3;
            // 
            // frmMapToolboxMarkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(314, 346);
            this.Controls.Add(this.lvwMapMarkers);
            this.Controls.Add(this.btnRemoveMarker);
            this.Controls.Add(this.chkApplyFilterMarkers);
            this.Controls.Add(this.btnAddMarker);
            this.Controls.Add(this.btnEditMarker);
            this.Controls.Add(this.txtMarkerFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(330, 200);
            this.Name = "frmMapToolboxMarkers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Map Markers";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMapToolboxMarkers_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwMapMarkers;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Name;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Lat;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Lon;
        private System.Windows.Forms.Button btnRemoveMarker;
        private System.Windows.Forms.CheckBox chkApplyFilterMarkers;
        private System.Windows.Forms.Button btnAddMarker;
        private System.Windows.Forms.Button btnEditMarker;
        private System.Windows.Forms.TextBox txtMarkerFilter;
    }
}