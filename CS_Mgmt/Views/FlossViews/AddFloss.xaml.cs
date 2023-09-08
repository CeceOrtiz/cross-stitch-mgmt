using CS_Mgmt.Views.Dashboard;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CS_Mgmt.Models;
using CS_Mgmt.Validations;
using SQLite;

namespace CS_Mgmt.Views.FlossViews
{
    /// <summary>
    /// Interaction logic for AddFloss.xaml
    /// </summary>
    public partial class AddFloss : Page
    {
        #region Initialization
        public AddFloss()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Floss> nonUserFlossItems = Floss.GetNonUserFloss(App.DatabasePath);

            foreach (var floss in nonUserFlossItems)
            {
                ComboBoxItem flossItem = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                ColorCB.Items.Add(flossItem);
            }

            // Populate the quantity box
            for (int i = 1; i < 16; i++)
            {
                ComboBoxItem qtyItem = new ComboBoxItem
                {
                    Content = i,
                    Tag = i
                };

                QuantityCB.Items.Add(qtyItem);
            }
        }
        #endregion

        #region Buttons
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedColor = ColorCB.SelectedItem as ComboBoxItem;
            ComboBoxItem selectedQty = QuantityCB.SelectedItem as ComboBoxItem;

            bool continueSave = FlossValidation.ValidNewFloss(selectedColor, selectedQty);

            if (continueSave == true)
            {
                int flossId = (int)selectedColor.Tag;
                int quantity = (int)selectedQty.Tag;
                string storageLocation = StorageLocationTB.Text;

                UserFloss newFloss = new UserFloss
                {
                    FlossId = flossId,
                    Quantity = quantity,
                    StorageLocation = storageLocation
                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Insert(newFloss);
                }

                MessageBox.Show("Floss saved!");

                // Navigate to dashboard
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new Dash());
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
        #endregion
    }
}
