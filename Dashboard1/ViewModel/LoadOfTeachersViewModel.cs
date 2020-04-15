using Dashboard1.Utils;
using System.Windows;

namespace Dashboard1.ViewModel
{
    public class LoadOfTeachersViewModel : ViewModelBase, INavigationAware
    {
        public Command ImportFromExcelCommand { get; }
        public DatabaseOperations dbOperations = new DatabaseOperations();
        internal LoadOfTeachersViewModel(NavigationManager navManager) : base(navManager)
        {
            ImportFromExcelCommand = new Command(ImportFromExcel);
        }
        private void ImportFromExcel(object obj)
        {
            dbOperations.ImportExelToDG();
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
