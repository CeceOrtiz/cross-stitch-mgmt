using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Views;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class Floss
    {
        [PrimaryKey]
        public int FlossId { get; set; }
        public string StandardName { get; set; }
        public string Color { get; set; }

        public static List<Floss> GetFloss(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Floss> allFloss = connection.Table<Floss>().ToList();
                List<int> userFlossIDs = connection.Table<UserFloss>().Select(u => u.FlossId).ToList();

                List<Floss> nonUserFloss = allFloss.Where(floss => !userFlossIDs.Contains(floss.FlossId)).ToList();

                return nonUserFloss;
            }
        }

        public static List<Floss> GetUserFloss(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Floss> allFloss = connection.Table<Floss>().ToList();
                List<int> userFlossIDs = connection.Table<UserFloss>().Select(u => u.FlossId).ToList();

                List<Floss> userFloss = allFloss.Where(floss => userFlossIDs.Contains(floss.FlossId)).ToList();

                return userFloss;
            }
        }

        public static Floss GetSelectedFloss(string dbPath, int flossID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                Floss selectedFloss = connection.Table<Floss>().FirstOrDefault(f => f.FlossId == flossID);
                return selectedFloss;
            }
        }

        public static List<Floss> GetAllFloss(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Floss> allFloss = connection.Table<Floss>().ToList();

                return allFloss;
            }
        }
        public static List<Floss> GetPatternFlosses(string dbPath, List<PatternFloss> pFlosses)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<int> flossIds = pFlosses.Select(pf => pf.FlossId).ToList();

                List<Floss> flossEntries = connection.Table<Floss>().Where(f => flossIds.Contains(f.FlossId)).ToList();

                return flossEntries;
            }
        }

    }
}
