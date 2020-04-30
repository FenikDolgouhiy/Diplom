using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard1.ViewModel;
using Dashboard1.Utils;

namespace Dashboard1.ViewModel
{
    public class ListsViewModel : ViewModelBase, INavigationAware
    {
        
        public Command OpenListsTeachersCommand { get; }
        public Command OpenListsGroupsAndSubjectsCommand { get; }
        public Command OpenListsCabinetsCommand { get; }

        internal ListsViewModel()
        {
            OpenListsTeachersCommand = new Command(OpenListsTeachers);
            OpenListsCabinetsCommand = new Command(OpenListsCabinets);
            OpenListsGroupsAndSubjectsCommand = new Command(OpenListsGroupsAndSubjects);
        }
        private void OpenListsCabinets(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsCabinets);
        }
        private void OpenListsGroupsAndSubjects(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsGroupsAndSubjects);
        }
        private void OpenListsTeachers(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsTeachers);
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
