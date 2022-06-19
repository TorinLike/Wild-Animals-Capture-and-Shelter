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
using System.Windows.Shapes;

namespace Wild_Animals_Capture_and_Shelter.Windows
{
    /// <summary>
    /// Логика взаимодействия для Forgot.xaml
    /// </summary>
    public partial class Forgot : Window
    {
        public Forgot()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win2 = new MainWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Письмо с ссылкой для восстановления аккаунта было выслано на указанный адрес электронной почты", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
