using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_Mgmt.Models;

namespace CS_Mgmt.ViewModels
{
    internal class AddFlossVM : ViewModelBase
    {
        public ObservableCollection<Floss> NonUserFloss { get; } = new ObservableCollection<Floss>();
        public Floss SelectedFloss { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }
    }
}
