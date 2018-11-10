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
        double[] bedrag = new double[] { 0.1, 0.2, 0.5, 1, 2};
        int[] hoeveel = new int[] { 0, 0, 0, 0, 0 };
        int[] hoeveeltotaal = new int[] { 0, 0, 0, 0, 0 };
        char eurosymbool = '€';
        double ingeworpen = 0;
        double prijs = 0;
        double wisselgeld = 0;
        string artikel = "";
        double omzet = 0;
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
            lblinformatie3.Visibility = Visibility.Hidden;
            lblKeuze.Visibility = Visibility.Hidden;
            lblWisselgeld.Visibility = Visibility.Hidden;
            lblOverzicht.Visibility = Visibility.Hidden;
        }


        private void lstKeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeuzeArtikel();
        }

        public void KeuzeArtikel()
        {
            artikel = Convert.ToString(lstKeuze.SelectedValue);
            int index = (int)Enum.Parse(typeof(Producten), artikel);
            double indexdl = Convert.ToDouble(index);
            prijs = indexdl / 100;
            lblKeuze.Content = artikel + " "+ eurosymbool + prijs;


            lblInworp.Visibility = Visibility.Visible;
            lstInworp.Visibility = Visibility.Visible;
            btnSamenvatting.Visibility = Visibility.Visible;
            lblInformatie4.Visibility = Visibility.Visible;
            lblSamenvatting.Visibility = Visibility.Visible;
            lblKeuze.Visibility = Visibility.Visible;

        }

        private void lstInworp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KiesBedrag();
            lstInworp.SelectedIndex = -1;
        }

        public void KiesBedrag()
        {
            double gekozenbedrag = Convert.ToDouble(lstInworp.SelectedValue);
            int teller = 0;
            lblinformatie3.Content = "";
            lblOverzicht.Content = "";


            foreach (double keuze in bedrag)
            {
                if (gekozenbedrag == keuze)
                {
                    hoeveel[teller] = hoeveel[teller] + 1;
                }
                lblOverzicht.Content += hoeveel[teller] + " stukken van " + eurosymbool + " " + bedrag[teller] + Environment.NewLine;
                teller = teller + 1;
            }
            
            ingeworpen = ingeworpen + gekozenbedrag;
            lblWisselgeld.Content = "jouw inworp" + Environment.NewLine +
                eurosymbool + " " + ingeworpen;

            if (ingeworpen == prijs)
            {
                lblinformatie3.Content = "Geniet van je " + artikel;
                lblinformatie3.Visibility = Visibility.Visible;
                omzet = omzet + prijs;
                btnNieuweklant.Content = "Nieuwe Klant";
                lstKeuze.IsEnabled = false;
                lstInworp.IsEnabled = false;
                btnSamenvatting.Visibility = Visibility.Visible;
                Voegtoeaantotaal();
            }

            else if (ingeworpen > prijs)
            {
                wisselgeld = ingeworpen - prijs;
                lblWisselgeld.Content = "jouw wisselgeld bedraagt " + eurosymbool + wisselgeld;
                lblWisselgeld.Visibility = Visibility.Visible;
                lblinformatie3.Content = "Geniet van je " + artikel;
                lblinformatie3.Visibility = Visibility.Visible;
                omzet = omzet + prijs;
                btnNieuweklant.Content = "Nieuwe Klant";
                lstKeuze.IsEnabled = false;
                lstInworp.IsEnabled = false;
                btnSamenvatting.Visibility = Visibility.Visible;
                lblOverzicht.Visibility = Visibility.Visible;
                Voegtoeaantotaal();
            }
            else
            {
                lblWisselgeld.Content = "jouw inworp";
                lblinformatie3.Content = eurosymbool +" "+ ingeworpen;
                lblWisselgeld.Visibility = Visibility.Visible;
                lblinformatie3.Visibility = Visibility.Visible;
            }
        }

        private void btnSamenvatting_Click(object sender, RoutedEventArgs e)
        {
            Samenvatting();
        }

        public void Samenvatting()
        {
            int teller = 0;

            lblSamenvatting.Content = "";

            foreach (int aantal in hoeveeltotaal)
            {
                lblSamenvatting.Content += hoeveeltotaal[teller] + " stukken van " + eurosymbool + " " + bedrag[teller] + Environment.NewLine;
                teller = teller + 1;
            }
            lblInformatie4.Visibility = Visibility.Visible;
            lblSamenvatting.Visibility = Visibility.Visible;
        }

        public void Voegtoeaantotaal()
        {
            int teller = 0;

            foreach (int aantal in hoeveel)
            {
                hoeveeltotaal[teller] += hoeveel[teller];
                teller = teller + 1;
            }
        }

        private void btnNieuweklant_Click(object sender, RoutedEventArgs e)
        {
            Annuleer();
        }

        public void Annuleer()
        {
            int teller = 0;
            foreach (int aantal in hoeveel)
            {
                hoeveel[teller] = 0;
                teller = teller + 1;
            }
            ingeworpen = 0;
            wisselgeld = 0;
            lstKeuze.IsEnabled = true;
            lstInworp.Visibility = Visibility.Hidden;
            lstInworp.IsEnabled = true;
            lblKeuze.Visibility = Visibility.Hidden;
            lblWisselgeld.Visibility = Visibility.Hidden;
            lblinformatie3.Visibility = Visibility.Hidden;
            lblOverzicht.Visibility = Visibility.Hidden;
            lblInworp.Visibility = Visibility.Hidden;
            btnSamenvatting.Visibility = Visibility.Hidden;
            lblInformatie4.Visibility = Visibility.Hidden;
            lblSamenvatting.Visibility = Visibility.Hidden;
        }

    }
}
