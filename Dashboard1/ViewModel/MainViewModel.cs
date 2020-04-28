using Dashboard1.Utils;


namespace Dashboard1.ViewModel
{
    public class MainViewModel : ViewModelBase, INavigationAware
    {
        public Command OpenListsCommand { get; }
        public Command OpenLoadOfTeachersCommand { get; }

        public MainViewModel()
        {
            OpenListsCommand = new Command(OpenLists);
            OpenLoadOfTeachersCommand = new Command(OpenLoadOfTeachers);

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
