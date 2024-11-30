using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms; // Для BindingSource
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Uch.Base;
using BindingSource = System.Windows.Forms.BindingSource;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;





namespace Uch.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmpPage.xaml
    /// </summary>
    public partial class EmpPage : Page
    {









        private List<Empols> originalList; // Сохраняем оригинальный список
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;
        //private ObservableCollection<string> employeeNames;

        public EmpPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();

            // Загрузка полного списка сотрудников
            originalList = connect.db.Empols.ToList();

            // Установка источника данных для ListView
            ListEmp.ItemsSource = originalList;

            // Добавление обработчика события изменения текста
            txt_sort.TextChanged += Txt_sort_TextChanged;
        }

        // Обработчик изменения текста фильтра
        private void Txt_sort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        // Метод для применения фильтрации
        private void ApplyFilter()
        {
            // Получение текста фильтра
            string filterText = txt_sort.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filterText))
            {
              
                ListEmp.ItemsSource = originalList;
            }
            else
            {
                // Фильтрация списка
                var filteredList = originalList
                    .Where(emp =>
                        emp != null &&
                        !string.IsNullOrEmpty(emp.Last_Name) &&
                        emp.Last_Name.ToLower().StartsWith(filterText)
                    )
                    .OrderBy(emp => emp.Last_Name)
                    .ToList();

               
                ListEmp.ItemsSource = filteredList;
            }
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
                    ListEmp.ItemsSource = connect.db.Discipline.ToList();
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


    

        // Обработчик сброса фильтра
        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            // Очистка текстового поля
            txt_sort.Clear();

            // Восстановление полного списка
            _bindingSource.DataSource = new BindingList<Empols>(originalList);
        }







    }
} 





// Метод для применения фильтрации
        //private void ApplyFilter()
        //{
        //    // Получение текста фильтра
        //    string filterText = txt_sort.Text.ToLower().Trim();

        //    if (string.IsNullOrWhiteSpace(filterText))
        //    {
        //        // Если фильтр пустой, восстанавливаем полный список
        //        _bindingSource.DataSource = new BindingList<Empols>(originalList);
        //    }
        //    else
        //    {
        //        // Расширенная фильтрация с несколькими вариантами поиска
        //        //var filteredList = originalList
        //        //    .Where(emp =>
        //        //        // Начало фамилии 
        //        //        (emp.Last_Name != null && emp.Last_Name.ToLower().StartsWith(filterText))
        //        //    )
        //        //    .OrderBy(emp => emp.Last_Name)
        //        //    .ThenBy(emp => emp.Last_Name)
        //        //    .ToList();
        //        var filteredList = (originalList ?? new List<Empols>())
        //            .Where(emp =>
        //                emp != null &&
        //                !string.IsNullOrEmpty(emp.Last_Name) &&
        //                emp.Last_Name.ToLower().StartsWith(filterText)
        //            )
        //            .OrderBy(emp => emp.Last_Name)
        //            .ThenBy(emp => emp.Last_Name)
        //            .ToList();




        //        // Обновление источника данных
        //        _bindingSource.DataSource = new BindingList<Empols>(filteredList);
        //    }
        //}