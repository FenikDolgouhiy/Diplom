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

        internal MainViewModel(NavigationManager navManager) : base(navManager)
        {
            OpenListsCommand = new Command(OpenLists);
        }
        private void OpenLists(object obj)
        {
            base.NavManager.Navigate(NavigationKeys.Lists);
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
