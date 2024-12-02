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
    
    public partial class MenuWPage : Page
    {
        static MainWindow _mainWindow;
        Employee _employee;
        Department _department;
        
        public MenuWPage(MainWindow mainWindow, Employee employee)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            _employee = employee;
            txt_Nam.Text = employee.Last_Name;
            _mainWindow.BtnMenu();
            if (employee.Position == "зав. кафедрой")
            {
                btn_disp.Visibility = Visibility.Hidden;
                btn_emp.Visibility = Visibility.Hidden;
                btn_cafed.Visibility = Visibility.Visible;
                btn_fuclt.Visibility = Visibility.Visible;
                btn_exam.Visibility = Visibility.Visible;
            }
            else if (employee.Position == "инженер")
            {
                btn_disp.Visibility = Visibility.Visible;
                btn_emp.Visibility = Visibility.Visible;
                btn_cafed.Visibility = Visibility.Visible;
                btn_fuclt.Visibility = Visibility.Visible;
                btn_exam.Visibility = Visibility.Visible;
            }
            else if (employee.Position == "преподаватель") 
            {
                btn_disp.Visibility = Visibility.Visible;
                btn_emp.Visibility = Visibility.Hidden;
                btn_cafed.Visibility = Visibility.Hidden;
                btn_fuclt.Visibility = Visibility.Hidden;
                btn_exam.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new DisciplinePage(_mainWindow, _employee));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new EmpPage(_mainWindow, _employee));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new FacultyPage(_mainWindow, _employee));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new CafedraPage(_mainWindow, _employee));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new ExamPage(_mainWindow, _employee));
        }
    }
}
