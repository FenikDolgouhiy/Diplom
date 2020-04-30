using Dashboard1.Model;
using Dashboard1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.ViewModel.ListsTabsViewModels
{
    public class ListsTeachersViewModel: ViewModelBase, INavigationAware
    {
        public List<TeachersWeekLoad> _teachersWeekLoad = new List<TeachersWeekLoad>();
        public Command LoadToDBCommand { get; }
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
        }
        private async void LoadTeachersList()
        {
            var loads = await loadListsTabs.ReturnTeachers();
            if (loads!=null)
            {
                TeachersList = loads;
            }
        }
        private async void LoadToDB(object obj)
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
