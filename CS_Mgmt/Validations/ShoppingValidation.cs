using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CS_Mgmt.Validations
{
    internal class ShoppingValidation
    {
        public static bool PatternValidation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.");
                return false;
            }
            return true;
        }
        public static bool FlossValidation(ComboBoxItem color)
        {
            if (color == null)
            {
                MessageBox.Show("Select a color.");
                return false;
            }
            return true;
        }
        public static bool FabricValidation(string type, string color)
        {
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please add a type of fabric.");
                return false;
            }
            if (string.IsNullOrEmpty(color))
            {
                MessageBox.Show("Please add a fabric color.");
                return false;
            }
            return true;
        }
        public static bool ItemValidation(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                MessageBox.Show("Please add an item.");
                return false;
            }
            return true;
        }
    }
}
