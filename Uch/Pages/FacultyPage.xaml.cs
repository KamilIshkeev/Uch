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
using System.Windows.Forms; 
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BindingSource = System.Windows.Forms.BindingSource;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;


namespace Uch.Pages
{
    
    public partial class FacultyPage : Page
    {  
        private List<Discip> originalList; 
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;
        Faculty _faculty;

        public FacultyPage(MainWindow mainWindow, Employee employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();
            _employee = employee;
           
            originalList = connect.db.Discip.ToList();

            
            ListFaculty.ItemsSource = originalList;
            txt_Nam.Text = employee.Last_Name;

            
            txt_sort.TextChanged += Txt_sort_TextChanged;
        }






      

        
        private void Txt_sort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }



          int fil = 0;




        
        private void ApplyFilter()
        {
            
         
            string filterText = txt_sort.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filterText))
            {

                ListFaculty.ItemsSource = originalList;
            }
            else
            {
                if (fil == 1)
                {
                    
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
               
                var Codes = txt_code.Text;
                string cafe = txt_nazvanie.Text;
                string facul = txt_vol.Text;
                var truc = connect.db.Faculty.FirstOrDefault(id => id.Name == facul);
                var struc = (truc as Faculty);
                string fc = struc.Abbreviation;

                var Departments = new Department()
                {
                    Code = Codes,
                    Name = cafe,
                    Faculty = fc,

                };

                connect.db.Department.Add(Departments);
                connect.db.SaveChanges();
                
                MessageBox.Show("Строка была успешно отредактированна");
                ListFaculty.ItemsSource = connect.db.Department.ToList();
                ListFaculty.ItemsSource = connect.db.Discip.ToList();
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
                var Codes = txt_code.Text;
                string cafe = txt_nazvanie.Text;
                string facul = txt_vol.Text;
                var truc = connect.db.Faculty.FirstOrDefault(id => id.Name == facul);
                var struc = (truc as Faculty);
                string fc = struc.Abbreviation;

                var Departments = new Department()
                {
                    Code = Codes,
                    Name = cafe,
                    Faculty = fc,

                };

                connect.db.Department.AddOrUpdate(Departments);
                connect.db.SaveChanges();
                MessageBox.Show("Строка была успешно отредактированна");
                ListFaculty.ItemsSource = connect.db.Department.ToList();
                ListFaculty.ItemsSource = connect.db.Discip.ToList();
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
                var cood = txt_code.Text;
                if (cood != null)
                {
                    Department even = (from r in connect.db.Department where r.Code == cood select r).SingleOrDefault();
                    connect.db.Department.Remove(even);
                    connect.db.SaveChanges();
                    ListFaculty.ItemsSource = connect.db.Department.ToList();
                    ListFaculty.ItemsSource = connect.db.Discip.ToList();
                    MessageBox.Show("Информация о строке удалена");
                }
                else
                {
                    MessageBox.Show("Для удаления впишите Code");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }
        }




     
        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            fil =0;
          
            txt_sort.Clear();

           
            _bindingSource.DataSource = new BindingList<Discip>(originalList);
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedValue == f1) { fil = 1; }
            else if (filter.SelectedValue == f2) { fil = 2; }


        }
    }
}

