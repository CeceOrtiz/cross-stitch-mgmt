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
using CS_Mgmt.Views.Dashboard;
using OfficeOpenXml;
using CS_Mgmt.Models;
using System.IO;

namespace CS_Mgmt.Views.ToolViews
{
    /// <summary>
    /// Interaction logic for InventoryReport.xaml
    /// </summary>
    public partial class InventoryReport : Page
    {
        public InventoryReport()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatePatterns(App.DatabasePath);
            PopulateFabrics(App.DatabasePath);
            PopulateOtherItems(App.DatabasePath);
            PopulateFloss(App.DatabasePath);
        }

        // Methods for populating DGs
        private void PopulatePatterns(string dbPath)
        {
            List<Pattern> patterns = Pattern.GetPatterns(dbPath);
            foreach (Pattern p in patterns)
            {
                PatternItem pi = new PatternItem
                {
                    Name = p.Name,
                    StorageLocation = p.StorageLocation
                };

                PatternsDG.Items.Add(pi);
            }
        }
        private void PopulateFabrics(string dbPath)
        {
            List<Fabric> fabrics = Fabric.GetFabrics(dbPath);
            foreach (Fabric f in fabrics)
            {
                FabricItem fi = new FabricItem
                {
                    Color = f.Color,
                    Type = f.Type,
                    Count = f.Count.ToString(),
                    StorageLocation = f.StorageLocation
                };

                FabricsDG.Items.Add(fi);
            }
        }
        private void PopulateOtherItems(string dbPath)
        {
            List<Supply> supplies = Supply.GetSupplies(dbPath);
            foreach (Supply s in supplies)
            {
                OtherItem oi = new OtherItem
                {
                    Item = s.Description,
                    Quantity = s.Quantity.ToString(),
                    StorageLocation = s.StorageLocation
                };

                ItemsDG.Items.Add(oi);
            }
        }
        private void PopulateFloss(string dbPath)
        {
            List<UserFloss> userFlosses = UserFloss.GetAllUserFloss(dbPath);
            List<Floss> flosses = Floss.GetUserFloss(dbPath);

            foreach (UserFloss uf in userFlosses)
            {
                FlossItem fi = new FlossItem
                {
                    StandardName = flosses.FirstOrDefault(f => f.FlossId == uf.FlossId).StandardName,
                    Color = flosses.FirstOrDefault(f => f.FlossId == uf.FlossId).Color,
                    Quantity = uf.Quantity.ToString(),
                    StorageLocation = uf.StorageLocation
                };

                FlossDG.Items.Add(fi);
            }
        }

        // Event handler for double-clicking DGs
        private void PatternsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void ItemTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = ItemTypeCB.SelectedItem as ComboBoxItem;

            if (selectedItem.Content.ToString() == "Patterns")
            {
                PatternsDG.Visibility = Visibility.Visible;
                PatternsDG.IsEnabled = true;

                // make everything else invisible/disabled
                FlossDG.Visibility = Visibility.Hidden;
                FlossDG.IsEnabled = false;
                FabricsDG.Visibility = Visibility.Hidden;
                FabricsDG.IsEnabled = false;
                ItemsDG.Visibility = Visibility.Hidden;
                ItemsDG.IsEnabled = false;
            }
            else if (selectedItem.Content.ToString() == "Floss")
            {
                // make floss enabled/visible
                FlossDG.Visibility = Visibility.Visible;
                FlossDG.IsEnabled = true;

                // make everything else invisible/disabled
                PatternsDG.Visibility = Visibility.Hidden;
                PatternsDG.IsEnabled = false;
                FabricsDG.Visibility = Visibility.Hidden;
                FabricsDG.IsEnabled = false;
                ItemsDG.Visibility = Visibility.Hidden;
                ItemsDG.IsEnabled = false;
            }
            else if (selectedItem.Content.ToString() == "Fabrics")
            {
                // make fabrics enabled/visible
                FabricsDG.Visibility = Visibility.Visible;
                FabricsDG.IsEnabled = true;

                // make everything else invisible/disabled
                PatternsDG.Visibility = Visibility.Hidden;
                PatternsDG.IsEnabled = false;
                FlossDG.Visibility = Visibility.Hidden;
                FlossDG.IsEnabled = false;
                ItemsDG.Visibility = Visibility.Hidden;
                ItemsDG.IsEnabled = false;
            }
            else if (selectedItem.Content.ToString() == "Other Items")
            {
                // make other items enabled/visible
                ItemsDG.Visibility = Visibility.Visible;
                ItemsDG.IsEnabled = true;

                // make everything else invisible/disabled
                PatternsDG.Visibility = Visibility.Hidden;
                PatternsDG.IsEnabled = false;
                FlossDG.Visibility = Visibility.Hidden;
                FlossDG.IsEnabled = false;
                FabricsDG.Visibility = Visibility.Hidden;
                FabricsDG.IsEnabled = false;
            }
        }

        

        // Save to Excel

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        // Classes for DG items
        private class PatternItem
        {
            public string Name { get; set; }
            public string StorageLocation { get; set; }
        }
        private class FlossItem
        {
            public string StandardName { get; set; }
            public string Color { get; set; }
            public string Quantity { get; set; }
            public string StorageLocation { get; set; }
        }
        private class FabricItem
        {
            public string Color { get; set; }
            public string Type { get; set; }
            public string Count { get; set; }
            public string StorageLocation { get; set; }
        }
        private class OtherItem
        {
            public string Item { get; set; }
            public string Quantity { get; set; }
            public string StorageLocation { get; set; }
        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Change this if making commercial

            using (var package = new ExcelPackage())
            {
                SaveToSpreadsheet(package, PatternsDG, "Patterns");
                SaveToSpreadsheet(package, FlossDG, "Floss");
                SaveToSpreadsheet(package, FabricsDG, "Fabrics");
                SaveToSpreadsheet(package, ItemsDG, "Other Items");

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string excelFilePath = System.IO.Path.Combine(folderPath, "Inventory.xslx");
                File.WriteAllBytes(excelFilePath, package.GetAsByteArray());

                MessageBox.Show($"Spreadsheet saved to: \n{excelFilePath}", "Spreadsheet Saved", MessageBoxButton.OK);
            }
        }

        private void SaveToSpreadsheet(ExcelPackage package, DataGrid dg, string sheetName)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);

            for (int col = 0; col < dg.Columns.Count; col++)
            {
                worksheet.Cells[1, col + 1].Value = dg.Columns[col].Header;
            }

            for (int row = 0; row < dg.Items.Count; row++)
            {
                var item = dg.Items[row];

                for (int column = 0; column < dg.Columns.Count; column++)
                {
                    var cellValue = dg.Columns[column].GetCellContent(item);
                    worksheet.Cells[row + 2, column + 1].Value = (cellValue as TextBlock)?.Text;
                }
            }
        }
    }
}
