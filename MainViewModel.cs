using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<HorseInfo> Horses { get; set; } = new ObservableCollection<HorseInfo>();

        public void UpdateHorseInfo(string name, double newPosition, double newTime)
        {
            var horse = Horses.FirstOrDefault(h => h.Name == name);
            if (horse != null)
            {
                horse.Position = newPosition;
                horse.TimeRunning = newTime;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
