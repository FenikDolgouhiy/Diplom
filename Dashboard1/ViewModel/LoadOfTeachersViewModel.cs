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
        public Command DeleteTeachLoadIDCommand { get; }
        public Command DeleteAllCommand { get; }
        public LoadDTO SelectedLOF { get; set; }

        public DatabaseOperations dbOperations = new DatabaseOperations();
        public FBOperations fbOperations = new FBOperations();

        List<LoadDTO> _loadList = new List<LoadDTO>();
        internal LoadOfTeachersViewModel()
        {
            ImportFromExcelCommand = new Command(ImportFromExcel);
            ImportFromDGToFBCommand = new Command(ImportFromDGToFB);
            DeleteTeachLoadIDCommand = new Command(DeleteTeachLoadID);
            DeleteAllCommand = new Command(DeleteAll);
            
            UploadFromFB();
        }
        public List<LoadDTO> LoadList
        {
            get { return _loadList; }
            private set 
            {
                _loadList = value;
                OnPropertyChanged("LoadList");
            }
        }
        private async void ImportFromExcel(object obj) // загрузка из Excel в DataGrid
        {
            var loads = await dbOperations.ImportTeacherLoadsFromExcel();
            if (loads != null)
            {
                LoadList = loads;
            }
        }
        private async void UploadFromFB() // Загрузка из БД в таблицу
        {
            var loads = await fbOperations.ExportFromFBToDG();
            if (loads != null)
            {
                LoadList = loads;
            }
        }
        private async void ImportFromDGToFB(object gridExcel) // Загрузка из таблицы в БД
        {
            await fbOperations.ExportDGToFB(_loadList);
        }
        private void DeleteTeachLoadID(object obj) // Удалить элемент из DataGrid
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную строку?", "Подтверждение операции", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (SelectedLOF != null)
                {
                    var loads = LoadList;
                    loads.Remove(SelectedLOF);
                    LoadList = null;
                    LoadList = loads;
                }
            }
        }
        private void DeleteAll(object obj) // Очистить Firebase
        {
            if (MessageBox.Show("Вы действительно хотите удалить ВСЁ из Базы данных?", "Подтверждение операции", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                fbOperations.DeleteAllInfoFromFB();

                MessageBox.Show("База данных была полностью очищенна");
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
