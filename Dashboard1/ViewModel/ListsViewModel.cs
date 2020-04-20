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
        internal ListsViewModel()
        {
            OpenListsSpecialtyCommand = new Command(OpenListsSpecialty);
        }
        private void OpenListsSpecialty(object obj)
        {
            NavigationUtils.GetNavigationManager(NavigationUtils.NavigationPanel.LISTS)
                .Navigate(NavigationKeys.ListsSpecialty);
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
