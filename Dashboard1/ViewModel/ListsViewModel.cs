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

        internal ListsViewModel(NavigationManager navManager) : base(navManager)
        {
           
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
