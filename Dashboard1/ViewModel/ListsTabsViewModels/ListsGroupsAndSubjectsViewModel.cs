using Dashboard1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.ViewModel.ListsTabsViewModels
{
    public class ListsGroupsAndSubjectsViewModel: ViewModelBase, INavigationAware
    {
        private LoadListsTabs loadListsTabs = new LoadListsTabs();
        public List<string> _groupList = new List<string>();
        public List<string> _subjectList = new List<string>();
        public List<string> SubjectList
        {
            get { return _subjectList; }
            set
            {
                _subjectList = value;
                OnPropertyChanged("SubjectList");
            }
        }
        public List<string> GroupList
        {
            get { return _groupList; }
            private set
            {
                _groupList = value;
                OnPropertyChanged("GroupList");
            }
        }
        internal ListsGroupsAndSubjectsViewModel()
        {
            ExportGroupToDG();
            ExportSubjectToDG();
        }
        private async void ExportGroupToDG()
        {
            var loads = await loadListsTabs.ReturnGroups();
            if (loads != null)
            {
                GroupList = loads;
            }
        }
        private async void ExportSubjectToDG()
        {
            var loads = await loadListsTabs.ReturnSubjects();
            if (loads != null)
            {
                SubjectList = loads;
            }

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
