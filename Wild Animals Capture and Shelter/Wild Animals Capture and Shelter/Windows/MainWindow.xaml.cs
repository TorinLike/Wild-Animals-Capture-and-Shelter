using System;
using System.Collections.Generic;
using System.Data;
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
using Wild_Animals_Capture_and_Shelter.Model;
using Wild_Animals_Capture_and_Shelter.Windows;

namespace Wild_Animals_Capture_and_Shelter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Registration win2 = new Registration();
            win2.Show();
            this.Close();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            List<Profile> profileList = DBInteractions.GetProfile();

            if (loginBox.Text != "Введите данные")
            {
                if (passwordBox.Password != "Введите данные")
                {
                    Profile profile = new Profile();
                    profile = profileList.Where(i => i.Login == loginBox.Text && i.Password == passwordBox.Password).FirstOrDefault();
                    if (profile != null)
                    {
                        PassClass.currentUser = profile;
                        AnimalList win2 = new AnimalList(profile);
                        win2.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Профиль не найден");
                    }
                }
                else MessageBox.Show("Введите пароль");
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }

        private void ForgorButton_Click(object sender, RoutedEventArgs e)
        {
            Forgot win2 = new Forgot();
            win2.Show();
            this.Close();
        }

        private void EnterButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            AnimalList win2 = new AnimalList();
            win2.Show();
            this.Close();
        }

        private void loginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "Введите данные")
            {
                loginBox.Text = "";
                loginBox.Foreground = Brushes.Black;
            }
        }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "")
            {
                loginBox.Text = "Введите данные";
                loginBox.Foreground = Brushes.LightGray;
            }
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "Введите данные")
            {
                passwordBox.Password = "";
                passwordBox.Foreground = Brushes.Black;
            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                passwordBox.Password = "Введите данные";
                passwordBox.Foreground = Brushes.LightGray;
            }
        }
    }
}
