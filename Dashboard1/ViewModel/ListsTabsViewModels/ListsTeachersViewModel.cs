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
    public class ListsTeachersViewModel: ViewModelBase, INavigationAware
    {
        public List<TeachersWeekLoad> _teachersWeekLoad = new List<TeachersWeekLoad>();
        public Command LoadToDBCommand { get; }
        public Command DeleteAllFromDBCommand { get; }
        private LoadListsTabs loadListsTabs = new LoadListsTabs();
        private FBOperations fBOperations = new FBOperations();

        public List<TeachersWeekLoad> TeachersList
        {
            get { return _teachersWeekLoad; }
            private set
            {
                _teachersWeekLoad = value;
                OnPropertyChanged("TeachersList");
            }
        }
        internal ListsTeachersViewModel()
        {
            LoadTeachersList();
            LoadToDBCommand = new Command(LoadToDB);
            DeleteAllFromDBCommand = new Command(DeleteAllFromDB);
        }
        private async void LoadTeachersList() // Загрузка из базы
        {
            var loads = await loadListsTabs.ReturnTeachers();
            if (loads!=null)
            {
                TeachersList = loads;
            }
        }
        private void DeleteAllFromDB(object obj) //Удаление нагрузок с Грида
        {
            if (MessageBox.Show("Вы действительно хотите удалить нагрузки из таблицы полностью?\nВ БД сохранится.", "Подтверждение операции", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var loads = TeachersList;
                foreach (var item in loads)
                {
                    item.Monday = null;
                    item.Tuesday = null;
                    item.Wednesday = null;
                    item.Thursday = null;
                    item.Friday = null;
                }
                TeachersList = null;
                TeachersList = loads;
            }
        }
        private async void LoadToDB(object obj) // Загрузка нагрузки в базу
        {
            await fBOperations.LoadTeachersListToDb(_teachersWeekLoad);
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
