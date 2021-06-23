using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmColourPicker : Form
    {
        public int ColourId { get; set; } = 0;

        public frmColourPicker()
        {
            InitializeComponent();
            PopulateColours();
        }

        private void chkApplyFilterColours_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PopulateColours()
        {
            this.Cursor = Cursors.WaitCursor;

            //populate class map
            lvwColours.Items.Clear();
            lvwColours.Refresh();
            lvwColours.BeginUpdate();
            if (Program.ProgramConfig.ColourMap.Count > 0)
            {
                foreach (var colourMap in Program.ProgramConfig.ColourMap.OrderBy(d => d.Id))
                {
                    ListViewItem newItem = lvwColours.Items.Add(colourMap.Id.ToString());
                    newItem.UseItemStyleForSubItems = false;
                    newItem.SubItems.Add(colourMap.Hex);
                    newItem.SubItems.Add("");
                    newItem.SubItems[2].BackColor = colourMap.Color;
                    newItem.Tag = colourMap;
                }

            }

            lvwColours.EndUpdate();

            btnSave.Enabled = lvwColours.SelectedItems.Count == 1;

            this.Cursor = Cursors.Default;
        }

        private void lvwColours_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
