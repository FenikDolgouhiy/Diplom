using Dashboard1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dashboard1.ViewModel
{
    public class MainViewModel:  ViewModelBase, INavigationAware
    {
        public Command OpenListsCommand { get; }
        public Command OpenLoadOfTeachersCommand { get; }
        public Command OpenTimetableCommand { get; }
        public Command OpenManualCommand { get; }

        public MainViewModel() 
        {
            OpenListsCommand = new Command(OpenLists);
            OpenLoadOfTeachersCommand = new Command(OpenLoadOfTeachers);
            OpenTimetableCommand = new Command(OpenTimetable);
            OpenManualCommand = new Command(OpenManual);
        }
        private void OpenManual(Object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.START_WINDOW).Navigate(NavigationKeys.Manual);
        }
        private void OpenTimetable(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.START_WINDOW).Navigate(NavigationKeys.Timetable);
        }
        private void OpenLists(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.START_WINDOW).Navigate(NavigationKeys.Lists);
        }
        private void OpenLoadOfTeachers(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.START_WINDOW).Navigate(NavigationKeys.LoadOfTeachers);
        }


        #region Implementation of INavigationAware

        public void OnNavigatingTo(object arg)
        {
        }

        public void OnNavigatingFrom()
        {
        }

        #endregion
    }
}
