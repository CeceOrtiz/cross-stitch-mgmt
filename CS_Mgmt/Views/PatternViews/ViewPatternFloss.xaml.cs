﻿using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OfficeOpenXml;

namespace CS_Mgmt.Views.PatternViews
{
    /// <summary>
    /// Interaction logic for ViewPatternFloss.xaml
    /// </summary>
    public partial class ViewPatternFloss : Page
    {
        #region Initialization
        private int selectedPatternID;
        public ViewPatternFloss(int patternId)
        {
            InitializeComponent();
            selectedPatternID = patternId;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Populate the datagrid
            List<PatternFloss> pFlosses = PatternFloss.GetPatternFlosses(App.DatabasePath, selectedPatternID);
            List<Floss> assocFlosses = Floss.GetPatternFlosses(App.DatabasePath, pFlosses);
            List<UserFloss> uFlosses = UserFloss.GetPatternFlosses(App.DatabasePath, pFlosses);

            foreach (PatternFloss p in pFlosses)
            {
                Floss thisFloss = assocFlosses.FirstOrDefault(f => f.FlossId == p.FlossId);
                UserFloss thisUFloss = uFlosses.FirstOrDefault(u => u.FlossId == p.FlossId);

                PatternColorsDG.Items.Add(new PatternColorItem
                {
                    FlossID = p.FlossId.ToString(),
                    Color = $"{thisFloss.StandardName} - {thisFloss.Color}",
                    SkeinsNeeded = p.SkeinsNeeded.ToString(),
                    SkeinsOwned = thisUFloss?.Quantity.ToString() ?? "0"
                });
            }
        }

        private void PatternColorsDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PatternColorItem thisItem = e.Row.Item as PatternColorItem;
            int owned = int.Parse(thisItem.SkeinsOwned);
            int needed = int.Parse(thisItem.SkeinsNeeded);

            if (owned == 0 || owned < needed)
            {
                e.Row.Background = Brushes.Yellow;
            }
            else
            {
                e.Row.ClearValue(DataGridRow.BackgroundProperty);
            }
        }
        #endregion

        #region Classes
        private class PatternColorItem
        {
            public string FlossID { get; set; }
            public string Color { get; set; }
            public string SkeinsNeeded { get; set; }
            public string SkeinsOwned { get; set; }
        }

        #endregion

        #region Buttons
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Change this if making commercial

            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pattern Floss");

                for (int col = 0; col < PatternColorsDG.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = PatternColorsDG.Columns[col].Header;
                }

                for (int row = 0; row < PatternColorsDG.Items.Count; row++)
                {
                    var item = PatternColorsDG.Items[row];

                    for (int column = 0; column < PatternColorsDG.Columns.Count; column++)
                    {
                        var cellValue = PatternColorsDG.Columns[column].GetCellContent(item);
                        worksheet.Cells[row + 2, column + 1].Value = (cellValue as TextBlock)?.Text;
                    }
                }

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string excelFilePath = System.IO.Path.Combine(folderPath, "PatternFloss.xlsx");
                string baseFileName = "PatternFloss";

                int fileCounter = 1;
                while (File.Exists(excelFilePath))
                {
                    string tempFileName = $"{baseFileName}({fileCounter}).xlsx";
                    excelFilePath = System.IO.Path.Combine(folderPath, tempFileName);
                    fileCounter++;
                }

                File.WriteAllBytes(excelFilePath, package.GetAsByteArray());

                MessageBox.Show($"Spreadsheet saved to: \n{excelFilePath}", "Spreadsheet Saved", MessageBoxButton.OK);
            }
        }
        #endregion

        #region Limitations
        private void PatternColorsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        #endregion
        
    }
}
