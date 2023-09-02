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

        }
        private void ItemsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
