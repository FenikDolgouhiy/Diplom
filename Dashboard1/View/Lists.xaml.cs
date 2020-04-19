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
using Dashboard1.ViewModel;
using Dashboard1.Utils;
using Dashboard1.View.ListsTabs;

namespace Dashboard1.View
{
    /// <summary>
    /// Логика взаимодействия для Lists.xaml
    /// </summary>
    public partial class Lists : UserControl
    {
        public Lists()
        {
            InitializeComponent();
            var manager = new NavigationManager(Dispatcher, ContentTabs);
            manager.Register<ListsSpecialtyViewModel, ListsSpecialty>(new ListsSpecialtyViewModel(), NavigationKeys.ListsSpecialty);

            NavigationUtils.Register(NavigationUtils.NavigationPanel.LISTS, manager);
        }
    }
}
