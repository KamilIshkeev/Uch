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
    
    public partial class CafedraPage : Page
    {
        private List<Cafedra> originalList; 
        static MainWindow _mainWindow;
        Employee _employee;
        private BindingSource _bindingSource;
        Department _department;
        
        public CafedraPage(MainWindow mainWindow, Employee employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _bindingSource = new BindingSource();
            
            
            originalList = connect.db.Cafedra.ToList();

            
            ListCafedra.ItemsSource = originalList;

            txt_Nam.Text = employee.Last_Name;
        }
       






       

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            //try
            //{
                var Codes = Convert.ToInt32(txt_code.Text);
                string nazv = txt_nazvanie.Text;
                var voll = Convert.ToInt32(txt_vol.Text);
                string instruct = txt_instr.Text;
                var truc  = connect.db.Department.FirstOrDefault(id => id.Name == instruct);
            var struc = (truc as Department); 
            string inst = struc.Code;
                string fc = struc.Faculty;
               

                var Disciplines = new Discipline()
                {
                    Code = Codes,
                    Name = nazv,
                    Volume = voll,
                    Instructor = inst

                };
            if (inst != struc.Code) { 
                var Departments = new Department()
                {
                    Code = inst,
                    Name = instruct,
                    Faculty = fc,

                };
                connect.db.Department.Add(Departments);
                connect.db.SaveChanges();
            }

                connect.db.Discipline.Add(Disciplines);
                connect.db.SaveChanges();
                
                MessageBox.Show("Строка была успешно добавлена");
                ListCafedra.ItemsSource = connect.db.Discipline.ToList();
                ListCafedra.ItemsSource = connect.db.Cafedra.ToList();
                return;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Вы не ввели все данные!");
            //}
        }
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var Codes = Convert.ToInt32(txt_code.Text);
                string nazv = txt_nazvanie.Text;
                var voll = Convert.ToInt32(txt_vol.Text);
                string instruct = txt_instr.Text;
                var truc = connect.db.Department.FirstOrDefault(id => id.Name == instruct);
                var struc = (truc as Department);
                string inst = struc.Code;
                string fc = struc.Faculty;


                var Disciplines = new Discipline()
                {
                    Code = Codes,
                    Name = nazv,
                    Volume = voll,
                    Instructor = inst

                };
                var Departments = new Department()
                {
                    Code = inst,
                    Name = instruct,
                    Faculty = fc,

                };

                connect.db.Discipline.AddOrUpdate(Disciplines);
                connect.db.SaveChanges();
                connect.db.Department.AddOrUpdate(Departments);
                connect.db.SaveChanges();
                MessageBox.Show("Строка была успешно добавлена");
                ListCafedra.ItemsSource = connect.db.Discipline.ToList();
                ListCafedra.ItemsSource = connect.db.Cafedra.ToList();
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
                //var cod = txt_code.Text;
                var truc = connect.db.Discipline.FirstOrDefault(id => id.Code == cood);
                var struc = (truc as Discipline);
                string inst = struc.Instructor;
                if (cood != null)
                {
                    Discipline eve = (from r in connect.db.Discipline where r.Code == cood select r).SingleOrDefault();
                    connect.db.Discipline.Remove(eve);
                    connect.db.SaveChanges();
                    Department even = (from r in connect.db.Department where r.Code == inst select r).SingleOrDefault();
                    connect.db.Department.Remove(even);
                    connect.db.SaveChanges();
                    ListCafedra.ItemsSource = connect.db.Discipline.ToList();
                    ListCafedra.ItemsSource = connect.db.Cafedra.ToList();
                    MessageBox.Show("Информация о строке удалена");

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
