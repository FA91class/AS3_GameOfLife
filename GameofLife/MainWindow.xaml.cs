using GameofLife.config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace GameofLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Rectangle[,] felder = new Rectangle[Config.anzahlZellenBreit, Config.anzahlZellenHoch];

        DispatcherTimer timer = new DispatcherTimer();

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Config.anzahlZellenHoch; i++)
            {
                for (int j = 0; j < Config.anzahlZellenBreit; j++)
                {
                    Rectangle r = new()
                    {
                        Width = zeichenflaeche.ActualWidth / Config.anzahlZellenBreit - 2.0,
                        Height = zeichenflaeche.ActualHeight / Config.anzahlZellenHoch - 2.0,
                        Fill = Config.primColor,
                    };

                    zeichenflaeche.Children.Add(r);

                    Canvas.SetLeft(r, j * zeichenflaeche.ActualWidth / Config.anzahlZellenBreit - 2.0);
                    Canvas.SetTop(r, i * zeichenflaeche.ActualHeight / Config.anzahlZellenHoch - 2.0);

                    r.MouseDown += R_MouseDown;

                    felder[i, j] = r;
                }

            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill =
                (((Rectangle)sender).Fill == Config.primColor)
                    ? Config.secColor : Config.primColor;
        }

        private void nextStep_Click(object sender, RoutedEventArgs e)
        {
            int[,] anzahlnachbarn = new int[Config.anzahlZellenHoch, Config.anzahlZellenBreit];
            for (int i = 0; i < Config.anzahlZellenHoch; i++)
            {
                for (int j = 0; j < Config.anzahlZellenBreit; j++)
                {
                    int nachbarn = 0;
                    int iÜber = i - 1;
                    int iUnter = i + 1;
                    int jLinks = j - 1;
                    int jRechts = j + 1;

                    if (iÜber < 0)
                    {
                        iÜber = Config.anzahlZellenHoch - 1;
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

                    if (felder[iÜber, jLinks].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iÜber, j].Fill == Config.secColor)
                    {
                        nachbarn++;
                    }

                    if (felder[iÜber, jRechts].Fill == Config.secColor)
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
    }
}
