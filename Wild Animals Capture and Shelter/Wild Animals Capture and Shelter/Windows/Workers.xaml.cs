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
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {

        public Workers()
        {
            InitializeComponent();
            WorkerList.ItemsSource = DBInteractions.GetProfile();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnimalList win2 = new AnimalList(PassClass.currentUser);
            win2.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow win2 = new AddWorkerWindow();
            win2.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (WorkerList.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы уверены? Данная запись будет безвозвратно удалена.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DBInteractions.DeleteProfile((Profile)WorkerList.SelectedItem);
                    MessageBox.Show("Запись успешно удалена");
                    WorkerList.ItemsSource = DBInteractions.GetProfile();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (WorkerList.SelectedIndex != -1)
            {
                EditWorker win2 = new EditWorker((Profile)WorkerList.SelectedItem); ;
                win2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выделите профиль");
            }
        }
    }
}
