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
using CS_Mgmt.Views.PatternViews;
using CS_Mgmt.Views.FlossViews;
using CS_Mgmt.Views.FabricsViews;
using CS_Mgmt.Views.SuppliesViews;
using CS_Mgmt.Views.ToolViews;
using CS_Mgmt.Models;

namespace CS_Mgmt.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dash : Page
    {
        public Dash()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Pattern> patterns = Pattern.GetPatterns(App.DatabasePath);
            foreach (var pattern in patterns)
            {
                ComboBoxItem patternItem = new ComboBoxItem
                {
                    Content = pattern.Name,
                    Tag = pattern.PatternId
                };

                PatternsCB.Items.Add(patternItem);
            }

            List<Floss> userFloss = Floss.GetUserFloss(App.DatabasePath);
            foreach (var floss in userFloss)
            {
                ComboBoxItem ufItem = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                FlossCB.Items.Add(ufItem);
            }

            List<Fabric> fabrics = Fabric.GetFabrics(App.DatabasePath);
            foreach (var fabric in fabrics)
            {
                ComboBoxItem fabricItem = new ComboBoxItem
                {
                    Content = $"{fabric.Type} - {fabric.Color} - {fabric.Count} ct",
                    Tag = fabric.FabricId
                };

                FabricsCB.Items.Add(fabricItem);
            }

            List<Supply> supplies = Supply.GetSupplies(App.DatabasePath);
            foreach (var supply in supplies)
            {
                ComboBoxItem supplyItem = new ComboBoxItem
                {
                    Content = supply.Description,
                    Tag = supply.SupplyId
                };

                SuppliesCB.Items.Add(supplyItem);
            }
        }

        private void AddPattern_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddPattern());
        }

        private void ViewEditPattern_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ViewEditPattern());
        }

        private void AddFloss_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddFloss());
        }

        private void ViewEditFloss_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedCBItem = FlossCB.SelectedItem as ComboBoxItem;
            int selectedFlossId = (int)selectedCBItem.Tag;

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ViewEditFloss(selectedFlossId));
        }

        private void AddFabric_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddFabric());
        }

        private void ViewEditFabric_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedFabric = FabricsCB.SelectedItem as ComboBoxItem;
            int selectedFabricID = (int)selectedFabric.Tag;

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ViewEditFabric(selectedFabricID));
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddItem());
        }

        private void ViewEditItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ViewEditItem());
        }

        private void ShoppingList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ShoppingListReport());
        }
    }
}
