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
        public Command OpenListsSpecialtyCommand { get; }
        public Command OpenListsTeachersCommand { get; }
        public Command OpenListsGroupsCommand { get; }
        public Command OpenListsCabinetsCommand { get; }
        public Command OpenListsSubjectsCommand { get; }
        internal ListsViewModel()
        {
            OpenListsSpecialtyCommand = new Command(OpenListsSpecialty);
            OpenListsTeachersCommand = new Command(OpenListsTeachers);
            OpenListsCabinetsCommand = new Command(OpenListsCabinets);
            OpenListsGroupsCommand = new Command(OpenListsGroups);
            OpenListsSubjectsCommand = new Command(OpenListsSubjects);
        }
        private void OpenListsCabinets(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsCabinets);
        }
        private void OpenListsGroups(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsGroups);
        }
        private void OpenListsSubjects(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsSubjects);
        }
        private void OpenListsSpecialty(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsSpecialty);
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
