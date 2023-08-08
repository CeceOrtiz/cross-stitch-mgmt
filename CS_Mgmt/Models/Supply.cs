using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Views;
using System.Windows;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class Supply
    {
        [PrimaryKey]
        public int SupplyId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }

        public static List<Supply> GetSupplies(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Supply> allSupplies = connection.Table<Supply>().ToList();
                return allSupplies;
            }
        }
        public static Supply GetSelectedSupply(string dbPath, int supplyID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                Supply selectedSupply = connection.Table<Supply>().FirstOrDefault(s => s.SupplyId == supplyID);
                return selectedSupply;
            }
        }
        public static void DeleteItem(string dbPath, int itemID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Table<Supply>().Delete(f => f.SupplyId == itemID);
            }

            MessageBox.Show("Item deleted.");

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
