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
    public partial class frmMapToolboxMarkers : Form
    {
        private frmMapView MapViewer = null;
        private ColumnHeader SortingColumn_Markers = null;

        private static frmMapToolboxMarkers inst;
        public static frmMapToolboxMarkers GetForm(frmMapView viewer)
        {
            if (inst == null || inst.IsDisposed)
            {
                inst = new frmMapToolboxMarkers(viewer);
                inst.Owner = viewer;
            }

            return inst;
        }


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



        private frmMapToolboxMarkers(frmMapView viewer)
        {
            InitializeComponent();
            LoadWindowSettings();
            MapViewer = viewer;
            PopulateCustomMarkers();
        }

        private void frmMapToolboxMarkers_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void PopulateCustomMarkers()
        {
            lvwMapMarkers.SmallImageList = Program.MarkerImageList;
            lvwMapMarkers.LargeImageList = Program.MarkerImageList;

            lvwMapMarkers.Items.Clear();
            lvwMapMarkers.Refresh();
            lvwMapMarkers.BeginUpdate();
            foreach (var marker in MapViewer.CustomMarkers)
            {
                if (marker.Name.ToLower().Contains(txtMarkerFilter.Text.ToLower()))
                {
                    ListViewItem newItem = lvwMapMarkers.Items.Add(marker.Name);
                    newItem.ImageIndex = Program.GetMarkerImageIndex(marker.Image) - 1;
                    newItem.SubItems.Add(marker.Lat.ToString("0.00"));
                    newItem.SubItems.Add(marker.Lon.ToString("0.00"));
                    newItem.Tag = marker;

                    if (marker.Displayed)
                    {
                        newItem.Checked = true;
                    }
                }
            }

            lvwMapMarkers.EndUpdate();
        }

        private void lvwMapMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvwMapMarkers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void lvwMapMarkers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwMapMarkers.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Markers == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Markers)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Markers.Text.StartsWith("> "))
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
                SortingColumn_Markers.Text = SortingColumn_Markers.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Markers = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Markers.Text = "> " + SortingColumn_Markers.Text;
            }
            else
            {
                SortingColumn_Markers.Text = "< " + SortingColumn_Markers.Text;
            }

            // Create a comparer.
            lvwMapMarkers.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwMapMarkers.Sort();
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveMarker_Click(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];
            ASVMapMarker selectedMarker = (ASVMapMarker)selectedItem.Tag;
            if (MessageBox.Show($"Are you sure you want to remove your marker for '{selectedMarker.Name}'?", "Remove Marker?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lvwMapMarkers.Items.Remove(selectedItem);
                if (ARKViewer.Program.ProgramConfig.MapMarkerList.Contains(selectedMarker))
                {
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Remove(selectedMarker);
                }
            }
        }

        private void chkApplyFilterMarkers_CheckedChanged(object sender, EventArgs e)
        {
            txtMarkerFilter.Enabled = !chkApplyFilterMarkers.Checked;
            if (!chkApplyFilterMarkers.Checked)
            {
                txtMarkerFilter.Text = string.Empty;
                txtMarkerFilter.Focus();
            }

            PopulateCustomMarkers();
        }

        private void btnEditMarker_Click(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];
            ASVMapMarker selectedMarker = (ASVMapMarker)selectedItem.Tag;

            frmMarkerEditor markerEditor = new frmMarkerEditor(Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile), Program.ProgramConfig.MapMarkerList, selectedMarker.Name);
            markerEditor.Owner = this;
            if (markerEditor.ShowDialog() == DialogResult.OK)
            {
                selectedItem.Text = markerEditor.EditingMarker.Name;
                selectedItem.ImageKey = $"marker_{markerEditor.EditingMarker.Image}";
                selectedItem.SubItems[1].Text = markerEditor.EditingMarker.Lat.ToString("0.00");
                selectedItem.SubItems[2].Text = markerEditor.EditingMarker.Lon.ToString("0.00");
                selectedItem.Tag = markerEditor.EditingMarker;

                if (ARKViewer.Program.ProgramConfig.MapMarkerList.Contains(selectedMarker))
                {
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Remove(selectedMarker);
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Add(markerEditor.EditingMarker);
                }

            }
        }

    }
}
