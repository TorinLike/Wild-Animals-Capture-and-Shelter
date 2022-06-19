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
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        private BitmapImage ChangedPhoto = null;
        public AddWorkerWindow()
        {
            InitializeComponent();
            GenderBox.ItemsSource = DBInteractions.GetGender();
            RoleBox.ItemsSource = DBInteractions.GetRole();


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Введённые данные не сохранятся.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Workers win2 = new Workers();
                win2.Show();
                this.Close();
            }
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Имя")
            {
                NameBox.Text = "";
                NameBox.Foreground = Brushes.Black;
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = "Имя";
                NameBox.Foreground = Brushes.LightGray;
            }
        }

        private void SurnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text == "Фамилия")
            {
                SurnameBox.Text = "";
                SurnameBox.Foreground = Brushes.Black;
            }
        }

        private void SurnameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text == "")
            {
                SurnameBox.Text = "Фамилия";
                SurnameBox.Foreground = Brushes.LightGray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                ChangedPhoto = new BitmapImage(new Uri(path, UriKind.Absolute));

                iPhoto.Source = ChangedPhoto;
            }
        }

        private void BackButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            string ErrorMessage = "";

            if (NameBox.Text.Equals("Имя") || NameBox.Text == "")
                ErrorMessage += "Не введено имя \n";

            if (SurnameBox.Text.Equals("Фамилия") || SurnameBox.Text == "")
                ErrorMessage += "Не введена фамилия \n";

            if (AddressBox.Text.Equals(""))
                ErrorMessage += "Не введён адрес \n";

            if (PhoneBox.Text == "")
                ErrorMessage += "Не введён телефон \n";

            if (MailBox.Text == "")
                ErrorMessage += "Не введена электронная почта \n";

            if (GenderBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбран пол \n";

            if (RoleBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбрана роль \n";

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
            MessageBox.Show("Профиль успешно добален");
        }

        private Profile FillProfile(Profile profile)
        {
            List<Gender> genders = DBInteractions.GetGender();

            profile.Name = NameBox.Text;
            profile.Surname = SurnameBox.Text;
            profile.Phone = PhoneBox.Text;
            profile.Email = MailBox.Text;
            profile.Login = LoginBox.Text;
            profile.Password = PasswordBox.Text;

            profile.GenderID = GenderBox.SelectedIndex + 1;
            profile.RoleID = RoleBox.SelectedIndex + 1;


            if (ChangedPhoto != null)
            {
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
    }
    
}
