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
            const int anzahlZellenBreit = 30;
            const int anzahlZellenHoch = 30;

            for (int i = 0; i< anzahlZellenHoch; i++)
            {
                for(int j = 0; j < anzahlZellenBreit; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = zeichenflaeche.ActualWidth / anzahlZellenBreit;
                    r.Height = zeichenflaeche.ActualHeight / anzahlZellenHoch;
                    r.Fill = Brushes.Violet;
                    zeichenflaeche.Children.Add(r);
                    Canvas.SetLeft(r,j*r.Width);
                    Canvas.SetTop(r, i * r.Height);

                }

            }
        }
    }
}
