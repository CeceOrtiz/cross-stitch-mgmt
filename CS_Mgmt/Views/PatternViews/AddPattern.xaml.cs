using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SQLite;
using CS_Mgmt.Validations;

namespace CS_Mgmt.Views.PatternViews
{
    /// <summary>
    /// Interaction logic for AddPattern.xaml
    /// </summary>
    public partial class AddPattern : Page
    {
        #region Initialization
        public AddPattern()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> statuses = new List<string> {"Not purchased", "Not started", "In-progress", "Incomplete", "Completed"};
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
        }
        #endregion

        #region Buttons
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
                    connection.Insert(newPattern);

                    int lastInsertedId = connection.ExecuteScalar<int>("SELECT last_insert_rowid()");

                    foreach (PatternColorItem item in PatternColorsDG.Items)
                    {
                        int flossId = int.Parse(item.FlossID);
                        int skeinsNeeded = int.Parse(item.SkeinsNeeded);

                        string compositeKey = $"{lastInsertedId} + {flossId}";

                        PatternFloss newPFloss = new PatternFloss
                        {
                            CompositeKey = compositeKey,
                            PatternId = lastInsertedId,
                            FlossId = flossId,
                            SkeinsNeeded = skeinsNeeded
                        };

                        connection.Insert(newPFloss);
                    }
                }

                MessageBox.Show("Pattern saved!");

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new Dash());
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
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
        #endregion

        #region Classes
        private class PatternColorItem
        {
            public string FlossID { get; set; }
            public string Color { get; set; }
            public string SkeinsNeeded { get; set; }
        }
        #endregion

    }
}
