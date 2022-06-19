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
    /// Логика взаимодействия для AnimalList.xaml
    /// </summary>
    public partial class AnimalList : Window
    {
        Profile profile1 = null;

        public AnimalList()
        {
            InitializeComponent();
            ShelterBox.ItemsSource = DBInteractions.GetShelter();
            SpeciesBox.ItemsSource = DBInteractions.GetSpecies();

            ALAnimalList.ItemsSource = DBInteractions.GetAnimal();
            AddAnimalBut.Visibility = Visibility.Hidden;
            EditAnimalBut.Visibility = Visibility.Hidden; 
            DeleteAnimalBut.Visibility = Visibility.Hidden;
            JournalBut.Visibility = Visibility.Hidden;
            WorkersBut.Visibility = Visibility.Hidden;
        }

        public AnimalList(Profile profile)
        {
            InitializeComponent();

            profile1 = profile;
            ALAnimalList.ItemsSource = DBInteractions.GetAnimal();
            if (profile.RoleID == 1)
            {
                ALAnimalList.ItemsSource = DBInteractions.GetAnimal();
                AddAnimalBut.Visibility = Visibility.Hidden;
                EditAnimalBut.Visibility = Visibility.Hidden;
                DeleteAnimalBut.Visibility = Visibility.Hidden;
                JournalBut.Visibility = Visibility.Hidden;
                WorkersBut.Visibility = Visibility.Hidden;
            }
            else if (profile.RoleID == 2)
            {
                ALAnimalList.ItemsSource = DBInteractions.GetAnimal();
                WorkersBut.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CaptureWindow win2 = new CaptureWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow win2 = new MainWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Animal selectedAnimal = (Animal)ALAnimalList.SelectedItem;
            if (selectedAnimal.StatusID == 1)
            {
                selectedAnimal.StatusID = 2;
                DBInteractions.SaveAnimal(selectedAnimal);
                UpdateAnimalList();
                MessageBox.Show("Статус изменён");
            }
            else if (selectedAnimal.StatusID == 2)
            {
                selectedAnimal.StatusID = 1;
                DBInteractions.SaveAnimal(selectedAnimal);
                UpdateAnimalList();
                MessageBox.Show("Статус изменён");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Workers win2 = new Workers();
            win2.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CaptureJournalWindow win2 = new CaptureJournalWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AddAnimalWindow win2 = new AddAnimalWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Cabinet win2 = new Cabinet(PassClass.currentUser);
            win2.Show();
            this.Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (ALAnimalList.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы уверены? Данная запись будет безвозвратно удалена.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DBInteractions.DeleteAnimal((Animal)ALAnimalList.SelectedItem);
                    MessageBox.Show("Животное успешно удалено");
                    ALAnimalList.ItemsSource = DBInteractions.GetAnimal();
                }
            }
            else
            {
                MessageBox.Show("Выберите животное");
            }
        }

       

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "Введите данные")
            {
                SearchBar.Text = "";
                SearchBar.Foreground = Brushes.Black;
            }
        }

        private void NoteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "")
            {
                SearchBar.Text = "Введите данные";
                SearchBar.Foreground = Brushes.LightGray;
            }
        }

        private void UpdateAnimalList()
        {
            if (SearchBar is null || ShelterBox is null || SpeciesBox is null || ShelterBox.SelectedItem is null || SpeciesBox.SelectedItem is null)
            {
                return;
            }

            List<Animal> animalList = DBInteractions.GetAnimal();

            if (SearchBar.Text != "Введите данные")
            {
                animalList = animalList.Where(i => i.Name.Contains(SearchBar.Text)).ToList();
            }

            if (((Shelter)ShelterBox.SelectedItem).Name != "Все")
            {
                animalList = animalList.Where(m => m.Shelter == ((Shelter)ShelterBox.SelectedItem)).ToList();
            }
            if (((Species)SpeciesBox.SelectedItem).Title != "Все")
            {
                animalList = animalList.Where(m => m.Species == ((Species)SpeciesBox.SelectedItem)).ToList();
            }
            ALAnimalList.ItemsSource = animalList;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAnimalList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAnimalList();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateAnimalList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var shelters = DBInteractions.GetShelter();
            shelters.Insert(0, new Shelter { Name = "Все" });
            var species = DBInteractions.GetSpecies();
            species.Insert(0, new Species { Title = "Все" });

            ShelterBox.ItemsSource = shelters;
            SpeciesBox.ItemsSource = species;

            ShelterBox.SelectedIndex = 0;
            SpeciesBox.SelectedIndex = 0;
        }

        private void EditAnimalBut_Click(object sender, RoutedEventArgs e)
        {
            
            if (ALAnimalList.SelectedIndex != -1)
            {
                EditAnimal win2 = new EditAnimal((Animal)ALAnimalList.SelectedItem);
                win2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выделите животное");
            }
        }
 
        
    }
}
