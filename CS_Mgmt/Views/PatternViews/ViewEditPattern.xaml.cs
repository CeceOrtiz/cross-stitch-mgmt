using CS_Mgmt.Models;
using CS_Mgmt.Validations;
using CS_Mgmt.Views.Dashboard;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS_Mgmt.Views.PatternViews
{
    /// <summary>
    /// Interaction logic for ViewEditPattern.xaml
    /// </summary>
    public partial class ViewEditPattern : Page
    {
        private int selectedPatternID;
        public ViewEditPattern(int patternID)
        {
            InitializeComponent();
            selectedPatternID = patternID;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> statuses = new List<string> { "Not purchased", "Not started", "In-progress", "Incomplete", "Completed" };
            foreach (var status in statuses)
            {
                ComboBoxItem statusItem = new ComboBoxItem
                {
                    Content = status,
                    Tag = status
                };

                StatusCB.Items.Add(statusItem);
            }

            List<Floss> allFloss = Floss.GetAllFloss(App.DatabasePath);
            foreach (var floss in allFloss)
            {
                ComboBoxItem flossItem = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                FlossColorCB.Items.Add(flossItem);
            }

            for (int i = 1; i < 11; i++)
            {
                ComboBoxItem skeinsQty = new ComboBoxItem
                {
                    Content = i,
                    Tag = i
                };

                SkeinsCB.Items.Add(skeinsQty);
            }

            PopulateFields(selectedPatternID);
        }
        private void PopulateFields(int selectedPatternID)
        {
            Pattern pattern = Pattern.GetSelectedPattern(App.DatabasePath, selectedPatternID);

            NameTB.Text = pattern.Name;
            DimensionsTB.Text = pattern.Dimensions;
            FabricColorTB.Text = pattern.FabricColor;
            CreatorTB.Text = pattern.Creator;
            SourceTB.Text = pattern.Source;
            StorageLocationTB.Text = pattern.StorageLocation;

            foreach (ComboBoxItem item in StatusCB.Items)
            {
                if (item.Content.ToString() == pattern.Status.ToString())
                {
                    StatusCB.SelectedItem = item;
                    break;
                }
            }

            // Populate the datagrid
            List<PatternFloss> pFlosses = PatternFloss.GetPatternFlosses(App.DatabasePath, selectedPatternID);
            List<Floss> assocFlosses = Floss.GetPatternFlosses(App.DatabasePath, pFlosses);

            foreach (PatternFloss p in pFlosses)
            {
                Floss thisFloss = assocFlosses.FirstOrDefault(f => f.FlossId == p.FlossId);

                PatternColorsDG.Items.Add(new PatternColorItem
                {
                    FlossID = p.FlossId.ToString(),
                    Color = $"{thisFloss.StandardName} - {thisFloss.Color}",
                    SkeinsNeeded = p.SkeinsNeeded.ToString()
                });

                ComboBoxItem currentCBItem = FlossColorCB.Items.OfType<ComboBoxItem>()
                    .FirstOrDefault(i => (int)i.Tag == p.FlossId);

                if (currentCBItem != null)
                {
                    FlossColorCB.Items.Remove(currentCBItem);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTB.Text;
            ComboBoxItem selectedStatus = StatusCB.SelectedItem as ComboBoxItem;

            bool continueSave = PatternValidation.ValidPattern(name, selectedStatus);

            if (continueSave == true)
            {
                string status = selectedStatus.Tag as string;
                string dimensions = DimensionsTB.Text;
                string fabricColor = FabricColorTB.Text;
                string creator = CreatorTB.Text;
                string source = SourceTB.Text;
                string storageLocation = StorageLocationTB.Text;

                Pattern newPattern = new Pattern
                {
                    PatternId = selectedPatternID,
                    Name = name,
                    Status = status,
                    Dimensions = dimensions,
                    FabricColor = fabricColor,
                    Creator = creator,
                    Source = source,
                    StorageLocation = storageLocation
                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.BeginTransaction();

                    connection.Update(newPattern);

                    // Remove all existing pattern flosses associated with this pattern
                    connection.Execute("DELETE FROM PatternFloss WHERE PatternId = ?", selectedPatternID);

                    // Add back in all the pattern flosses currently in the dg
                    foreach (PatternColorItem item in PatternColorsDG.Items)
                    {
                        int flossId = int.Parse(item.FlossID);
                        int skeinsNeeded = int.Parse(item.SkeinsNeeded);

                        string compositeKey = $"{selectedPatternID} + {flossId}";

                        PatternFloss newPFloss = new PatternFloss
                        {
                            CompositeKey = compositeKey,
                            PatternId = selectedPatternID,
                            FlossId = flossId,
                            SkeinsNeeded = skeinsNeeded
                        };

                        connection.Insert(newPFloss);
                    }

                    connection.Commit();

                    MessageBox.Show("Pattern saved!");

                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.MainFrame.NavigationService.Navigate(new Dash());
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private class PatternColorItem
        {
            public string FlossID { get; set; }
            public string Color { get; set; }
            public string SkeinsNeeded { get; set; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedColor = FlossColorCB.SelectedItem as ComboBoxItem;
            ComboBoxItem selectedQty = SkeinsCB.SelectedItem as ComboBoxItem;

            if (selectedColor == null)
            {
                MessageBox.Show("Please select a color.");
            }

            else if (selectedQty == null)
            {
                MessageBox.Show("Please select the number of skeins needed.");
            }

            else
            {
                PatternColorsDG.Items.Add(new PatternColorItem
                {
                    FlossID = selectedColor.Tag.ToString(),
                    Color = selectedColor.Content.ToString(),
                    SkeinsNeeded = selectedQty.Tag.ToString()
                });

                FlossColorCB.Items.Remove(selectedColor);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PatternColorsDG.SelectedItem != null)
            {
                PatternColorItem selectedItem = (PatternColorItem)PatternColorsDG.SelectedItem;
                PatternColorsDG.Items.Remove(selectedItem);

                ComboBoxItem removedFloss = new ComboBoxItem
                {
                    Content = $"{selectedItem.Color}",
                    Tag = selectedItem.FlossID
                };

                FlossColorCB.Items.Add(removedFloss);
            }

            else
            {
                MessageBox.Show("Please select the floss you want to remove.");
            }
        }
    }
}
