using CS_Mgmt.Models;
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

namespace CS_Mgmt.Views.SuppliesViews
{
    /// <summary>
    /// Interaction logic for ViewEditItem.xaml
    /// </summary>
    public partial class ViewEditItem : Page
    {
        private int selectedSupplyID;
        public ViewEditItem(int supplyID)
        {
            InitializeComponent();
            selectedSupplyID = supplyID;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateFields(selectedSupplyID);
        }
        private void PopulateFields(int selectedSupplyId)
        {
            Supply supply = Supply.GetSelectedSupply(App.DatabasePath, selectedSupplyId);
            DescriptionTB.Text = supply.Description;
            QuantityTB.Text = supply.Quantity.ToString();
            StorageLocationTB.Text = supply.StorageLocation;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string newDesc = DescriptionTB.Text;
            int newQty = int.Parse(QuantityTB.Text);
            string newStorage = StorageLocationTB.Text;

            Supply supply = new Supply
            {
                SupplyId = selectedSupplyID,
                Description = newDesc,
                Quantity = newQty,
                StorageLocation = newStorage
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Update(supply);
            }

            MessageBox.Show("Updates saved!");

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
