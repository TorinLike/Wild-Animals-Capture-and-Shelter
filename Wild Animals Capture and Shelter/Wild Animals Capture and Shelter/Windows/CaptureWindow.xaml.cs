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
using Wild_Animals_Capture_and_Shelter.Model;

namespace Wild_Animals_Capture_and_Shelter.Windows
{
    /// <summary>
    /// Логика взаимодействия для CaptureWindow.xaml
    /// </summary>
    public partial class CaptureWindow : Window
    {
        public CaptureWindow()
        {
            InitializeComponent();

            SpecBox.ItemsSource = DBInteractions.GetSpecies();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Введённые данные не сохранятся.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AnimalList win2 = new AnimalList(PassClass.currentUser);
                win2.Show();
                this.Close();
            }
            
        }

      

        private void AdresBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AdresBox.Text == "Введите адрес")
            {
                AdresBox.Text = "";
                AdresBox.Foreground = Brushes.Black;
            }
        }

        private void AdresBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AdresBox.Text == "")
            {
                AdresBox.Text = "Введите адрес";
                AdresBox.Foreground = Brushes.LightGray;
            }
        }

        private void phoneBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text == "Введите телефон")
            {
                phoneBox.Text = "";
                phoneBox.Foreground = Brushes.Black;
            }
        }

        private void phoneBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text == "")
            {
                phoneBox.Text = "Введите телефон";
                phoneBox.Foreground = Brushes.LightGray;
            }
        }

        private void MailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MailBox.Text == "Введите email")
            {
                MailBox.Text = "";
                MailBox.Foreground = Brushes.Black;
            }
        }

        private void MailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MailBox.Text == "")
            {
                MailBox.Text = "Введите email";
                MailBox.Foreground = Brushes.LightGray;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ErrorMessage = "";

            if (AdresBox.Text.Equals("Введите адрес"))
                ErrorMessage += "Не введён адрес\n";

            if (phoneBox.Text.Equals("Введите телефон"))
                ErrorMessage += "Не введён телефон \n";

            if (MailBox.Text.Equals("Введите email"))
                ErrorMessage += "Не введён email \n";

            if (SpecBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбрана порода живтоного \n";

            if (ErrorMessage.Length != 0)
            {
                MessageBox.Show("Ошибка: \n" + ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddCapture();
        }

        private void AddCapture()
        {
            DBInteractions.SaveCapture(FillCapture(new CaptureHistory()));
            MessageBox.Show("Заявка успешно добалена");
        }

        private CaptureHistory FillCapture(CaptureHistory chis)
        {
            chis.Address = AdresBox.Text;
            chis.Phone = phoneBox.Text;
            chis.Email = MailBox.Text;
            chis.SpeciesID = SpecBox.SelectedIndex + 1;


            return chis;
        }
    }
}
