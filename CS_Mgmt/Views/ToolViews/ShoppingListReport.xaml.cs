using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
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

namespace CS_Mgmt.Views.ToolViews
{
    /// <summary>
    /// Interaction logic for ShoppingListReport.xaml
    /// </summary>
    public partial class ShoppingListReport : Page
    {
        public ShoppingListReport()
        {
            InitializeComponent();
        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void ItemTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Pattern")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Visible;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Visible;
                QuantityLabel2.Visibility = Visibility.Hidden;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Hidden;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Hidden;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Hidden;
            }

            else if (itemType == "Floss")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Visible;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Visible;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Visible;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Hidden;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Visible;
            }

            else if (itemType == "Fabric")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Visible;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Hidden;
                ColorLabel2.Visibility = Visibility.Visible;
                StoreLabel3.Visibility = Visibility.Hidden;
                CountLabel3.Visibility = Visibility.Visible;
                QuantityLabel4.Visibility = Visibility.Visible;
                StoreLabel5.Visibility = Visibility.Visible;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Visible;
                FifthTB.Visibility = Visibility.Visible;
                FlossColorCB.Visibility = Visibility.Hidden;
            }

            else if (itemType == "Other Item")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Visible;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Visible;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Visible;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Hidden;
            }
        }
        private void ItemsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Pattern")
            {
                ShoppingListItem sli = new ShoppingListItem
                {
                    Item = "Pattern",
                    Name = FirstTB.Text,
                    Quantity = 1,
                    Store = SecondTB.Text
                };
                ItemsDG.Items.Add(sli);
                FirstTB.Clear();
                SecondTB.Clear();
            }

            else if (itemType == "Floss")
            {
                ComboBoxItem selectedFloss = FlossColorCB.SelectedItem as ComboBoxItem;

                ShoppingListItem sli = new ShoppingListItem
                {
                    Item = "Floss",
                    Name = selectedFloss.Content.ToString(),
                    Quantity = int.Parse(SecondTB.Text),
                    Store = ThirdTB.Text
                };
                ItemsDG.Items.Add(sli);
                SecondTB.Clear();
                ThirdTB.Clear();
            }

            else if (itemType == "Fabric")
            {
                ShoppingListItem sli = new ShoppingListItem
                {
                    Item = "Fabric",
                    Name = FirstTB.Text + " - " + SecondTB.Text + " - " + ThirdTB.Text + " ct.",
                    Quantity = int.Parse(FourthTB.Text),
                    Store = FifthTB.Text
                };
                ItemsDG.Items.Add(sli);
                FirstTB.Clear();
                SecondTB.Clear();
                ThirdTB.Clear();
                FourthTB.Clear();
                FifthTB.Clear();
            }

            else if (itemType == "Other Item")
            {
                ShoppingListItem sli = new ShoppingListItem
                {
                    Item = "Other Item",
                    Name = FirstTB.Text,
                    Quantity = int.Parse(SecondTB.Text),
                    Store = ThirdTB.Text
                };
                ItemsDG.Items.Add(sli);
                FirstTB.Clear();
                SecondTB.Clear();
                ThirdTB.Clear();
            }
        }

        private class ShoppingListItem
        {
            public string Item { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public string Store { get; set; }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Floss> allFloss = Floss.GetAllFloss(App.DatabasePath);
            foreach (var floss in allFloss)
            {
                ComboBoxItem flossItem = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                FlossColorCB.Items.Add(flossItem);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDG.SelectedItem != null)
            {
                ShoppingListItem selectedItem = (ShoppingListItem)ItemsDG.SelectedItem;
                ItemsDG.Items.Remove(selectedItem);
            }
        }
    }
}
