using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SQLite;
using CS_Mgmt.Models;

namespace CS_Mgmt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string dbFilename = "cs_mgmt_db.db";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFilename);

            // Create db if it doesn't exist already
            if (!File.Exists(dbPath))
            {
                using (var dbContext = new SQLiteConnection(dbPath))
                {
                    // Enable foreign keys
                    dbContext.Execute("PRAGMA foreign_keys = ON;");

                    dbContext.CreateTable<Floss>();
                    dbContext.CreateTable<Pattern>();
                    dbContext.CreateTable<PatternFloss>();
                    dbContext.CreateTable<UserFloss>();
                    dbContext.CreateTable<Fabric>();
                    dbContext.CreateTable<Supply>();


                }

            }
        }
    }
}
