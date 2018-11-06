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
        enum Producten { Chocolade, wafel, chips, pannenkoek };
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
            for (int i=0;i<4;i++)
            {
                lstKeuze.Items.Add((Producten)i);
                lstInworp.Items.Add(bedrag[i]);
            }
            lblInworp.Visibility = Visibility.Hidden;
            lstInworp.Visibility = Visibility.Hidden;
            btnSamenvatting.Visibility = Visibility.Hidden;
            lblInformatie4.Visibility = Visibility.Hidden;
            lblSamenvatting.Visibility = Visibility.Hidden;
            
            
        }
    }
}
