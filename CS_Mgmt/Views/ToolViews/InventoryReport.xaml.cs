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

        }

        // Event handlers for double-clicking DGs
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

    }
}
