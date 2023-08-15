using CS_Mgmt.Views.Dashboard;
using CS_Mgmt.Models;
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

namespace CS_Mgmt.Views.PatternViews
{
    /// <summary>
    /// Interaction logic for AddPattern.xaml
    /// </summary>
    public partial class AddPattern : Page
    {
        public AddPattern()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> statuses = new List<string> {"Not purchased", "Not started", "In-progress", "Incomplete", "Completed"};
            StatusCB.ItemsSource = statuses;

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

            for (int i = 1; i < 11; i++)
            {
                ComboBoxItem skeinsQty = new ComboBoxItem
                {
                    Content = i,
                    Tag = i
                };

                SkeinsCB.Items.Add(skeinsQty);
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
