using CS_Mgmt.Models;
using CS_Mgmt.Validations;
using CS_Mgmt.Views.Dashboard;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CS_Mgmt.Views.ToolViews
{
    /// <summary>
    /// Interaction logic for ShoppingListReport.xaml
    /// </summary>
    public partial class ShoppingListReport : Page
    {
        #region Initialization
        public ShoppingListReport()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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

            ItemTypeCB.SelectedIndex = 0;
        }
        #endregion

        #region Buttons

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Change this if making commercial

            using (var package = new ExcelPackage())
            {
                SaveToSpreadsheet(package, ItemsDG, "Shopping List");

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string excelFilePath = System.IO.Path.Combine(folderPath, "ShoppingList.xlsx");
                string baseFileName = "ShoppingList";

                int fileCounter = 1;
                while (File.Exists(excelFilePath))
                {
                    string tempFileName = $"{baseFileName}({fileCounter}).xlsx";
                    excelFilePath = System.IO.Path.Combine(folderPath, tempFileName);
                    fileCounter++;
                }

                File.WriteAllBytes(excelFilePath, package.GetAsByteArray());

                MessageBox.Show($"Spreadsheet saved to: \n{excelFilePath}", "Spreadsheet Saved", MessageBoxButton.OK);
            }
        }
        private void SaveToSpreadsheet(ExcelPackage package, DataGrid dg, string sheetName)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);

            for (int col = 0; col < dg.Columns.Count; col++)
            {
                worksheet.Cells[1, col + 1].Value = dg.Columns[col].Header;
            }

            for (int row = 0; row < dg.Items.Count; row++)
            {
                var item = dg.Items[row];

                for (int column = 0; column < dg.Columns.Count; column++)
                {
                    var cellValue = dg.Columns[column].GetCellContent(item);
                    worksheet.Cells[row + 2, column + 1].Value = (cellValue as TextBlock)?.Text;
                }
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new Dash());
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Pattern")
            {
                bool continueSave = ShoppingValidation.PatternValidation(FirstTB.Text);

                if (continueSave == true)
                {
                    ShoppingListItem sli = new ShoppingListItem
                    {
                        Item = "Pattern",
                        Name = FirstTB.Text,
                        Quantity = "1",
                        Store = SecondTB.Text
                    };
                    ItemsDG.Items.Add(sli);
                    FirstTB.Clear();
                    SecondTB.Clear();
                }
            }

            else if (itemType == "Floss")
            {
                ComboBoxItem selectedFloss = FlossColorCB.SelectedItem as ComboBoxItem;

                bool continueSave = ShoppingValidation.FlossValidation(selectedFloss);

                if (continueSave == true)
                {
                    ShoppingListItem sli = new ShoppingListItem
                    {
                        Item = "Floss",
                        Name = selectedFloss.Content.ToString(),
                        Quantity = string.IsNullOrEmpty(SecondTB.Text) ? "1" : SecondTB.Text,
                        Store = ThirdTB.Text
                    };
                    ItemsDG.Items.Add(sli);
                    SecondTB.Clear();
                    ThirdTB.Clear();
                }
            }

            else if (itemType == "Fabric")
            {
                bool continueSave = ShoppingValidation.FabricValidation(FirstTB.Text, SecondTB.Text);

                if (continueSave == true)
                {
                    int count = 0;
                    if (!string.IsNullOrEmpty(ThirdTB.Text))
                    {
                        count = int.Parse(ThirdTB.Text);
                    }

                    ShoppingListItem sli = new ShoppingListItem
                    {
                        Item = "Fabric",
                        Name = FirstTB.Text + " - " + SecondTB.Text + " - " + count + " ct.",
                        Quantity = string.IsNullOrEmpty(FourthTB.Text) ? "1" : FourthTB.Text,
                        Store = FifthTB.Text
                    };
                    ItemsDG.Items.Add(sli);
                    FirstTB.Clear();
                    SecondTB.Clear();
                    ThirdTB.Clear();
                    FourthTB.Clear();
                    FifthTB.Clear();
                }
            }

            else if (itemType == "Other Item")
            {
                bool continueSave = ShoppingValidation.ItemValidation(FirstTB.Text);

                if (continueSave == true)
                {
                    ShoppingListItem sli = new ShoppingListItem
                    {
                        Item = "Other Item",
                        Name = FirstTB.Text,
                        Quantity = string.IsNullOrEmpty(SecondTB.Text) ? "1" : SecondTB.Text,
                        Store = ThirdTB.Text
                    };
                    ItemsDG.Items.Add(sli);
                    FirstTB.Clear();
                    SecondTB.Clear();
                    ThirdTB.Clear();
                }
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDG.SelectedItem != null)
            {
                ShoppingListItem selectedItem = (ShoppingListItem)ItemsDG.SelectedItem;
                ItemsDG.Items.Remove(selectedItem);
            }
        }

        #endregion

        #region Form
        private void ItemTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Pattern")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Visible;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Visible;
                QuantityLabel2.Visibility = Visibility.Hidden;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Hidden;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Hidden;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Hidden;
            }

            else if (itemType == "Floss")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Visible;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Visible;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Visible;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Hidden;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Visible;
            }

            else if (itemType == "Fabric")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Hidden;
                TypeLabel1.Visibility = Visibility.Visible;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Hidden;
                ColorLabel2.Visibility = Visibility.Visible;
                StoreLabel3.Visibility = Visibility.Hidden;
                CountLabel3.Visibility = Visibility.Visible;
                QuantityLabel4.Visibility = Visibility.Visible;
                StoreLabel5.Visibility = Visibility.Visible;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Visible;
                FifthTB.Visibility = Visibility.Visible;
                FlossColorCB.Visibility = Visibility.Hidden;
            }

            else if (itemType == "Other Item")
            {
                // Labels
                NameLabel1.Visibility = Visibility.Hidden;
                ItemLabel1.Visibility = Visibility.Visible;
                TypeLabel1.Visibility = Visibility.Hidden;
                ColorLabel1.Visibility = Visibility.Hidden;
                StoreLabel2.Visibility = Visibility.Hidden;
                QuantityLabel2.Visibility = Visibility.Visible;
                ColorLabel2.Visibility = Visibility.Hidden;
                StoreLabel3.Visibility = Visibility.Visible;
                CountLabel3.Visibility = Visibility.Hidden;
                QuantityLabel4.Visibility = Visibility.Hidden;
                StoreLabel5.Visibility = Visibility.Hidden;

                // Fields
                FirstTB.Visibility = Visibility.Visible;
                SecondTB.Visibility = Visibility.Visible;
                ThirdTB.Visibility = Visibility.Visible;
                FourthTB.Visibility = Visibility.Hidden;
                FifthTB.Visibility = Visibility.Hidden;
                FlossColorCB.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Limitations
        private void ItemsDG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void SecondTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Other Item" || itemType == "Floss")
            {
                e.Handled = IsTextNumeric(e.Text);
            }
        }

        private bool IsTextNumeric(string text)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(text);
        }

        private void ThirdTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Fabric")
            {
                e.Handled = IsTextNumeric(e.Text);
            }
        }

        private void FourthTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBoxItem cbItem = ItemTypeCB.SelectedItem as ComboBoxItem;
            string itemType = cbItem.Content.ToString();

            if (itemType == "Fabric")
            {
                e.Handled = IsTextNumeric(e.Text);
            }
        }

        #endregion

        #region Classes
        private class ShoppingListItem
        {
            public string Item { get; set; }
            public string Name { get; set; }
            public string Quantity { get; set; }
            public string Store { get; set; }
        }
        #endregion

    }
}
