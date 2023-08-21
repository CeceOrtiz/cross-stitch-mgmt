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

namespace CS_Mgmt.Views.PatternViews
{
    /// <summary>
    /// Interaction logic for ViewEditPattern.xaml
    /// </summary>
    public partial class ViewEditPattern : Page
    {
        private int selectedPatternID;
        public ViewEditPattern(int patternID)
        {
            InitializeComponent();
            selectedPatternID = patternID;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> statuses = new List<string> { "Not purchased", "Not started", "In-progress", "Incomplete", "Completed" };
            foreach (var status in statuses)
            {
                ComboBoxItem statusItem = new ComboBoxItem
                {
                    Content = status,
                    Tag = status
                };

                StatusCB.Items.Add(statusItem);
            }

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

            PopulateFields(selectedPatternID);
        }
        private void PopulateFields(int selectedPatternID)
        {
            Pattern pattern = Pattern.GetSelectedPattern(App.DatabasePath, selectedPatternID);

            NameTB.Text = pattern.Name;
            DimensionsTB.Text = pattern.Dimensions;
            FabricColorTB.Text = pattern.FabricColor;
            CreatorTB.Text = pattern.Creator;
            SourceTB.Text = pattern.Source;
            StorageLocationTB.Text = pattern.StorageLocation;

            foreach (ComboBoxItem item in StatusCB.Items)
            {
                if (item.Content.ToString() == pattern.Status.ToString())
                {
                    StatusCB.SelectedItem = item;
                    break;
                }
            }

            // Populate the datagrid
            List<PatternFloss> pFlosses = PatternFloss.GetPatternFlosses(App.DatabasePath, selectedPatternID);
            List<Floss> assocFlosses = Floss.GetPatternFlosses(App.DatabasePath, pFlosses);

            foreach (PatternFloss p in pFlosses)
            {
                Floss thisFloss = assocFlosses.FirstOrDefault(f => f.FlossId == p.FlossId);

                PatternColorsDG.Items.Add(new PatternColorItem
                {
                    FlossID = p.FlossId.ToString(),
                    Color = $"{thisFloss.StandardName} - {thisFloss.Color}",
                    SkeinsNeeded = p.SkeinsNeeded.ToString()
                });
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



        private class PatternColorItem
        {
            public string FlossID { get; set; }
            public string Color { get; set; }
            public string SkeinsNeeded { get; set; }
        }
    }
}
