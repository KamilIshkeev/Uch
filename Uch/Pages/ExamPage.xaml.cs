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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BindingSource = System.Windows.Forms.BindingSource;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;
using System.Data.Entity;
using System.Windows.Markup;






namespace Uch.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExamPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        private List<Exam> originalList;
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;

        public ExamPage(MainWindow mainWindow, Employee employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();
            _employee = employee;

            originalList = connect.db.Exam.ToList();


            ListEx.ItemsSource = originalList;
            txt_Nam.Text = employee.Last_Name;

            
            if (employee.Position == "преподаватель")
            {
                txt_code.Visibility = Visibility.Hidden;
                txt_code_e.Visibility = Visibility.Hidden;
                txt_audit.Visibility = Visibility.Hidden;
                ButtonAdd.Visibility = Visibility.Hidden;
                ButtonDelete.Visibility = Visibility.Hidden;    
            }


        }





        private void Txt_sort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }


        private void ApplyFilter()
        {

            string filterText = txt_sort.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filterText))
            {

                ListEx.ItemsSource = originalList;
            }
            else
            {

                var filteredList = originalList
                    .Where(emp =>
                        emp != null &&
                        emp.Registration_Number.ToString().StartsWith(filterText)
                    )
                    .OrderBy(emp => emp.Registration_Number)
                    .ToList();


                ListEx.ItemsSource = filteredList;
            }
        }








        private void Button_Add(object sender, RoutedEventArgs e)
        {


            try
            {
                var data = Convert.ToDateTime(txt_data.Text);
                var Codes = Convert.ToInt32(txt_code.Text);
                var regn = Convert.ToInt32(txt_reg.Text);
                var empid = Convert.ToInt32(txt_code_e.Text);
                var auditor = txt_audit.Text;
                var grad = Convert.ToInt32(txt_grade);



                var Exams = new Exam 
                { 
                    Date = data,
                    Code = Codes,
                    Registration_Number = regn,
                    Employee_ID = empid,
                    Auditorium = auditor,
                    Grade = grad
                };
                


               

                    connect.db.Exam.Add(Exams);
                    connect.db.SaveChanges();
                

                ListEx.ItemsSource = connect.db.Exam.ToList();
                MessageBox.Show("Строка была успешно добавлена");
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
                var data = Convert.ToDateTime(txt_data.Text);
                var Codes = Convert.ToInt32(txt_code.Text);
                var regn = Convert.ToInt32(txt_reg.Text);
                var empid = Convert.ToInt32(txt_code_e.Text);
                var auditor = txt_audit.Text;
                var grad = Convert.ToInt32(txt_grade);



                var Exams = new Exam
                {
                    Date = data,
                    Code = Codes,
                    Registration_Number = regn,
                    Employee_ID = empid,
                    Auditorium = auditor,
                    Grade = grad
                };





                connect.db.Exam.Add(Exams);
                connect.db.SaveChanges();


                ListEx.ItemsSource = connect.db.Exam.ToList();
                MessageBox.Show("Строка была успешно добавлена");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }







        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {

            var cood = Convert.ToDateTime(txt_data.Text);
            var regn = Convert.ToInt32(txt_reg.Text);
            if (cood != null)
            {
                using (var transaction = connect.db.Database.BeginTransaction())
                {
                    try
                    {

                        var Exams = connect.db.Exam.FirstOrDefault(r => r.Date == cood && r.Registration_Number == regn);


                        if (Exams != null)
                        {

                            
                            connect.db.Exam.Remove(Exams);

                            connect.db.SaveChanges();
                            transaction.Commit();

                            ListEx.ItemsSource = connect.db.Exam.ToList();
                            MessageBox.Show("Информация о строке удалена");
                        }
                        else
                        {
                            MessageBox.Show("Сотрудник с таким ID не найден.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Для удаления впишите ID");
            }





        }





        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {

            txt_sort.Clear();

            _bindingSource.DataSource = new BindingList<Exam>(originalList);
        }

        private void btn_red_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ListEx.SelectedItem != null)
                {
                    Exam exam = ListEx.SelectedItem as Exam;
                    exam.Grade = Convert.ToInt32(txt_grade.Text);
                    connect.db.SaveChanges();
                    ListEx.ItemsSource = connect.db.Exam.ToList();
                }
            }
            catch (Exception ex)
            {
                // Обработка непредвиденных ошибок
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}");

                // Опционально: логирование ошибки
                
            }
        }
    }
}
