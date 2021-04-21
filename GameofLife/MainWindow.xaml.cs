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

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            const int anzahlZellenBreit = 20;
            const int anzahlZellenHoch = 20;
            var primColor = Brushes.Violet;
            var secColor = Brushes.Pink;

            for (int i = 0; i< anzahlZellenHoch; i++)
            {
                for(int j = 0; j < anzahlZellenBreit; j++)
                {
                    Rectangle r = new()
                    {
                        Width = zeichenflaeche.ActualWidth / anzahlZellenBreit - 2.0,
                        Height = zeichenflaeche.ActualHeight / anzahlZellenHoch -2.0,
                        Fill = primColor,
                    };

                    zeichenflaeche.Children.Add(r);

                    Canvas.SetLeft(r, j * zeichenflaeche.ActualWidth / anzahlZellenBreit - 2.0);
                    Canvas.SetTop(r, i * zeichenflaeche.ActualHeight / anzahlZellenHoch - 2.0);

                    r.MouseDown += R_MouseDown;
                }

            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill = 
                (((Rectangle)sender).Fill == Brushes.Violet)
                    ? Brushes.Pink : Brushes.Violet;
        }
    }
}
