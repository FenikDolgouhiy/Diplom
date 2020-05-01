using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dashboard1.Utils;
using Dashboard1.View;
using Dashboard1.ViewModel;
using Dashboard1.View.ListsTabs;

namespace Dashboard1
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new StartWindow();
            var startWindowManager = new NavigationManager(Dispatcher, window.FrameContent);

            startWindowManager.Register<ListsViewModel, Lists>
                (new ListsViewModel(), NavigationKeys.Lists);
            startWindowManager.Register<LoadOfTeachersViewModel, LoadOfTeachers>
                (new LoadOfTeachersViewModel(), NavigationKeys.LoadOfTeachers);
            startWindowManager.Register<TimetableViewModel, Timetable>
                (new TimetableViewModel(), NavigationKeys.Timetable);
            startWindowManager.Register<ManualViewModel, Manual>
                (new ManualViewModel(), NavigationKeys.Manual);

            NavigationUtils.Register(NavigationUtils.NavigationPanel.START_WINDOW, startWindowManager);

            startWindowManager.Navigate(NavigationKeys.Manual);
            window.Show();
        }
    }
}
