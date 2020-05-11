using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dashboard1.Model;
using Dashboard1.Utils;
using Dashboard1.View;
using Remotion.Data.Linq.Collections;

namespace Dashboard1.ViewModel
{
    class TimetableViewModel : ViewModelBase, INavigationAware
    {
        private List<string> _timeTableGroup = new List<string>();
        public Command ShowTimeTCommand  { get; }
        private List<FullTimeTable> _fullTimeTable = new List<FullTimeTable>();
        public string SelectedItem { get; set; }
        TimeTableOperations timeTableOp = new TimeTableOperations();
        public List<string> TimeTableGroupGrid
        {
            get { return _timeTableGroup; }
            set
            {
                _timeTableGroup = value;
                OnPropertyChanged("TimeTableGrid");
            }
        }
        public List<FullTimeTable> TeachersList
        {
            get { return _fullTimeTable; }
            set
            {
                _fullTimeTable = value;
                OnPropertyChanged("TeachersList");
            }
        }
        internal TimetableViewModel()
        {
            LoadGroups();
            ShowTimeTCommand = new Command(LoadGroupTimetable);
        }
        private async void LoadGroups()
        {
            TimeTableGroupGrid = await timeTableOp.LoadGroupsToComboBox();
        }
        private async void LoadGroupTimetable(object obj)// Загрузка расписания по выбранной группе
        {
            if (SelectedItem != null)
            {
                TeachersList = null;
                TeachersList = await timeTableOp.LoadFullTimeTable(SelectedItem);
            }
            else
                MessageBox.Show("Вы ничего не выбрали");
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
