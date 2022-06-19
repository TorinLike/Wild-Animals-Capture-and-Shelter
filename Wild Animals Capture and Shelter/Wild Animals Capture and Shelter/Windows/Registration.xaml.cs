using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Wild_Animals_Capture_and_Shelter.Model;

namespace Wild_Animals_Capture_and_Shelter.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private BitmapImage ChangedPhoto = null;

        public Registration()
        {
            InitializeComponent();
            GenderBox.ItemsSource = DBInteractions.GetGender();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Введённые данные не сохранятся.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MainWindow win2 = new MainWindow();
                win2.Show();
                this.Close();
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ErrorMessage = "";

            if (nameBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введено имя работника \n";

            if (surnameBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введена фамилия работника \n";

            if (mailBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введен Email работника \n";

            if (phoneBox.Text.Equals(string.Empty) || phoneBox.Text.Length > 11)
                ErrorMessage += "Неверно введен телефон сотрудника \n";

            if (GenderBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбран пол сотрудника \n";

            if (loginBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введен логин! \n";

            if (passwordBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введен пароль! \n";

            if (passwordTwoBox.Text.Equals(string.Empty))
                ErrorMessage += "Повторно не введен пароль! \n";

            if (ErrorMessage.Length != 0)
            {
                MessageBox.Show("Ошибка: \n" + ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddProfile();


        }

        private void AddProfile()
        {
            DBInteractions.SaveProfile(FillProfile(new Profile()));
            MessageBox.Show("Пользователь успешно добален");
        }

        private Profile FillProfile(Profile profile)
        {
            List<Gender> genders = DBInteractions.GetGender();
            //List<Role> genders = _entity.Gender.ToList();
            profile.RoleID = 2;
            profile.Login = loginBox.Text;
            profile.Password = passwordBox.Text;
            profile.Name = nameBox.Text;
            profile.Surname = surnameBox.Text;
            profile.Phone = phoneBox.Text;
            profile.Email = mailBox.Text;
            profile.GenderID = GenderBox.SelectedIndex + 1;


            if (ChangedPhoto != null) {
                byte[] data;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ChangedPhoto));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                profile.Photo = data;
            }
            else
            {
                profile.Photo = null;
            }
            
            return profile;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                ChangedPhoto = new BitmapImage(new Uri(path, UriKind.Absolute));

                iPhoto.Source = ChangedPhoto;
            }
        }
    }
}
