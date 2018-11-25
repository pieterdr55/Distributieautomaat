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
using System.Diagnostics;

namespace Distributieautomaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Producten { Chocolade = 150, Water = 200, Wafel = 250, Verassing = 350 };
        double[] bedrag = new double[] { 0.1, 0.2, 0.5, 1, 2 };
        int[] hoeveel = new int[] { 0, 0, 0, 0, 0 };
        int[] teruggave = new int[] { 0, 0, 0, 0, 0 };
        int[] artikelen= new int[] { 0,0,0,0};
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
            int keuzeindex = lstKeuze.SelectedIndex;
            int teller = 0;
            foreach (int artikel in artikelen)
            {
                if (teller == keuzeindex)
                {
                    artikelen[teller] = artikelen[teller] + 1;
                }
                teller = teller + 1;
            }

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
        }

        public void KiesBedrag()
        {
            double gekozenbedrag = Convert.ToDouble(lstInworp.SelectedValue);
            int gekozenindex = Convert.ToInt32(lstInworp.SelectedIndex);
            int teller = 0;
            int teller2 = 0;
            int teller3 = 0;
            lblinformatie3.Content = "";
            lblOverzicht.Content = "";
            bool wisselgeldbericht = false;

            foreach (double keuze in bedrag)
            {
                if (gekozenbedrag == keuze)
                {
                    hoeveel[teller] = hoeveel[teller] + 1;
                }
                teller = teller + 1;
            }

            Debug.WriteLine(hoeveel[gekozenindex] + " stukken van " + eurosymbool + bedrag[gekozenindex]);
            
        
            ingeworpen = ingeworpen + gekozenbedrag;
            lblWisselgeld.Content = "jouw inworp" + Environment.NewLine +
                eurosymbool + " " + ingeworpen;

            ingeworpen = Math.Round(ingeworpen, 2);

            if (ingeworpen == prijs)
            {
                lblinformatie3.Content = "Geniet van je " + artikel;
                lblinformatie3.Visibility = Visibility.Visible;
                omzet = omzet + prijs;
                btnNieuweklant.Content = "Nieuwe Klant";
                lstKeuze.IsEnabled = false;
                lstInworp.IsEnabled = false;
                btnSamenvatting.Visibility = Visibility.Visible;
                
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
                

                
                while (wisselgeld>=2)
                {
                    teruggave[4] = teruggave[4] + 1;
                    wisselgeld = wisselgeld - 2;
                }

                while (wisselgeld >= 1)
                {
                    teruggave[3] = teruggave[3] + 1;
                    wisselgeld = wisselgeld - 1;
                }

                while (wisselgeld >= 0.5)
                {
                    teruggave[2] = teruggave[2] + 1;
                    wisselgeld = wisselgeld - 0.5;
                }

                while (wisselgeld >= 0.2)
                {
                    teruggave[1] = teruggave[1] + 1;
                    wisselgeld = wisselgeld - 0.2;
                }

                while (wisselgeld >= 0.1)
                {
                    teruggave[0] = teruggave[0] + 1;
                    wisselgeld = wisselgeld - 0.1;
                }

                foreach (int keuze in bedrag)
                {
                    lblOverzicht.Content += teruggave[teller2] + " stukken van " + eurosymbool + bedrag[teller2] + Environment.NewLine;
                    teller2 = teller2 + 1;
                }

                foreach (int muntstuk in teruggave)
                {
                    if(hoeveel[teller3]>=teruggave[teller3])
                    {
                        hoeveel[teller3] = hoeveel[teller3] - teruggave[teller3];
                        teller3 = teller3 + 1;
                    }
                    else if (wisselgeldbericht == false)
                    {
                        MessageBox.Show("De automaat kan geen wisselgeld teruggeven, spijtig");
                        wisselgeldbericht = true;
                    }
                    else
                    {

                    }
                }


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
            int teller2 = 0;

            lblSamenvatting.Content = "";

            foreach (int aantal in hoeveel)
            {
                lblSamenvatting.Content += hoeveel[teller] + " stukken van " + eurosymbool + " " + bedrag[teller] + Environment.NewLine;
                teller = teller + 1;
            }

            foreach (string product in Enum.GetNames(typeof(Producten)))
            {
                lblSamenvatting.Content += product + ":" + artikelen[teller2] + Environment.NewLine;
                teller2 = teller2 + 1;
            }
            lblInformatie4.Visibility = Visibility.Visible;
            lblSamenvatting.Visibility = Visibility.Visible;
        }

        

        private void btnNieuweklant_Click(object sender, RoutedEventArgs e)
        {
            Annuleer();
        }

        public void Annuleer()
        {
            int teller = 0;
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
            foreach(int muntstuk in teruggave)
            {
                teruggave[teller] = 0;
                teller = teller + 1;
            }
        }

        
    }
}
