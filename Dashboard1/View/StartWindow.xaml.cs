using Dashboard1.Utils;
using Dashboard1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard1
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = int.Parse(((Button)e.Source).Uid);

        //    GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

        //    switch (index)
        //    {
        //        case 0:
        //            GridMainSecond.Background = Brushes.Aquamarine;
        //            break;
        //        case 1:
        //            GridMainSecond.Background = Brushes.Beige;
        //            break;
        //        case 2:
        //            GridMainSecond.Background = Brushes.CadetBlue;
        //            break;
        //        case 3:
        //            GridMainSecond.Background = Brushes.DarkBlue;
        //            break;
        //        case 4:
        //            GridMainSecond.Background = Brushes.Firebrick;
        //            break;
        //        case 5:
        //            GridMainSecond.Background = Brushes.Gainsboro;
        //            break;
        //        case 6:
        //            GridMainSecond.Background = Brushes.HotPink;
        //            break;
        //    }
        //}

        private void Button_Click_MainMenu(object sender, RoutedEventArgs e)
        {

        }
    }
}
