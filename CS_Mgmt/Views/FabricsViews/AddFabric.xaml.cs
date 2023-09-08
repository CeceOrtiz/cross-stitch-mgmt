using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Validations;
using SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace CS_Mgmt.Views.FabricsViews
{
    /// <summary>
    /// Interaction logic for AddFabric.xaml
    /// </summary>
    public partial class AddFabric : Page
    {
        #region Initialization
        public AddFabric()
        {
            InitializeComponent();
        }
        #endregion

        #region Buttons
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
        #endregion

        #region TextBox Limitations
        private void CountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(text);
        }
        #endregion
    }
}
