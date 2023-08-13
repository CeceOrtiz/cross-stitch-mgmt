using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CS_Mgmt.Validations
{
    internal class FabricValidation
    {
        public static bool ValidFabric(string type, string color, string count)
        {
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please enter a fabric type.");
                return false;
            }
            if (string.IsNullOrEmpty(color))
            {
                MessageBox.Show("Please enter a color.");
                return false;
            }
            if (count.All(char.IsDigit) == false)
            {
                MessageBox.Show("Please use numbers only for the count (blank defaults to 0)");
                return false;
            }

            return true;
        }
        public static bool ValidSelectedFabric(ComboBoxItem fabric)
        {
            if (fabric == null)
            {
                MessageBox.Show("Select a fabric.");
                return false;
            }

            return true;
        }
    }
}
