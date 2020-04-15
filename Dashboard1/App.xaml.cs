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
        public static NavigationManager NavigationManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new StartWindow();

            NavigationManager = new NavigationManager(Dispatcher, window.FrameContent);

            NavigationManager.Register<ListsViewModel, Lists>
                (new ListsViewModel(NavigationManager), NavigationKeys.Lists);

            NavigationManager.Register<LoadOfTeachersViewModel, LoadOfTeachers>
                (new LoadOfTeachersViewModel(NavigationManager), NavigationKeys.LoadOfTeachers);

            NavigationManager.Register<ListsSpecialtyViewModel, ListsSpecialty>
                (new ListsSpecialtyViewModel(NavigationManager), NavigationKeys.ListsSpecialty);

            NavigationManager.Navigate(NavigationKeys.Lists);
            (window.DataContext as MainViewModel).NavManager = NavigationManager;
            window.Show();
        }
    }
}
