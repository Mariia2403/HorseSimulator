using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public class Horse
    {

        private static Random random = new Random();

        public string Name { get; private set; }
        public Color Color { get; private set; }
        public double PositionX { get;  set; } = 0;
        public TimeSpan Time { get; private set; }
        public double Speed { get; private set; }
        public double Acceleration { get; private set; }

      //  private DateTime _startTime;

        public Horse(string name, Color color)
        {
            Name = name;
            Color = color;
            Speed = random.Next(5, 11); // швидкість 5-10
        }

        public async Task RunAsync(Barrier barrier)
        {
            while (PositionX < 410)
            {
                ChangeAcceleration();
                barrier.SignalAndWait();//!!!!
                await Task.Delay(10);//!!!!!
            }
        }
        public void ResetForNewRace(Random rnd)
        {
            PositionX = 0;
            Speed = rnd.Next(5, 11);
            Acceleration = Speed * rnd.NextDouble() * 0.3 + 0.7; // Наприклад 0.7–1.0 множник
        }
        private void ChangeAcceleration()
        {
            Acceleration = Speed * (random.NextDouble() * 0.3 + 0.7);
        }

    }
}
