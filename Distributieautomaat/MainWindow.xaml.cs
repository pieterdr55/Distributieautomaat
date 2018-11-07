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

namespace Distributieautomaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Producten { Chocolade=150, wafel=200, chips=250, pannenkoek=350 };
        double[] bedrag = new double[] { 0.1, 0.2, 0.5, 1, 2 };
        int[] hoeveel = new int[] { 0, 0, 0, 0, 0 };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Opstart();
        }
        
        public void Opstart()
        {
            for (int i=0;i<351;i++)
            {
                if  (Enum.IsDefined(typeof(Producten), i))
                {
                    lstKeuze.Items.Add((Producten)i);
                }
            }

            for (int j = 0;j<5;j++)
            {
                lstInworp.Items.Add(bedrag[j]);
            }

            lblInworp.Visibility = Visibility.Hidden;
            lstInworp.Visibility = Visibility.Hidden;
            btnSamenvatting.Visibility = Visibility.Hidden;
            lblInformatie4.Visibility = Visibility.Hidden;
            lblSamenvatting.Visibility = Visibility.Hidden;
        }

        private void lstKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void KeuzeArtikel()
        {
            string artikel = Convert.ToString(lstKeuze.SelectedValue);
            double index = (double)Enum.Parse(typeof(Producten), artikel);
            double prijs = index / 100;
            lblKeuze.Content = artikel + " " + prijs;
        }
    }
}
