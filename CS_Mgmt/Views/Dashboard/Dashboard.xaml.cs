﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CS_Mgmt.Views.PatternViews;
using CS_Mgmt.Views.FlossViews;
using CS_Mgmt.Views.FabricsViews;
using CS_Mgmt.Views.SuppliesViews;
using CS_Mgmt.Views.ToolViews;
using CS_Mgmt.Models;
using CS_Mgmt.Validations;

namespace CS_Mgmt.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dash : Page
    {
        #region Initialization
        public Dash()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Pattern> patterns = Pattern.GetPatterns(App.DatabasePath);
            foreach (var pattern in patterns)
            {
                ComboBoxItem patternItem = new ComboBoxItem
                {
                    Content = pattern.Name,
                    Tag = pattern.PatternId
                };

                PatternsCB.Items.Add(patternItem);
            }

            List<Floss> userFloss = Floss.GetUserFloss(App.DatabasePath);
            foreach (var floss in userFloss)
            {
                ComboBoxItem ufItem = new ComboBoxItem
                {
                    Content = $"{floss.StandardName} - {floss.Color}",
                    Tag = floss.FlossId
                };

                FlossCB.Items.Add(ufItem);
            }

            List<Fabric> fabrics = Fabric.GetFabrics(App.DatabasePath);
            foreach (var fabric in fabrics)
            {
                ComboBoxItem fabricItem = new ComboBoxItem
                {
                    Content = $"{fabric.Type} - {fabric.Color} - {fabric.Count} ct",
                    Tag = fabric.FabricId
                };

                FabricsCB.Items.Add(fabricItem);
            }

            List<Supply> supplies = Supply.GetSupplies(App.DatabasePath);
            foreach (var supply in supplies)
            {
                ComboBoxItem supplyItem = new ComboBoxItem
                {
                    Content = supply.Description,
                    Tag = supply.SupplyId
                };

                SuppliesCB.Items.Add(supplyItem);
            }
        }
        #endregion

        #region Pattern
        private void AddPattern_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddPattern());
        }

        private void ViewEditPattern_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedPattern = PatternsCB.SelectedItem as ComboBoxItem;

            bool continueEdit = PatternValidation.ValidSelectedPattern(selectedPattern);

            if (continueEdit == true)
            {
                int selectedPatternId = (int)selectedPattern.Tag;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new ViewEditPattern(selectedPatternId));
            }
        }
        private void DeletePattern_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedPattern = PatternsCB.SelectedItem as ComboBoxItem;

            bool continueDelete = PatternValidation.ValidSelectedPattern(selectedPattern);

            if (continueDelete == true)
            {
                if (MessageBox.Show("Are you sure you want to delete this pattern?", "Confirm Delete", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
                {
                    int selectedPatternId = (int)selectedPattern.Tag;

                    Pattern.DeletePattern(App.DatabasePath, selectedPatternId);
                    PatternFloss.DeletePatternFloss(App.DatabasePath, selectedPatternId);
                }
            }
        }
        private void FlossNeeded_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedPattern = PatternsCB.SelectedItem as ComboBoxItem;

            bool continueToPage = PatternValidation.ValidSelectedPattern(selectedPattern);

            if (continueToPage == true)
            {
                int selectedPatternId = (int)selectedPattern.Tag;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new ViewPatternFloss(selectedPatternId));
            }
        }
        #endregion

        #region Floss
        private void AddFloss_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddFloss());
        }

        private void ViewEditFloss_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedCBItem = FlossCB.SelectedItem as ComboBoxItem;

            bool continueEdit = FlossValidation.ValidSelectedFloss(selectedCBItem);

            if (continueEdit == true)
            {
                int selectedFlossId = (int)selectedCBItem.Tag;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new ViewEditFloss(selectedFlossId));
            }
        }
        private void DeleteFloss_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedFloss = FlossCB.SelectedItem as ComboBoxItem;

            bool continueDelete = FlossValidation.ValidSelectedFloss(selectedFloss);

            if (continueDelete == true)
            {
                if (MessageBox.Show("Are you sure you want to delete this floss?", "Confirm Delete", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
                {
                    int selectedFlossId = (int)selectedFloss.Tag;

                    UserFloss.DeleteFloss(App.DatabasePath, selectedFlossId);
                }
            }
        }

        #endregion

        #region Fabric
        private void AddFabric_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddFabric());
        }

        private void ViewEditFabric_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedFabric = FabricsCB.SelectedItem as ComboBoxItem;

            bool continueEdit = FabricValidation.ValidSelectedFabric(selectedFabric);

            if (continueEdit == true)
            {
                int selectedFabricID = (int)selectedFabric.Tag;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new ViewEditFabric(selectedFabricID));
            }
        }
        private void DeleteFabrics_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedFabric = FabricsCB.SelectedItem as ComboBoxItem;
            bool continueDelete = FabricValidation.ValidSelectedFabric(selectedFabric);

            if (continueDelete == true)
            {
                if (MessageBox.Show("Are you sure you want to delete this fabric?", "Confirm Delete", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
                {
                    int selectedFabricId = (int)selectedFabric.Tag;

                    Fabric.DeleteFabric(App.DatabasePath, selectedFabricId);
                }
            }
        }
        #endregion

        #region Supplies
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new AddItem());
        }

        private void ViewEditItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedSupply = SuppliesCB.SelectedItem as ComboBoxItem;

            bool continueEdit = ItemValidation.ValidSelectedItem(selectedSupply);

            if (continueEdit == true)
            {
                int selectedSupplyID = (int)selectedSupply.Tag;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new ViewEditItem(selectedSupplyID));
            }
        }
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = SuppliesCB.SelectedItem as ComboBoxItem;
            bool continueDelete = ItemValidation.ValidSelectedItem(selectedItem);

            if (continueDelete == true)
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButton.YesNo)
               == MessageBoxResult.Yes)
                {
                    int selectedItemId = (int)selectedItem.Tag;

                    Supply.DeleteItem(App.DatabasePath, selectedItemId);
                }
            }
        }
        #endregion

        #region Tools
        private void ShoppingList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ShoppingListReport());
        }
        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new InventoryReport());
        }
        #endregion


    }
}
