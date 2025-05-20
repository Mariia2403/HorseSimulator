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
    internal class Horse
    {

        public string Name { get; private set; }
        public Color Color { get; private set; }
        public TimeSpan? RaceTime { get; private set; }//скільки часу кінь витратив до фінішу

        public double Speed { get; private set; }          // фіксована швидкість
        public double Acceleration { get; private set; }   // змінюється на кожному кроці
        public double PositionX { get; set; }              // координата по X

        private Stopwatch stopwatch = new Stopwatch();               // для відліку часу
        private static Random random = new Random();              // рандом для всіх коней

        public Horse(string name, Color color)
        {
            Name = name;
            Color = color;
            Speed = random.Next(5, 11); // випадкова швидкість від 5 до 10
            PositionX = 0;
        }

        public async Task ChangeAccelerationAsync(Barrier barrier)
        {
            if (!stopwatch.IsRunning)
                stopwatch.Start();

            await Task.Delay(50);

            double k = random.NextDouble() * 0.3 + 0.7;
            Acceleration = Speed * k;
            PositionX += Acceleration;

            if (PositionX >= 1000 && RaceTime == null)
            {
                stopwatch.Stop();
                RaceTime = stopwatch.Elapsed;
            }

            barrier.SignalAndWait(); // чекаємо решту коней і малювання
        }

    }
}
