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
        private RaceManager raceManager;
        //Dictionary<Horse, Image> horseImages = new Dictionary<Horse, Image>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int horseCount = 3; // або візьми з ComboBox/ListBox

            raceManager = new RaceManager(RaceCanvas, horseCount);
            raceManager.StartRace();
        }

        private void NextHorse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
