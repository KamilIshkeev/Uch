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
using System.Windows.Forms; 
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Uch.Base;
using BindingSource = System.Windows.Forms.BindingSource;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;





namespace Uch.Pages
{

    public partial class EmpPage : Page
    {


        private List<Empols> originalList; 
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;
        

        public EmpPage(MainWindow mainWindow, Employee employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();

            
            originalList = connect.db.Empols.ToList();

           
            ListEmp.ItemsSource = originalList;
            txt_Nam.Text = employee.Last_Name;

            
            txt_sort.TextChanged += Txt_sort_TextChanged;
            txt_pos.TextChanged += Txt_up_TextChanged;

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
              
                ListEmp.ItemsSource = originalList;
            }
            else
            {
             
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






     

        private void Txt_up_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_pos.Text == "инженер")
            {
                txt_stage.Visibility = Visibility.Hidden;
                txt_rang.Visibility = Visibility.Hidden;
                txt_stepen.Visibility = Visibility.Hidden;
            }
            else
            {
                txt_stage.Visibility = Visibility.Visible;
                txt_rang.Visibility = Visibility.Visible;
                txt_stepen.Visibility = Visibility.Visible;
            }
        }
        private void Button_Add(object sender, RoutedEventArgs e)
        {
                
                
            try
            {
                var empid = Convert.ToInt32(txt_code.Text);
                var Codes = txt_code_caf.Text;
                string nazv = txt_name.Text;
                var pos = txt_pos.Text;
                var zp = Convert.ToInt32(txt_zp.Text);
                int stag = 0;
                string rank = "";
                string step = "";
                  


                    var Employees = new Employee()
                {
                    Employee_ID = empid,
                    Code = Codes,
                    Last_Name = nazv,
                    Position = pos,
                    Salary = zp,


                };
                
                    
                if (pos == "инженер")
                {
                    
                    connect.db.Employee.Add(Employees);
                    connect.db.SaveChanges();
                }
                else 
                {
                    if (pos != "инженер")
                    {
                        stag = Convert.ToInt32(txt_stage.Text);
                        rank = txt_rang.Text;
                        step = txt_stepen.Text;
                    }

                    var Lecturers = new Lecturer 
                {
                    Employee_ID = empid,
                    Title = rank,
                    Degree = step
                };
                var Head_of_Departments = new Head_of_Department 
                {
                    Employee_ID = empid,
                    Experience = stag
                };
                    connect.db.Employee.Add(Employees);
                    connect.db.SaveChanges();
                    connect.db.Lecturer.Add(Lecturers);
                    connect.db.SaveChanges();
                    connect.db.Head_of_Department.Add(Head_of_Departments);
                    connect.db.SaveChanges();
                }
                
                ListEmp.ItemsSource = connect.db.Employee.ToList();
                ListEmp.ItemsSource = connect.db.Lecturer.ToList();
                ListEmp.ItemsSource = connect.db.Head_of_Department.ToList();
                ListEmp.ItemsSource = connect.db.Empols.ToList();
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
                var empid = Convert.ToInt32(txt_code.Text);
                var Codes = txt_code_caf.Text;
                string nazv = txt_name.Text;
                var pos = txt_pos.Text;
                var zp = Convert.ToInt32(txt_zp.Text);
                var stag = Convert.ToInt32(txt_stage.Text);
                string rank = txt_rang.Text;
                string step = txt_stepen.Text;



                var Employees = new Employee()
                {
                    Employee_ID = empid,
                    Code = Codes,
                    Last_Name = nazv,
                    Position = pos,
                    Salary = zp,


                };
                var Lecturers = new Lecturer
                {
                    Employee_ID = empid,
                    Title = rank,
                    Degree = step
                };
                var Head_of_Departments = new Head_of_Department
                {
                    Employee_ID = empid,
                    Experience = stag
                };
                connect.db.Employee.AddOrUpdate(Employees);
                connect.db.SaveChanges();
                connect.db.Lecturer.AddOrUpdate(Lecturers);
                connect.db.SaveChanges();
                connect.db.Head_of_Department.AddOrUpdate(Head_of_Departments);
                connect.db.SaveChanges();
                ListEmp.ItemsSource = connect.db.Employee.ToList();
                ListEmp.ItemsSource = connect.db.Empols.ToList();
                MessageBox.Show("Строка была успешно отредактированна");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не ввели все данные!");
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
          
            var cood = Convert.ToInt32(txt_code.Text);
            if (cood != null)
            {
                using (var transaction = connect.db.Database.BeginTransaction()) 
                {
                    try
                    {
                       
                        var employee = connect.db.Employee.FirstOrDefault(r => r.Employee_ID == cood);


                        if (employee != null)
                        {
                         
                            var lecturer = connect.db.Lecturer.FirstOrDefault(r => r.Employee_ID == cood);
                            var headOfDepartment = connect.db.Head_of_Department.FirstOrDefault(r => r.Employee_ID == cood);

                            if (lecturer != null) connect.db.Lecturer.Remove(lecturer);
                            if (headOfDepartment != null) connect.db.Head_of_Department.Remove(headOfDepartment);
                            connect.db.Employee.Remove(employee);

                            connect.db.SaveChanges(); 
                            transaction.Commit();

                            ListEmp.ItemsSource = connect.db.Employee.ToList();
                            ListEmp.ItemsSource = connect.db.Lecturer.ToList();
                            ListEmp.ItemsSource = connect.db.Head_of_Department.ToList();
                            ListEmp.ItemsSource = connect.db.Empols.ToList();
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

            _bindingSource.DataSource = new BindingList<Empols>(originalList);
        }







    }
} 



