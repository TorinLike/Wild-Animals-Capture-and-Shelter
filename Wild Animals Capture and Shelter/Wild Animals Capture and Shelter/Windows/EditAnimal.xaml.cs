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
    /// Логика взаимодействия для EditAnimal.xaml
    /// </summary>
    public partial class EditAnimal : Window
    {
        private BitmapImage ChangedPhoto = null;

        private Animal animal1 { get; set; }
        public EditAnimal(Animal animal)
        {
            InitializeComponent();

            GenderBox.ItemsSource = DBInteractions.GetGender();
            ShelterBox.ItemsSource = DBInteractions.GetShelter();
            SpecBox.ItemsSource = DBInteractions.GetSpecies();

            animal1 = animal;
            NameBox.Text = animal1.Name;
            NoteBox.Text = animal1.Notes;
            GenderBox.SelectedItem = animal1.Gender;
            ShelterBox.SelectedItem = animal1.Shelter;
            SpecBox.SelectedItem = animal1.Species;

            if (animal1.Photo == null)
            {
                iPhoto.Source = new BitmapImage(new Uri("/Images/null_image.jpg", UriKind.Relative));
            }
            else
            {

                iPhoto.Source = animal.PhotoImage;
            }
            

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
            if (NameBox != null)
            {
                animal1.Name = NameBox.Text;
                animal1.GenderID = GenderBox.SelectedIndex + 1;
                animal1.SpeciesID = SpecBox.SelectedIndex + 1;
                animal1.Notes = NoteBox.Text;
                animal1.ShelterID = ShelterBox.SelectedIndex + 1;

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

                    animal1.Photo = data;
                }
                else if (animal1.Photo != null)
                {
                    animal1.Photo = animal1.Photo;
                }
                else
                {
                    animal1.Photo = null;
                }
                
            }
            else
            {
                MessageBox.Show("Поле клички не может быть пустым");
            }    
            DBInteractions.SaveEditedAnimal(animal1);
            MessageBox.Show("Изменения сохранены");

            AnimalList win2 = new AnimalList(PassClass.currentUser);
            win2.Show();
            this.Close();
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
