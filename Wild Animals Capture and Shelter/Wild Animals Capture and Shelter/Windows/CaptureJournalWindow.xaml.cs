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
    /// Логика взаимодействия для CaptureJournalWindow.xaml
    /// </summary>
    public partial class CaptureJournalWindow : Window
    {
        public CaptureJournalWindow()
        {
            InitializeComponent();
            CaptureList.ItemsSource = DBInteractions.GetCapture();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Данная запись будет безвозвратно удалена.", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DBInteractions.DeleteCapture((CaptureHistory)CaptureList.SelectedItem);
                MessageBox.Show("Запись успешно удалена");
                CaptureList.ItemsSource = DBInteractions.GetCapture();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AnimalList win2 = new AnimalList(PassClass.currentUser);
            win2.Show();
            this.Close();
        }
    }
}
