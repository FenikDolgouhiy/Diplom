using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dashboard1.Utils;

namespace Dashboard1.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public NavigationManager NavManager { get; set; }

        public ViewModelBase()
        {

        }

        public ViewModelBase(NavigationManager navManager)
        {
            NavManager = navManager;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
