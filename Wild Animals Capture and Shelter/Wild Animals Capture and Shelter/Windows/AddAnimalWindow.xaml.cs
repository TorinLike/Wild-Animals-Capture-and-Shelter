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
    /// Логика взаимодействия для AddAnimalWindow.xaml
    /// </summary>
    public partial class AddAnimalWindow : Window
    {
        private BitmapImage ChangedPhoto = null;
        public AddAnimalWindow()
        {
            InitializeComponent();
            GenderBox.ItemsSource = DBInteractions.GetGender();
            ShelterBox.ItemsSource = DBInteractions.GetShelter();
            SpecBox.ItemsSource = DBInteractions.GetSpecies();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Введённые данные не сохранятся.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AnimalList win2 = new AnimalList(PassClass.currentUser);
                win2.Show();
                this.Close();
            }
            
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NoteBox.Text == "Введите данные")
            {
                NoteBox.Text = "";
                NoteBox.Foreground = Brushes.Black;
            }
        }

        private void NoteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NoteBox.Text == "")
            {
                NoteBox.Text = "Введите данные";
                NoteBox.Foreground = Brushes.LightGray;
            }
        }

        private void BackButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            string ErrorMessage = "";

            if (NameBox.Text.Equals("Кличка") || NameBox.Text == "")
                ErrorMessage += "Не введена кличка животного \n";

            if (GenderBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбран пол живтоного \n";

            if (ShelterBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбран приют \n";

            if (SpecBox.SelectedIndex.Equals(-1))
                ErrorMessage += "Не выбран вид живтоного \n";

            if (NoteBox.Text.Equals("Введите данные"))
                ErrorMessage += "Не введено описание животного \n";

            if (ErrorMessage.Length != 0)
            {
                MessageBox.Show("Ошибка: \n" + ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddAnimal();
        }

        private void AddAnimal()
        {
            DBInteractions.SaveAnimal(FillAnimal(new Animal()));
            MessageBox.Show("Животное успешно добалено");
        }

        private Animal FillAnimal(Animal animal)
        {
            List<Gender> genders = DBInteractions.GetGender();

            animal.Name = NameBox.Text;
            animal.GenderID = GenderBox.SelectedIndex + 1;
            animal.SpeciesID = SpecBox.SelectedIndex + 1;
            animal.Notes = NoteBox.Text;
            animal.StatusID = 1;
            animal.ShelterID = ShelterBox.SelectedIndex + 1;


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

                animal.Photo = data;
            }
            else
            {
                animal.Photo = null;
            }

            return animal;
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
    }
}
