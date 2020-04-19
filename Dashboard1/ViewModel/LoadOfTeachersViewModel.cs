using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard1.ViewModel;
using Dashboard1.Utils;

namespace Dashboard1.ViewModel
{
    public class LoadOfTeachersViewModel : ViewModelBase, INavigationAware
    {

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
