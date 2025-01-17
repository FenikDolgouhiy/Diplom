﻿using System.Collections.Generic;
using System.Windows;
using Dashboard1.Model;
using Dashboard1.Utils;
using Dashboard1.Logic;

namespace Dashboard1.ViewModel
{
    class TimetableViewModel : ViewModelBase, INavigationAware
    {
        private List<string> _timeTableGroup = new List<string>();
        public Command ShowTimeTCommand  { get; }
        public Command ExportToExcelCommand { get; }
        public Command DeleteTimeTableCommand { get; }
        public Command CreateTimetableCommand { get; }

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
            ShowTimeTCommand = new Command(LoadGroupTimetable);
            ExportToExcelCommand = new Command(ExportToExcel);
            CreateTimetableCommand = new Command(CreateTimetable);
            DeleteTimeTableCommand = new Command(DeleteTimeTable);

            LoadGroupTimetable(SelectedItem);
        }
        private void DeleteTimeTable(object obj)
        {
             timeTableOp.DeleteTimeTableFromFB();
        }

        private void CreateTimetable(object obj)
        {
            AlgoLogic.LogicRun();
            MessageBox.Show("Розклад створен, тепер ви маєте можливість переглянути його!");
        }

        private async void LoadGroupTimetable(object obj)// Загрузка расписания по выбранной группе
        {
            TimeTableGroupGrid = await timeTableOp.LoadGroupsToComboBox();
            if (SelectedItem != null)
            {
                TeachersList = null;
                TeachersList = await timeTableOp.LoadFullTimeTable(SelectedItem);
            }
        }
        private void ExportToExcel(object obj)
        {
            if (TeachersList != null)
            {
                timeTableOp.ListToExcel(TeachersList, SelectedItem);
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
