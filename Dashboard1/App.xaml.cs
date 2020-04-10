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

            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            mainNavManager.Register<ListsViewModel, Lists>
                (new ListsViewModel(mainNavManager), NavigationKeys.Lists);

            mainNavManager.Register<LoadOfTeachersViewModel, LoadOfTeachers>
                (new LoadOfTeachersViewModel(mainNavManager), NavigationKeys.LoadOfTeachers);

            mainNavManager.Navigate(NavigationKeys.Lists);
            window.Show();
        }
    }
}
