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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RaceManager raceManager =null;
        public MainViewModel ViewModel { get; } = new MainViewModel();
        //Dictionary<Horse, Image> horseImages = new Dictionary<Horse, Image>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
           
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (horseCountComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                int horseCount = int.Parse(selectedItem.Content.ToString());

                // перший запуск — створити RaceManager
                if (raceManager == null)
                {
                    raceManager = new RaceManager(RaceCanvas, horseCount, ViewModel);
                }
                else
                {
                    // вдруге — просто скинути існуючих коней
                    raceManager.ResetRace();
                }

                raceManager.StartRace();
            }
            else
            {
                MessageBox.Show("Choose the number of horses!");
            }

        }

        private void horseCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (horseCountComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                int horseCount = int.Parse(selectedItem.Content.ToString());

                // Очистити старе
                RaceCanvas.Children.Clear();
                ViewModel.Horses.Clear();

                // Створити і запустити нових коней
                raceManager = new RaceManager(RaceCanvas, horseCount, ViewModel);
                
            }
        }

      
    }
}
