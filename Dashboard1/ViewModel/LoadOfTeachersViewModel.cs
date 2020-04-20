using Dashboard1.Model;
using Dashboard1.Utils;
using System.Collections.Generic;
using System.Windows;

namespace Dashboard1.ViewModel
{
    public class LoadOfTeachersViewModel : ViewModelBase, INavigationAware
    {
        public Command ImportFromExcelCommand { get; }
        public DatabaseOperations dbOperations = new DatabaseOperations();

        List<LoadDTO> _loadList = new List<LoadDTO>();
        public List<LoadDTO> LoadList
        {
            get { return _loadList; }
            private set {
                _loadList = value;
                OnPropertyChanged("LoadList");
            }
        }

        internal LoadOfTeachersViewModel(NavigationManager navManager) : base(navManager)
        {
            ImportFromExcelCommand = new Command(ImportFromExcel);
        }
        private void ImportFromExcel(object obj)
        {
            var loads = dbOperations.ImportTeacherLoadsFromExcel();
            if (loads != null)
            {
                LoadList = loads;
            }
        }

        #region Implementation of INavigationAware
        public void OnNavigatingFrom()
        {
        }

        public void OnNavigatingTo(object arg)
        {

        }

        #endregion
    }
}
