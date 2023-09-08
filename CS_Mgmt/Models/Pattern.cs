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
    internal class Pattern
    {
        [PrimaryKey, AutoIncrement]
        public int PatternId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Dimensions { get; set; }
        public string FabricColor { get; set; }
        public string Creator { get; set; }
        public string Source { get; set; }
        public string StorageLocation { get; set; }

        public static List<Pattern> GetPatterns(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Pattern> allPatterns = connection.Table<Pattern>().ToList();
                return allPatterns;
            }
        }
        public static Pattern GetSelectedPattern(string dbPath, int patternID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                Pattern selectedPattern = connection.Table<Pattern>().FirstOrDefault(p => p.PatternId == patternID);
                return selectedPattern;
            }
        }
        public static void DeletePattern(string dbPath, int patternID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Table<Pattern>().Delete(p => p.PatternId == patternID);
            }

            MessageBox.Show("Pattern deleted.");

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
