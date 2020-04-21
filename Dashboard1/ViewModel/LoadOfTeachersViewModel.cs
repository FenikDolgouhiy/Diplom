using Dashboard1.Model;
using Dashboard1.Utils;
using System.Collections.Generic;
using System.Windows;

namespace Dashboard1.ViewModel
{
    public class LoadOfTeachersViewModel : ViewModelBase, INavigationAware
    {
        public Command ImportFromExcelCommand { get; }
        public Command ImportFromDGToFBCommand { get; }
        public DatabaseOperations dbOperations = new DatabaseOperations();
        public FBOperations fbOperations = new FBOperations();

        List<LoadDTO> _loadList = new List<LoadDTO>();
        public List<LoadDTO> LoadList
        {
            get { return _loadList; }
            private set 
            {
                _loadList = value;
                OnPropertyChanged("LoadList");
            }
        }
        internal LoadOfTeachersViewModel()
        {
            ImportFromExcelCommand = new Command(ImportFromExcel);
            ImportFromDGToFBCommand = new Command(ImportFromDGToFB);
        }
        private async void ImportFromExcel(object obj)
        {
            var loads = await dbOperations.ImportTeacherLoadsFromExcel();
            if (loads != null)
            {
                LoadList = loads;
            }
        }
        
        private async void ImportFromDGToFB(object gridExcel)
        {
            
            await fbOperations.ExportDGToFB(_loadList);
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
