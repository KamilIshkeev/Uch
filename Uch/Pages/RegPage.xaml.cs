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
using System.Xml.Linq;
using Uch.Base;

namespace Uch.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public bool AllowGoBack { get; set; } = false; // Default to false
        static MainWindow _mainWindow;
        Employee newEmployee;
        int id;
        public RegPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            
        }



        

        
        private async void Button_Registration(object sender, RoutedEventArgs e)
        {
            int login_user = 0;

            if ( txtLogin.Text != "Введите логин" && txtLogin.Text != null && txtLogin.Text != "" && txtLogin.Text != " ") 
            {
                MessageBox.Show("Хм я что-то забыл а да Поздравляю вы проши игру!");
                login_user = Convert.ToInt32(txtLogin.Text);
                id = login_user;
                

            }
            else 
            {
                MessageBox.Show("Вы не вошли!!!");
                return;
            }

           

            var login_sql = await Task.Run(() => connect.db.Employee.FirstOrDefault(id => id.Employee_ID == login_user));

            try
            {
               
                    if (login_sql.Employee_ID == login_user)
                    {
                        // Пользователь существует, переходим на страницу профиля
                        var user = connect.db.Employee.FirstOrDefault(u => u.Employee_ID == login_user);
                        if (user != null)
                        {
                            
                            _mainWindow.MainFrame.NavigationService.Navigate(new MenuWPage(_mainWindow, user));
                            connect.db.Employee.FirstOrDefault(u => login_user==  u.Employee_ID);
                        newEmployee = user;
                        connect.db.SaveChanges();

                    }
                        else
                        {
                            MessageBox.Show("Ошибка: Пользователь не найден в таблице User.");
                        }
                    }
                   
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
