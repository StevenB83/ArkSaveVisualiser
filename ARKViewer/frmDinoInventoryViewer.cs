﻿using ARKViewer.Configuration;
using ARKViewer.Models;
using ARKViewer.Models.NameMap;
using ASVPack.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmDinoInventoryViewer : Form
    {
        List<ContentItem> loadedItems = new List<ContentItem>();

        ColumnHeader SortingColumn_Inventory = null;


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


        public frmDinoInventoryViewer(ContentTamedCreature tame, List<ContentItem> items)
        {
            InitializeComponent();
            LoadWindowSettings();

            lvwCreatureInventory.LargeImageList = Program.ItemImageList;
            lvwCreatureInventory.SmallImageList = Program.ItemImageList;

            loadedItems = items;

            var dinoMap = Program.ProgramConfig.DinoMap.FirstOrDefault(m => m.ClassName == tame.ClassName);
            string dinoName = dinoMap == null ? tame.ClassName : dinoMap.FriendlyName;

            if (tame.Name != null)
            {
                dinoName = tame.Name;
            }

            if (dinoName.Length == 0)
            {
                dinoName = tame.ClassName;
                DinoClassMap classMap = Program.ProgramConfig.DinoMap.Where(d => d.ClassName == tame.ClassName).FirstOrDefault();
                if (classMap != null && classMap.FriendlyName.Length > 0)
                {
                    dinoName = classMap.FriendlyName;
                }
            }

            lblName.Text = dinoName;
            lblLevel.Text = tame.Level.ToString();
            lblTribeName.Text = tame.TribeName;


            PopulateCreatureInventory();

        }

        private void PopulateCreatureInventory()
        {
            lvwCreatureInventory.Items.Clear();
            if (loadedItems != null && loadedItems.Count > 0)
            {
                //var playerItems = selectedPlayer.Creatures;

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(loadedItems, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.DisplayName;
                        categoryName = itemMap.Category;
                        itemIcon = Program.GetItemImageIndex(itemMap.Image);

                    }


                    if (itemName.ToLower().Contains(txtCreatureFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtCreatureFilter.Text.ToLower()))
                    {
                        if (!invItem.IsEngram)
                        {
                            ListViewItem newItem = new ListViewItem(itemName);
                            newItem.SubItems.Add(invItem.IsBlueprint ? "Yes" : "No");
                            newItem.SubItems.Add(categoryName);
                            newItem.SubItems.Add(invItem.Quality);
                            newItem.SubItems.Add(invItem.Rating.HasValue ? invItem.Rating.ToString() : "");
                            newItem.SubItems.Add(invItem.Quantity.ToString());
                            newItem.ImageIndex = itemIcon - 1;

                            listItems.Add(newItem);
                        }
                    }

                });

                lvwCreatureInventory.Items.AddRange(listItems.ToArray());

            }
        }

        private void chkApplyFilterDinos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmDinoInventoryViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void lvwCreatureInventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwCreatureInventory.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Inventory == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Inventory)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Inventory.Text.StartsWith("> "))
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
                SortingColumn_Inventory.Text = SortingColumn_Inventory.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Inventory = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Inventory.Text = "> " + SortingColumn_Inventory.Text;
            }
            else
            {
                SortingColumn_Inventory.Text = "< " + SortingColumn_Inventory.Text;
            }

            // Create a comparer.
            lvwCreatureInventory.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwCreatureInventory.Sort();
        }
    }
}
