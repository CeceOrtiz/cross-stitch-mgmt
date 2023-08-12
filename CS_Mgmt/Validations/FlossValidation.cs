using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CS_Mgmt.Validations
{
    internal class FlossValidation
    {
        public static bool ValidNewFloss(ComboBoxItem colorCB, ComboBoxItem qtyCB)
        {
            if (colorCB == null)
            {
                MessageBox.Show("Select a color.");
                return false;
            }
            
            if (qtyCB == null)
            {
                MessageBox.Show("Select a quantity.");
                return false;
            }

            return true;
        }

        public static bool ValidQty(ComboBoxItem qtyCB)
        {
            if (qtyCB == null)
            {
                MessageBox.Show("Select a quantity.");
                return false;
            }

            return true;
        }

        public static bool ValidSelectedFloss(ComboBoxItem floss)
        {
            if (floss == null)
            {
                MessageBox.Show("Select a floss.");
                return false;
            }

            return true;
        }
    }
}
