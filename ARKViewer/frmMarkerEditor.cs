using ARKViewer.Configuration;
using ARKViewer.Models;
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
    public partial class frmMarkerEditor : Form
    {
        private string selectedMap = "TheIsland.ark";
        public ASVMapMarker EditingMarker { get; set; } = new ASVMapMarker();
        private List<ASVMapMarker> markerList = new List<ASVMapMarker>();
        string imageFolder = "";

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


        public frmMarkerEditor(string currentMapFile, List<ASVMapMarker> currentMarkers, string selectedMarkerName)
        {
            InitializeComponent();
            LoadWindowSettings();

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\");
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            markerList = currentMarkers;
            selectedMap = currentMapFile;

            if (selectedMarkerName.Length > 0)
            {
                //attempt to find and load it
                ASVMapMarker selectedMarker = currentMarkers.Where(m => m.Map.ToLower() == currentMapFile.ToLower() && m.Name == selectedMarkerName).FirstOrDefault();
                EditingMarker = selectedMarker;
            }

            txtName.Enabled = selectedMarkerName.Length == 0;

            UpdateDisplay();

        }

        private void UpdateDisplay()
        {
            txtName.Text = "";
            pnlBackgroundColour.BackColor = Color.White;
            pnlBorderColour.BackColor = Color.Black;
            udBorderSize.Value = 0;
            udLat.Value = 0;
            udLon.Value = 0;
            picIcon.Image = new Bitmap(100,100);

            if (EditingMarker != null)
            {
                txtName.Text = EditingMarker.Name;
                pnlBackgroundColour.BackColor = Color.FromArgb(EditingMarker.Colour);
                pnlBorderColour.BackColor = Color.FromArgb(EditingMarker.BorderColour);
                udBorderSize.Value = EditingMarker.BorderWidth;
                udLat.Value = (decimal)EditingMarker.Lat;
                udLon.Value = (decimal)EditingMarker.Lon;
                UpdateImage();
            }

        }

        private void UpdateImage()
        {
            picIcon.Tag = string.Empty;

            if (EditingMarker.Image.Length > 0)
            {
                string imageFilename = Path.Combine(imageFolder, EditingMarker.Image);
                if (File.Exists(imageFilename))
                {
                    Image markerImage = Image.FromFile(imageFilename);
                    picIcon.Image = markerImage;
                    picIcon.Tag = Path.GetFileName(imageFilename);
                }
            }
        }



        private void txtName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void pnlBorderColour_Click(object sender, EventArgs e)
        {

        }

        private void pnlBackgroundColour_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void picIcon_Click(object sender, EventArgs e)
        {

        }

        private void frmMarkerEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
