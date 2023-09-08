using CS_Mgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CS_Mgmt.Validations
{
    internal class PatternValidation
    {
        public static bool ValidPattern(string name, ComboBoxItem status)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.");
                return false;
            }

            if (status == null)
            {
                MessageBox.Show("Please choose a status.");
                return false;
            }

            return true;
        }

        public static bool ValidSelectedPattern(ComboBoxItem pattern)
        {
            if (pattern == null)
            {
                MessageBox.Show("Select a pattern.");
                return false;
            }

            return true;
        }
    }
}
