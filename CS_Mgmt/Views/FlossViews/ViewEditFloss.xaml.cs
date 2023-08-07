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

namespace CS_Mgmt.Views.FlossViews
{
    /// <summary>
    /// Interaction logic for ViewEditFloss.xaml
    /// </summary>
    public partial class ViewEditFloss : Page
    {
        private int selectedFlossId;

        public ViewEditFloss(int flossId)
        {
            InitializeComponent();
            selectedFlossId = flossId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Populate the quantity box
            for (int i = 0; i < 16; i++)
            {
                ComboBoxItem qtyItem = new ComboBoxItem
                {
                    Content = i,
                    Tag = i
                };

                QuantityCB.Items.Add(qtyItem);
            }

            PopulateFields(selectedFlossId);
        }

        private void PopulateFields(int selectedFlossId)
        {
            Floss floss = Floss.GetSelectedFloss(App.DatabasePath, selectedFlossId);
            ColorTB.Text = $"{floss.StandardName} - {floss.Color}";

            UserFloss uFloss = UserFloss.GetSelectedUserFloss(App.DatabasePath, selectedFlossId);
            StorageLocationTB.Text = uFloss.StorageLocation;

            foreach (ComboBoxItem item in QuantityCB.Items)
            {
                if (item.Content.ToString() == uFloss.Quantity.ToString())
                {
                    QuantityCB.SelectedItem = item;
                    break;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }
    }
}
