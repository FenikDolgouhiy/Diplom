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
        internal ListsViewModel(NavigationManager navManager) : base(navManager)
        {
            OpenListsSpecialtyCommand = new Command(OpenListsSpecialty);
        }
        private void OpenListsSpecialty(object obj)
        {
            base.NavManager.Navigate(NavigationKeys.ListsSpecialty);
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
