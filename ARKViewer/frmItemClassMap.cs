using ARKViewer.Configuration;
using ARKViewer.Models;
using ARKViewer.Models.NameMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmItemClassMap : Form
    {
        string imageFolder = "";
        string loadedClassName = "";

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

        public ItemClassMap ClassMap { get; set; } = new ItemClassMap();

        public frmItemClassMap()
        {
            InitializeComponent();
            LoadWindowSettings();

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            txtClassName.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            //picIcon.Image = ASV.Properties.Resources.marker_0;

            txtClassName.ReadOnly = false;
        }

        public frmItemClassMap(ItemClassMap selectedMap)
        {
            InitializeComponent();

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            txtClassName.ReadOnly = true;

            loadedClassName = selectedMap.ClassName;

            ClassMap = selectedMap;
            txtClassName.Text = selectedMap.ClassName;
            txtDisplayName.Text = selectedMap.DisplayName;
            txtCategory.Text = selectedMap.Category;

            picIcon.Image = ARKViewer.Properties.Resources.marker_0;
            if (selectedMap.Image.Length > 0)
            {
                string imageFilename = Path.Combine(imageFolder, selectedMap.Image);
                if (File.Exists(imageFilename))
                {
                    picIcon.Image = Image.FromFile(imageFilename);
                }
            }
        }

        private void picIcon_Click(object sender, EventArgs e)
        {

        }

        private void txtDisplayName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCategory_Validating(object sender, CancelEventArgs e)
        {

        }

        private void frmItemClassMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void frmItemClassMap_Shown(object sender, EventArgs e)
        {
            if (ClassMap.ClassName != null && ClassMap.ClassName.Length > 0)
            {
                txtDisplayName.Focus();
                txtDisplayName.SelectAll();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
