using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data.Entity.Migrations;
using System.Windows.Forms; // Для BindingSource
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BindingSource = System.Windows.Forms.BindingSource;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;


namespace Uch.Pages
{
    /// <summary>
    /// Логика взаимодействия для FacultyPage.xaml
    /// </summary>
    public partial class FacultyPage : Page
    {  
        private List<Discip> originalList; // Сохраняем оригинальный список
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;
        public FacultyPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();

            // Загрузка полного списка сотрудников
            originalList = connect.db.Discip.ToList();

            // Установка источника данных для ListView
            ListFaculty.ItemsSource = originalList;

            // Добавление обработчика события изменения текста
            txt_sort.TextChanged += Txt_sort_TextChanged;
        }






      

        // Обработчик изменения текста фильтра
        private void Txt_sort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }



          int fil = 0;




        // Метод для применения фильтрации
        private void ApplyFilter()
        {
            
            // Получение текста фильтра
            string filterText = txt_sort.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filterText))
            {

                ListFaculty.ItemsSource = originalList;
            }
            else
            {
                if (fil == 1)
                {
                    // Фильтрация списка
                    var filteredList = originalList
                     .Where(emp =>
                         emp != null &&
                         !string.IsNullOrEmpty(emp.Дисциплина) &&
                         emp.Дисциплина.ToLower().StartsWith(filterText)
                     )
                     .OrderBy(emp => emp.Дисциплина)
                     .ToList();
                    ListFaculty.ItemsSource = filteredList;
                }
                else if (fil == 2)
                {
                    // Фильтрация списка
                    var filteredList = originalList
                     .Where(emp =>
                         emp != null &&
                         !string.IsNullOrEmpty(emp.Факультет) &&
                         emp.Факультет.ToLower().StartsWith(filterText)
                     )
                     .OrderBy(emp => emp.Факультет)
                     .ToList();
                    ListFaculty.ItemsSource = filteredList;
                }
                else if(fil == 0 )
                {
                    // Фильтрация списка
                    var filteredList = originalList
                     .Where(emp =>
                         emp != null &&
                         !string.IsNullOrEmpty(emp.Факультет) &&
                         emp.Факультет.ToLower().StartsWith(filterText) 
                     )
                     .OrderBy(emp => emp.Факультет)
                     .ThenBy(emp => emp.Дисциплина)
                     .ToList();
                    ListFaculty.ItemsSource = filteredList;
                }
                
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
                    ListFaculty.ItemsSource = connect.db.Discipline.ToList();
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
            fil =0;
            // Очистка текстового поля
            txt_sort.Clear();

            // Восстановление полного списка
            _bindingSource.DataSource = new BindingList<Discip>(originalList);
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedValue == f1) { fil = 1; }
            else if (filter.SelectedValue == f2) { fil = 2; }


        }
    }
}

