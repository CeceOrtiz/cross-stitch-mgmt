using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Validations;
using SQLite;
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
using System.Text.RegularExpressions;

namespace CS_Mgmt.Views.FabricsViews
{
    /// <summary>
    /// Interaction logic for AddFabric.xaml
    /// </summary>
    public partial class AddFabric : Page
    {
        public AddFabric()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save the fabric
            string fabricType = TypeTB.Text;
            string fabricColor = ColorTB.Text;
            string countStr = CountTB.Text;
            string storageLocation = StorageLocationTB.Text;

            bool continueSave = FabricValidation.ValidFabric(fabricType, fabricColor, countStr);

            if (continueSave == true)
            {
                int fabricCount = string.IsNullOrEmpty(CountTB.Text) ? 0 : int.Parse(CountTB.Text);

                Fabric newFabric = new Fabric
                {
                    Type = fabricType,
                    Color = fabricColor,
                    Count = fabricCount,
                    StorageLocation = storageLocation
                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Insert(newFabric);
                }

                MessageBox.Show("Fabric saved!");

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new Dash());
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void CountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(text);
        }
    }
}
