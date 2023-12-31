﻿using System;
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
    internal class UserFloss
    {
        [PrimaryKey]
        public int FlossId { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }

        public static UserFloss GetSelectedUserFloss(string dbPath, int flossID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                UserFloss selectedUserFloss = connection.Table<UserFloss>().FirstOrDefault(uf => uf.FlossId == flossID);
                return selectedUserFloss;
            }
        }
        public static void DeleteFloss(string dbPath, int flossID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Table<UserFloss>().Delete(uf => uf.FlossId == flossID);
            }

            MessageBox.Show("Floss deleted.");

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
        public static List<UserFloss> GetPatternFlosses(string dbPath, List<PatternFloss> pFlosses)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<int> flossIds = pFlosses.Select(pf => pf.FlossId).ToList();

                List<UserFloss> uFlossEntries = connection.Table<UserFloss>()
                    .Where(uf => flossIds.Contains(uf.FlossId)).ToList();

                return uFlossEntries;
            }
        }
        public static List<UserFloss> GetAllUserFloss(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<UserFloss> allFloss = connection.Table<UserFloss>().ToList();

                return allFloss;
            }
        }
    }
}
