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
    }
}
