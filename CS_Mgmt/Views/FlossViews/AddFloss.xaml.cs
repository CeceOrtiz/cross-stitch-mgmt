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
using CS_Mgmt.Models;

namespace CS_Mgmt.Views.FlossViews
{
    /// <summary>
    /// Interaction logic for AddFloss.xaml
    /// </summary>
    public partial class AddFloss : Page
    {
        public AddFloss()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Floss> nonUserFlossItems = Floss.GetFloss(App.DatabasePath);

            foreach (var floss in nonUserFlossItems)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                ColorCB.Items.Add(item);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
