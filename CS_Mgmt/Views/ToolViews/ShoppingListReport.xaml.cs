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
    }
}
