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

        public MainViewModel() 
        {
            OpenListsCommand = new Command(OpenLists);
            OpenLoadOfTeachersCommand = new Command(OpenLoadOfTeachers);
        }

        private void OpenLists(object obj)
        {
            base.NavManager.Navigate(NavigationKeys.Lists);
        }
        private void OpenLoadOfTeachers(object obj)
        {
            base.NavManager.Navigate(NavigationKeys.LoadOfTeachers);
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
