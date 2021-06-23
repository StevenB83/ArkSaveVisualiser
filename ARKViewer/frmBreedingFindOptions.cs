using ARKViewer.Configuration;
using ARKViewer.Models;
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
    public partial class frmBreedingFindOptions : Form
    {
        public ASVBreedingSearch SearchOptions { get; internal set; } = new ASVBreedingSearch();

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

        private void LoadOptions(string className)
        {
            SearchOptions = Program.ProgramConfig.BreedingSearchOptions.FirstOrDefault(o => o.ClassName == className);
            if (SearchOptions == null) SearchOptions = new ASVBreedingSearch() { ClassName = className };

            udMinHp.Value = SearchOptions.RangeHp.Item1;
            udMaxHp.Value = SearchOptions.RangeHp.Item2;

            udMinStamina.Value = SearchOptions.RangeStamina.Item1;
            udMaxStamina.Value = SearchOptions.RangeStamina.Item2;

            udMinMelee.Value = SearchOptions.RangeMelee.Item1;
            udMaxMelee.Value = SearchOptions.RangeMelee.Item2;

            udMinWeight.Value = SearchOptions.RangeWeight.Item1;
            udMaxWeight.Value = SearchOptions.RangeWeight.Item2;

            udMinSpeed.Value = SearchOptions.RangeSpeed.Item1;
            udMaxSpeed.Value = SearchOptions.RangeSpeed.Item2;

            udMinFood.Value = SearchOptions.RangeFood.Item1;
            udMaxFood.Value = SearchOptions.RangeFood.Item2;

            udMinOxygen.Value = SearchOptions.RangeOxygen.Item1;
            udMaxOxygen.Value = SearchOptions.RangeOxygen.Item2;

            udMinCrafting.Value = SearchOptions.RangeCrafting.Item1;
            udMaxCrafting.Value = SearchOptions.RangeCrafting.Item2;

            cboColours0.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion0)
            {
                cboColours0.Items.Add(colourId.ToString());
            }

            cboColours1.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion1)
            {
                cboColours1.Items.Add(colourId.ToString());
            }

            cboColours2.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion2)
            {
                cboColours2.Items.Add(colourId.ToString());
            }

            cboColours3.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion3)
            {
                cboColours3.Items.Add(colourId.ToString());
            }

            cboColours4.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion4)
            {
                cboColours4.Items.Add(colourId.ToString());
            }

            cboColours5.Items.Clear();
            foreach (var colourId in SearchOptions.ColoursRegion5)
            {
                cboColours5.Items.Add(colourId.ToString());
            }

        }

        private void SaveOptions()
        {
            SearchOptions.RangeHp = new Tuple<int, int>((int)udMinHp.Value, (int)udMaxHp.Value);
            SearchOptions.RangeCrafting = new Tuple<int, int>((int)udMinCrafting.Value, (int)udMaxCrafting.Value);
            SearchOptions.RangeFood = new Tuple<int, int>((int)udMinFood.Value, (int)udMaxFood.Value);
            SearchOptions.RangeMelee = new Tuple<int, int>((int)udMinMelee.Value, (int)udMaxMelee.Value);
            SearchOptions.RangeOxygen = new Tuple<int, int>((int)udMinOxygen.Value, (int)udMaxOxygen.Value);
            SearchOptions.RangeSpeed = new Tuple<int, int>((int)udMinSpeed.Value, (int)udMaxSpeed.Value);
            SearchOptions.RangeStamina = new Tuple<int, int>((int)udMinStamina.Value, (int)udMaxStamina.Value);
            SearchOptions.RangeWeight = new Tuple<int, int>((int)udMinWeight.Value, (int)udMaxWeight.Value);

            SearchOptions.ColoursRegion0 = new List<int>();
            SearchOptions.ColoursRegion0.AddRange(cboColours0.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            SearchOptions.ColoursRegion1 = new List<int>();
            SearchOptions.ColoursRegion1.AddRange(cboColours1.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            SearchOptions.ColoursRegion2 = new List<int>();
            SearchOptions.ColoursRegion2.AddRange(cboColours2.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            SearchOptions.ColoursRegion3 = new List<int>();
            SearchOptions.ColoursRegion3.AddRange(cboColours3.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            SearchOptions.ColoursRegion4 = new List<int>();
            SearchOptions.ColoursRegion4.AddRange(cboColours4.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            SearchOptions.ColoursRegion5 = new List<int>();
            SearchOptions.ColoursRegion5.AddRange(cboColours5.Items.Cast<Object>().Select(s => int.Parse(s.ToString())).ToList());

            var foundOption = Program.ProgramConfig.BreedingSearchOptions.FirstOrDefault(o => o.ClassName == SearchOptions.ClassName);
            if (foundOption != null)
            {
                foundOption = SearchOptions;
            }
            else
            {
                Program.ProgramConfig.BreedingSearchOptions.Add(SearchOptions);
            }
        }

        public frmBreedingFindOptions(string className)
        {
            InitializeComponent();
            LoadWindowSettings();
            LoadOptions(className);
        }

        private void frmBreedingFindOptions_Load(object sender, EventArgs e)
        {

        }


        private void frmBreedingFindOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveOptions();
        }

        private void btnAddColour0_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours0;
            AddColour(selectedCombo);
        }

        private void AddColour(ComboBox selectedCombo)
        {
            using (frmColourPicker picker = new frmColourPicker())
            {
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    if (!selectedCombo.Items.Contains(picker.ColourId.ToString()))
                    {
                        int newIndex = selectedCombo.Items.Add(picker.ColourId.ToString());
                        selectedCombo.SelectedIndex = newIndex;
                    }
                }
            }
        }

        private void DrawComboColourItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox selectedCombo = (ComboBox)sender;
            var item = selectedCombo.Items[e.Index];
            int.TryParse(item.ToString(), out int colourId);

            var map = Program.ProgramConfig.ColourMap.FirstOrDefault(c => c.Id == colourId);
            Brush backBrush = new SolidBrush(map.Color);
            e.Graphics.FillRectangle(backBrush, e.Bounds);

        }

        private void btnAddColour1_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours1;
            AddColour(selectedCombo);
        }

        private void btnAddColour2_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours2;
            AddColour(selectedCombo);
        }

        private void btnAddColour3_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours3;
            AddColour(selectedCombo);
        }

        private void btnAddColour4_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours4;
            AddColour(selectedCombo);
        }

        private void btnAddColour5_Click(object sender, EventArgs e)
        {
            ComboBox selectedCombo = cboColours5;
            AddColour(selectedCombo);
        }

        private void cboColours0_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour0.Enabled = cboColours0.SelectedIndex >= 0;
        }

        private void cboColours1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour1.Enabled = cboColours1.SelectedIndex >= 0;
        }

        private void cboColours2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour2.Enabled = cboColours2.SelectedIndex >= 0;
        }

        private void cboColours3_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour3.Enabled = cboColours3.SelectedIndex >= 0;
        }

        private void cboColours4_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour4.Enabled = cboColours4.SelectedIndex >= 0;
        }

        private void cboColours5_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour5.Enabled = cboColours5.SelectedIndex >= 0;
        }

        private void btnRemoveColour0_Click(object sender, EventArgs e)
        {
            cboColours0.Items.RemoveAt(cboColours0.SelectedIndex);
        }

        private void btnRemoveColour1_Click(object sender, EventArgs e)
        {
            cboColours1.Items.RemoveAt(cboColours1.SelectedIndex);
        }

        private void btnRemoveColour2_Click(object sender, EventArgs e)
        {
            cboColours2.Items.RemoveAt(cboColours2.SelectedIndex);
        }

        private void btnRemoveColour3_Click(object sender, EventArgs e)
        {
            cboColours3.Items.RemoveAt(cboColours3.SelectedIndex);
        }

        private void btnRemoveColour4_Click(object sender, EventArgs e)
        {
            cboColours4.Items.RemoveAt(cboColours4.SelectedIndex);
        }

        private void btnRemoveColour5_Click(object sender, EventArgs e)
        {
            cboColours5.Items.RemoveAt(cboColours5.SelectedIndex);
        }
    }
}
