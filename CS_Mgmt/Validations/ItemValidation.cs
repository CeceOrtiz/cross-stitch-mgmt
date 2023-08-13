using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace CS_Mgmt.Validations
{
    internal class ItemValidation
    {
        public static bool ValidItem(string description, ComboBoxItem qty)
        {
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter the item's description.");
                return false;
            }
            if (qty == null)
            {
                MessageBox.Show("Please select the item's quantity.");
                return false;
            }
            return true;
        }
        public static bool ValidItemQty(ComboBoxItem qty)
        {
            if (qty == null)
            {
                MessageBox.Show("Please select the item's quantity.");
                return false;
            }
            return true;
        }
        public static bool ValidSelectedItem(ComboBoxItem item)
        {
            if (item == null)
            {
                MessageBox.Show("Select an item.");
                return false;
            }

            return true;
        }
    }
}
