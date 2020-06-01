using Dashboard1.Model;
using Dashboard1.Utils;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dashboard1.ViewModel.ListsTabsViewModels
{
    class ListsCabinetsViewModel : ViewModelBase, INavigationAware
    {
        #region Implementation of INavigationAware
        public void OnNavigatingFrom()
        {
        }

        public void OnNavigatingTo(object arg)
        {

        }

        #endregion
        List<Cabinets> _cabinetsList = new List<Cabinets>();
        private LoadListsTabs loadListsTabs = new LoadListsTabs();
        public Command LoadToDBCommand { get; }
        public List<Cabinets> CabinetsList
        {
            get { return _cabinetsList; }
            private set
            {
                _cabinetsList = value;
                OnPropertyChanged("TeachersList");
            }
        }
        public ListsCabinetsViewModel()
        {
            GetInfo();
            LoadToDBCommand = new Command(LoadToDB);
        }
        private async void GetInfo()
        {
            var loads = await loadListsTabs.GetTeacherAndCabs();
            if (loads != null)
            {
                CabinetsList = loads;
            }
        }
        private async void LoadToDB(object obj)
        {
            await loadListsTabs.LoadCabinetsToDB(_cabinetsList);
        }
    }
}
