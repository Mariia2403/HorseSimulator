using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;


namespace WpfApp1
{
    public class RaceManager
    {
        private List<Horse> horses = new List<Horse>();
        private Dictionary<Horse, Image> horseImages = new Dictionary<Horse, Image>();

        private Dictionary<Horse, List<ImageSource>> horseAnimations = new Dictionary<Horse, List<ImageSource>>();

        private Canvas raceCanvas;
        private Barrier barrier;
        private int frameIndex = 0;//номер поточного кадру для анімації коней.

        private Random rnd = new Random();
        public RaceManager(Canvas canvas, int horseCount)
        {
            raceCanvas = canvas;
            CreateHorses(horseCount);
            barrier = new Barrier(horseCount + 1); // +1 для Render
        }

       

        private void CreateHorses(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Color color = Color.FromRgb(
                                (byte)rnd.Next(256),
                                (byte)rnd.Next(256),
                                (byte)rnd.Next(256));

                Horse horse = new Horse($"Horse {i + 1}", color);

                horses.Add(horse);

                var horseFrames = HorseAnimator.GetHorseAnimation(color);
                horseAnimations[horse] = horseFrames;

                Image img = new Image
                {
                    Width = 50,
                    Height = 50,
                    Source = horseFrames[0]
                };

                Canvas.SetLeft(img, 0);
                Canvas.SetTop(img, i * 60);
                raceCanvas.Children.Add(img);
                horseImages[horse] = img;
            }
        }

        private async Task RenderLoop()
        {
            bool running = true;

            while (running)
            {
                barrier.SignalAndWait();

                foreach (Horse horse in horses)
                {
                    horse.PositionX += horse.Acceleration;
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var horse in horses)
                    {
                        var img = horseImages[horse];
                        var frames = horseAnimations[horse];

                        Canvas.SetLeft(img, horse.PositionX);
                        img.Source = frames[frameIndex];
                    }

                    frameIndex = (frameIndex + 1) % 12;
                });

                running = horses.Any(h => h.PositionX < 400); // траса
                await Task.Delay(100);
            }
        }

        public void StartRace()
        {
            //запускає окрему асинхронну задачу для кожного коня
            //означає, що кожен кінь "біжить" у своєму потоці, незалежно від інших.

            foreach (var horse in horses)
            {
                //метод у класі Horse, де він рандомно обирає прискорення, чекає на бар'єрі, і так далі.
                Task.Run(() => horse.RunAsync(barrier));//barrier передається, щоб синхронізувати коня з іншими кіньми та Render.
            }

            Task.Run(() => RenderLoop());
        }
    }
}
