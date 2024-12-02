using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
using Uch.Pages;
using static System.Net.Mime.MediaTypeNames;


namespace Uch
{
    
    public partial class MainWindow : Window
    {
      
        MainWindow _mainWindow;
        internal async void BtnMenu() 
        { 
            
            tx_menu.Visibility = Visibility.Visible; 
        }
        
        public MainWindow()
        {
            InitializeComponent();
            
            WindowStyle = WindowStyle.None;
            MainFrame.NavigationService.Navigate(new RegPage(this));
          
            
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (imageQr.Visibility == Visibility.Hidden)
            {
                imageQr.Visibility = Visibility.Visible;
                btnQr.Content = "Назад";
                // Ссылка на XL таблицу
                string soucer_xl = "https://www.gifcen.com/wp-content/uploads/2024/02/rickroll-gif-6.gif";
                // Создание переменной библиотеки QRCoder
                QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
                // Присваеваем значиения
                QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
                // переводим в Qr
                QRCoder.QRCode code = new QRCoder.QRCode(data);
                Bitmap bitmap = code.GetGraphic(100);
                /// Создание картинки
                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                    imageQr.Source = bitmapimage;
                }
            }
            else 
            {
                btnQr.Content = "QrCode"; 
                imageQr.Visibility = Visibility.Hidden; 
            }
        }


       


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is MenuWPage)
            {
                Debug.WriteLine("Already on MenuWPage, navigation skipped");
                return;
            }

            
            if (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.GoBack();
                Debug.WriteLine("Navigated back successfully");
            }
            else
            {
                Debug.WriteLine("Cannot navigate back");
            }


        }

       
    }
}
