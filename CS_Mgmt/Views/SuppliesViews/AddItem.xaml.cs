using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Validations;
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

namespace CS_Mgmt.Views.SuppliesViews
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Page
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save the item
            string desc = DescriptionTB.Text;
            ComboBoxItem selectedQty = QuantityCB.SelectedItem as ComboBoxItem;

            bool continueSave = ItemValidation.ValidItem(desc, selectedQty);

            if (continueSave == true)
            {
                int quantity = (int)selectedQty.Tag;
                string storageLocation = StorageLocationTB.Text;

                Supply newItem = new Supply
                {
                    Description = desc,
                    Quantity = quantity,
                    StorageLocation = storageLocation
                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Insert(newItem);
                }

                MessageBox.Show("Item saved!");

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new Dash());
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
