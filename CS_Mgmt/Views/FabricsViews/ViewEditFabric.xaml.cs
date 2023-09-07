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
    /// Interaction logic for ViewEditFabric.xaml
    /// </summary>
    public partial class ViewEditFabric : Page
    {
        private int selectedFabricID;
        public ViewEditFabric(int fabricID)
        {
            InitializeComponent();
            selectedFabricID = fabricID;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateFields(selectedFabricID);
        }
        private void PopulateFields(int selectedFabricId)
        {
            Fabric fabric = Fabric.GetSelectedFabric(App.DatabasePath, selectedFabricId);
            TypeTB.Text = fabric.Type;
            ColorTB.Text = fabric.Color;
            CountTB.Text = fabric.Count.ToString();
            StorageLocationTB.Text = fabric.StorageLocation;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string newType = TypeTB.Text;
            string newColor = ColorTB.Text;
            string countStr = CountTB.Text;
            string newStorage = StorageLocationTB.Text;

            bool continueUpdate = FabricValidation.ValidFabric(newType, newColor, countStr);

            if (continueUpdate == true)
            {
                int newCount = string.IsNullOrEmpty(CountTB.Text) ? 0 : int.Parse(CountTB.Text);

                Fabric fabric = new Fabric
                {
                    FabricId = selectedFabricID,
                    Type = newType,
                    Color = newColor,
                    Count = newCount,
                    StorageLocation = newStorage
                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Update(fabric);
                }

                MessageBox.Show("Updates saved!");

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
