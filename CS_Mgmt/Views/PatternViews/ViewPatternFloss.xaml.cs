using CS_Mgmt.Models;
using CS_Mgmt.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ViewPatternFloss.xaml
    /// </summary>
    public partial class ViewPatternFloss : Page
    {
        private int selectedPatternID;
        public ViewPatternFloss(int patternId)
        {
            InitializeComponent();
            selectedPatternID = patternId;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Populate the datagrid
            List<PatternFloss> pFlosses = PatternFloss.GetPatternFlosses(App.DatabasePath, selectedPatternID);
            List<Floss> assocFlosses = Floss.GetPatternFlosses(App.DatabasePath, pFlosses);
            List<UserFloss> uFlosses = UserFloss.GetPatternFlosses(App.DatabasePath, pFlosses);

            foreach (PatternFloss p in pFlosses)
            {
                Floss thisFloss = assocFlosses.FirstOrDefault(f => f.FlossId == p.FlossId);
                UserFloss thisUFloss = uFlosses.FirstOrDefault(u => u.FlossId == p.FlossId);

                PatternColorsDG.Items.Add(new PatternColorItem
                {
                    FlossID = p.FlossId.ToString(),
                    Color = $"{thisFloss.StandardName} - {thisFloss.Color}",
                    SkeinsNeeded = p.SkeinsNeeded.ToString(),
                    SkeinsOwned = thisUFloss?.Quantity.ToString() ?? "0"
                });
            }
        }
        private class PatternColorItem
        {
            public string FlossID { get; set; }
            public string Color { get; set; }
            public string SkeinsNeeded { get; set; }
            public string SkeinsOwned { get; set; }
        }

        private void PatternColorsDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PatternColorItem thisItem = e.Row.Item as PatternColorItem;
            int owned = int.Parse(thisItem.SkeinsOwned);
            int needed = int.Parse(thisItem.SkeinsNeeded);

            if (owned == 0 || owned < needed)
            {
                e.Row.Background = Brushes.Yellow;
            }
            else
            {
                e.Row.ClearValue(DataGridRow.BackgroundProperty);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void PatternColorsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true){
                printDialog.PrintVisual(PatternColorsDG, "List of Floss");
            }
            // Add saving to PDF
            
        }
    }
}
