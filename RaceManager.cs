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
using System.Diagnostics;


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

        private MainViewModel viewModel;
        private Stopwatch stopwatch = new Stopwatch();
        private const double FinishLine = 410;


        //public double StartPositionX { get; set; }//змінити позиці. x
        public RaceManager(Canvas canvas, int horseCount, MainViewModel vm)
        {
            raceCanvas = canvas;
            viewModel = vm;
            stopwatch.Start();
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
                    Width = 80,
                    Height = 80,
                    Source = horseFrames[0]
                };
                //int startPosition = 10;
                Canvas.SetLeft(img, 0);
               // Canvas.SetTop(img, i * 40);
                Canvas.SetBottom(img, i * 40);
                raceCanvas.Children.Add(img);
                horseImages[horse] = img;

                viewModel.Horses.Add(new HorseInfo
                {
                    Name = horse.Name,
                    ColorBrush = new SolidColorBrush(color),
                    Position = horse.PositionX,
                    TimeRunning = 0
                });

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
                        viewModel.UpdateHorseInfo(horse.Name, horse.PositionX, stopwatch.Elapsed.TotalSeconds); 

                        Image img = horseImages[horse];
                        var frames = horseAnimations[horse];

                        Canvas.SetLeft(img, horse.PositionX);
                        img.Source = frames[frameIndex];
                    }

                    frameIndex = (frameIndex + 1) % 12;
                });

                // перевіряємо, чи хтось фінішував
                var winner = horses.FirstOrDefault(h => h.PositionX >= FinishLine);

                if (winner != null)
                {
                    running = false;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"{winner.Name} виграв гонку за {stopwatch.Elapsed.TotalSeconds:F2} секунд!", "Фініш ");
                        // Повертаємо всіх коней на початок
                        foreach (var horse in horses)
                        {
                            horse.PositionX = 0;
                            var img = horseImages[horse];
                            Canvas.SetLeft(img, 0);
                        }

                        frameIndex = 0; // обнуляємо кадри
                    });

                    break;
                }

                await Task.Delay(100);
            }
        }

        public void ResetRace()
        {
            foreach (var horse in horses)
            {
                horse.ResetForNewRace(rnd);

                // оновити в таблиці
                var info = viewModel.Horses.FirstOrDefault(h => h.Name == horse.Name);
                if (info != null)
                {
                    info.Position = 0;
                    info.TimeRunning = 0;
                }

                // пересунути зображення назад
                var img = horseImages[horse];
                Canvas.SetLeft(img, 0);
            }

            frameIndex = 0;
            stopwatch.Restart();
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
