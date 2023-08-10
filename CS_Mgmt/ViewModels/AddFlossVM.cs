using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_Mgmt.Models;
using SQLite;

namespace CS_Mgmt.ViewModels
{
    internal class AddFlossVM : ViewModelBase
    {
        private ObservableCollection<Floss> _nonUserFloss;
        private Floss _selectedFloss;
        private int _quantity;
        private string _storageLocation;

        public ObservableCollection<Floss> NonUserFloss
        {
            get { return _nonUserFloss; }
            set {
                _nonUserFloss = value;
                OnPropertyChanged(nameof(NonUserFloss));
            }
        }
        public Floss SelectedFloss
        {
            get { return _selectedFloss}
            set
            {
                _selectedFloss = value;
                OnPropertyChanged(nameof(SelectedFloss));
            }
        }
        public int Quantity
        {
            get { return _quantity}
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public string StorageLocation
        {
            get { return _storageLocation}
            set
            {
                _storageLocation = value;
                OnPropertyChanged(nameof(StorageLocation));
            }
        }

        public AddFlossVM() {
            NonUserFloss = GetNonUserFloss(App.DatabasePath);
            SelectedFloss = null;
            Quantity = 0;
            StorageLocation = string.Empty;
        }

        public static ObservableCollection<Floss> GetNonUserFloss(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Floss> allFloss = connection.Table<Floss>().ToList();
                List<int> userFlossIDs = connection.Table<UserFloss>().Select(u => u.FlossId).ToList();

                List<Floss> nonUserFlossList = allFloss.Where(floss => !userFlossIDs.Contains(floss.FlossId)).ToList();

                ObservableCollection<Floss> nonUserFloss = new ObservableCollection<Floss>(nonUserFlossList);

                return nonUserFloss;
            }
        }
    }
}
