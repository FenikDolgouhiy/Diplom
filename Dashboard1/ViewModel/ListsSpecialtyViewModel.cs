using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard1.Utils;
using Dashboard1.View;

namespace Dashboard1.ViewModel
{
    public class ListsSpecialtyViewModel: ViewModelBase, INavigationAware
    {
        internal ListsSpecialtyViewModel(NavigationManager navManager) : base(navManager)
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
