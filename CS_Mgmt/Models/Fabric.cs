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
    internal class Fabric
    {
        [PrimaryKey, AutoIncrement]
        public int FabricId { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
        public string StorageLocation { get; set; }

        public static List<Fabric> GetFabrics(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Fabric> allFabrics = connection.Table<Fabric>().ToList();
                return allFabrics;
            }
        }
        public static Fabric GetSelectedFabric(string dbPath, int fabricID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                Fabric selectedFabric = connection.Table<Fabric>().FirstOrDefault(f => f.FabricId == fabricID);
                return selectedFabric;
            }
        }
        public static void DeleteFabric(string dbPath, int fabricID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Table<Fabric>().Delete(f => f.FabricId == fabricID);
            }

            MessageBox.Show("Fabric deleted.");

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
