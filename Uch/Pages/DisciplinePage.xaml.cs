using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    
    public partial class DisciplinePage : Page
    {
        static MainWindow _mainWindow;
        Employee _employee;
        public DisciplinePage(MainWindow mainWindow,Employee employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            ListDisip.ItemsSource = connect.db.Discipline.ToList();
            _employee = employee;
            
            txt_Nam.Text = _employee.Last_Name; 
        }


        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Redakt_Panel.Visibility = Visibility.Hidden;
            Rect.Visibility = Visibility.Hidden;
            ButtonAdd.Visibility = Visibility.Hidden;
            ButtonClose.Visibility = Visibility.Hidden;
            ButtonDelete.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                var Codes = Convert.ToInt32(txt_code.Text);
                string nazv = txt_nazvanie.Text;
                var voll = Convert.ToInt32(txt_vol.Text);
                string instruct = txt_instr.Text;
           


                var Disciplines = new Discipline()
                {
                    Code = Codes,
                    Name = nazv,
                    Volume = voll,
                    Instructor = instruct

                };

                connect.db.Discipline.Add(Disciplines);
                connect.db.SaveChanges();
                MessageBox.Show("Дисциплина была успешно добавлена");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }
        }
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var Codes = Convert.ToInt32(txt_code.Text);
                string nazv = txt_nazvanie.Text;
                var voll = Convert.ToInt32(txt_vol.Text);
                string instruct = txt_instr.Text;



                var Disciplines = new Discipline()
                {
                    Code = Codes,
                    Name = nazv,
                    Volume = voll,
                    Instructor = instruct
                };

                connect.db.Discipline.AddOrUpdate(Disciplines);
                connect.db.SaveChanges();
                MessageBox.Show("Дисциплина была успешно отредактированна");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var cood = Convert.ToInt32(txt_code.Text);
                if (cood != null)
                {
                    Discipline even = (from r in connect.db.Discipline where r.Code == cood select r).SingleOrDefault();
                    connect.db.Discipline.Remove(even);
                    connect.db.SaveChanges();
                    ListDisip.ItemsSource = connect.db.Discipline.ToList();
                    MessageBox.Show("Информация о дисциплине удалена");
                }
                else
                {
                    MessageBox.Show("Для удаления впишите ID Дисциплины");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }
        }



         


    }
}
