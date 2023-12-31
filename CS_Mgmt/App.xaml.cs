﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CS_Mgmt.Models;
using SQLite;
using CsvHelper;
using CS_Mgmt.Views;
using CS_Mgmt.Views.Dashboard;

namespace CS_Mgmt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string DatabasePath { get; } = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.LocalApplicationData), "cs_mgmt_db.db");
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = Current.MainWindow as MainWindow;

            if (mainWindow == null )
            {
                mainWindow = new MainWindow();
                Current.MainWindow = mainWindow;
            }

            mainWindow.MainFrame.Navigate(new Dash());
            mainWindow.Show();

            string dbFilename = "cs_mgmt_db.db";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFilename);

            // Create db if it doesn't exist already
            if (!File.Exists(dbPath))
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    // Enable foreign keys
                    db.Execute("PRAGMA foreign_keys = ON;");

                    // Create all the tables
                    db.CreateTable<Floss>();
                    db.CreateTable<Pattern>();
                    db.CreateTable<PatternFloss>();
                    db.CreateTable<UserFloss>();
                    db.CreateTable<Fabric>();
                    db.CreateTable<Supply>();

                    // Populate the Floss table with the DMC Floss CSV
                    string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "floss_data.csv");

                    if (!db.Table<Floss>().Any())
                    {
                        using (var reader = new  StreamReader(csvFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<Floss>().ToList();

                            db.BeginTransaction();

                            try
                            {
                                db.InsertAll(records);
                                db.Commit();
                            }

                            catch (Exception ex)
                            {
                                db.Rollback();
                                Console.WriteLine($"Exception: {ex.Message}");
                            }
                        }
                    }
                }

            }
        }
    }
}
