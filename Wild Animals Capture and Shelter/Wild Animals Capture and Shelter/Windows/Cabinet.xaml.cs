using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wild_Animals_Capture_and_Shelter.Model;

namespace Wild_Animals_Capture_and_Shelter.Windows
{
    /// <summary>
    /// Логика взаимодействия для Cabinet.xaml
    /// </summary>
    public partial class Cabinet : Window
    {
        private BitmapImage ChangedPhoto = null;
        private Profile profile1 { get; set; }
        public Cabinet(Profile profile)
        {
            InitializeComponent();

            GenderBox.ItemsSource = DBInteractions.GetGender();

            profile1 = profile;
            loginBox.Text = profile1.Login;
            passwordBox.Text = profile1.Password;
            nameBox.Text = profile1.Name;
            surnameBox.Text = profile1.Name;
            phoneBox.Text = profile1.Phone;
            mailBox.Text = profile1.Email;

            GenderBox.SelectedItem = profile1.Gender;

            if (profile1.Photo == null)
            {
                iPhoto.Source = new BitmapImage(new Uri("/Images/null_image.jpg", UriKind.Relative));
            }
            else
            {

                iPhoto.Source = profile.PhotoImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            AnimalList win2 = new AnimalList(PassClass.currentUser);
            win2.Show();
            this.Close();
            
        }

        private void nameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "Введите данные")
            {
                nameBox.Text = "";
                nameBox.Foreground = Brushes.Black;
            }
        }

        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "")
            {
                nameBox.Text = "Введите данные";
                nameBox.Foreground = Brushes.LightGray;
            }
        }

        private void surnameameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (surnameBox.Text == "Введите данные")
            {
                surnameBox.Text = "";
                surnameBox.Foreground = Brushes.Black;
            }
        }

        private void surnameameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (surnameBox.Text == "")
            {
                surnameBox.Text = "Введите данные";
                surnameBox.Foreground = Brushes.LightGray;
            }
        }

        private void phoneBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text == "Введите данные")
            {
                phoneBox.Text = "";
                phoneBox.Foreground = Brushes.Black;
            }
        }

        private void phoneBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text == "")
            {
                phoneBox.Text = "Введите данные";
                phoneBox.Foreground = Brushes.LightGray;
            }
        }

        private void mailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (mailBox.Text == "Введите данные")
            {
                mailBox.Text = "";
                mailBox.Foreground = Brushes.Black;
            }
        }

        private void mailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (mailBox.Text == "")
            {
                mailBox.Text = "Введите данные";
                mailBox.Foreground = Brushes.LightGray;
            }
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
            if (passwordBox.Text == "Введите данные")
            {
                passwordBox.Text = "";
                passwordBox.Foreground = Brushes.Black;
            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text == "")
            {
                passwordBox.Text = "Введите данные";
                passwordBox.Foreground = Brushes.LightGray;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (nameBox != null && 
                surnameBox != null && 
                phoneBox != null && 
                mailBox != null &&
                loginBox != null &&
                passwordBox != null)
            {
                profile1.Name = nameBox.Text;
                profile1.Surname = surnameBox.Text;
                profile1.Phone = phoneBox.Text;
                profile1.Email = mailBox.Text;
                profile1.Login = loginBox.Text;
                profile1.Password = passwordBox.Text;

                profile1.GenderID = GenderBox.SelectedIndex + 1;

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

                    profile1.Photo = data;
                }
                else if (profile1.Photo != null)
                {
                    profile1.Photo = profile1.Photo;
                }
                else
                {
                    profile1.Photo = null;
                }

            }
            else
            {
                MessageBox.Show("Поля не можгут быть пустыми");
            }
            DBInteractions.SaveEditedProfile(profile1);
            MessageBox.Show("Изменения сохранены");
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
