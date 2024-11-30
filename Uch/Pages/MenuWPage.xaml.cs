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
using Uch.Base;

namespace Uch.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuWPage.xaml
    /// </summary>
    public partial class MenuWPage : Page
    {
        static MainWindow _mainWindow;
        
        public MenuWPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new DisciplinePage(_mainWindow));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new EmpPage(_mainWindow));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
