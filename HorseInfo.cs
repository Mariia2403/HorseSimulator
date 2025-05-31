using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public class HorseInfo : INotifyPropertyChanged
    {
        public string Name { get; set; }
        //public string ColorBrush { get; set; }

        private double _position;
        public SolidColorBrush ColorBrush { get; set; }
        public double Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private double _timeRunning;
        public double TimeRunning
        {
            get => _timeRunning;
            set
            {
                _timeRunning = value;
                OnPropertyChanged(nameof(TimeRunning));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

}
