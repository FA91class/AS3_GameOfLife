using GameofLife.config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace GameofLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer timer = new();
        Rectangle[,] felder = new Rectangle[0, 0];
      
        public MainWindow()
        {
            InitializeComponent();

            CreateCanvas(Config.anzahlZellenHoch, Config.anzahlZellenBreit);

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill =
                (((Rectangle)sender).Fill == Config.primColor)
                    ? Config.secColor : Config.primColor;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int[,] anzahlnachbarn = new int[Config.anzahlZellenHoch, Config.anzahlZellenBreit];
            for (int i = 0; i < Config.anzahlZellenHoch; i++)
            {
                for (int j = 0; j < Config.anzahlZellenBreit; j++)
                {
                    int nachbarn = 0;
                    int iUEber = i - 1;
                    int iUnter = i + 1;
                    int jLinks = j - 1;
                    int jRechts = j + 1;

                    if (iUEber < 0)
                    {
                        iUEber = Config.anzahlZellenHoch - 1;
                    }

                    if (iUnter == Config.anzahlZellenHoch)
                    {
                        iUnter = 0;
                    }

                    if (jLinks < 0)
                    {
                        jLinks = Config.anzahlZellenBreit - 1;
                    }

                    if (jRechts == Config.anzahlZellenBreit)
                    {
                        jRechts = 0;
                    }

                    // Felder Check

                    if (felder[iUEber, jLinks].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iUEber, j].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iUEber, jRechts].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[i, jLinks].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[i, jRechts].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iUnter, jLinks].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iUnter, j].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iUnter, jRechts].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    anzahlnachbarn[i, j] = nachbarn;
                }
            }

            for (int i = 0; i < Config.anzahlZellenHoch; i++)
            {
                for (int j = 0; j < Config.anzahlZellenBreit; j++)
                {
                    if (anzahlnachbarn[i, j] < 2 || anzahlnachbarn[i, j] > 3)
                    {
                        felder[i, j].Fill = Config.primColor;
                    }
                    else if (anzahlnachbarn[i, j] == 3)
                    {
                        felder[i, j].Fill = Config.secColor;
                    }
                }
            }
        }


        public void CreateCanvas(int height, int width)
        {
            zeichenflaeche.Children.Clear();

            Random random = new();
            felder = new Rectangle[height, width];


           zeichenflaeche.Measure(
               new Size(
                       double.PositiveInfinity,
                       double.PositiveInfinity
                   ));

            zeichenflaeche.Arrange(
                new Rect(
                        0.0,
                        0.0,
                        zeichenflaeche.DesiredSize.Width,
                        zeichenflaeche.DesiredSize.Height
                    ));


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < Config.anzahlZellenBreit; j++)
                {
                    Rectangle r = new()
                    {
                        Width = zeichenflaeche.ActualWidth / width - 2.0,
                        Height = zeichenflaeche.ActualHeight / height - 2.0,
                        Fill = (random.Next(0, 2) == 1) ? Config.primColor : Config.secColor,
                    };

                 zeichenflaeche.Children.Add(r);

                    Canvas.SetLeft(r, j * zeichenflaeche.ActualWidth / width - 2.0);
                    Canvas.SetTop(r, i * zeichenflaeche.ActualHeight / height - 2.0);

                    r.MouseDown += R_MouseDown;

                    felder[i, j] = r;
                }

            }
        }

        private void ButtonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                ButtonStartStop.Content = "Starte Animation";
            }
            else
            {
                timer.Start();
                ButtonStartStop.Content = "Stoppe Animation";
            }
        }

        private void StepByStep_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Stop();

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow configurationWindow = new();
            configurationWindow.ShowDialog();
           
            CreateCanvas(Config.anzahlZellenHoch, Config.anzahlZellenBreit);
        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
