using KatalogGier.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace KatalogGier.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy OcenianieGier.xaml
    /// </summary>
    public partial class OcenianieGier : Page
    {
        public Gra ocenianaGra { get; set; }
        public Uzytkownik zalogowanyUzytkownik { get; set; }
        public int iloscGier { get; set; }
        public Producent producent { get; set; }
        public int ocena { get; set; }
        public OcenianieGier()
        {
            InitializeComponent();
        }
        public OcenianieGier(Gra gra, Uzytkownik user, int i)
        {
            InitializeComponent();
            ocenianaGra = gra;
            zalogowanyUzytkownik = user;
            iloscGier = i;
        }

        private void PrevGame(object sender, MouseButtonEventArgs e)
        {
            if (ocenianaGra.GetGra_Id() > 1)
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new OcenianieGier(DbConnection.GetGraById(ocenianaGra.GetGra_Id() - 1), zalogowanyUzytkownik, iloscGier);
            else
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ocenianaGraLabel.Text = ocenianaGra.GetNazwa();
            ocenianaGraHyperLink.NavigateUri = new Uri(ocenianaGra.GetLink());
            ocenianaGraImage.Source = ocenianaGra.GetImage();
            graOpis.Text = ocenianaGra.GetOpis();
            producent = DbConnection.GetProducentByGameId(ocenianaGra.GetGra_Id());
            graProducent.Content = producent.GetNazwa();
            sredniaOcenaLabel.Content = ocenianaGra.GetSrednia_ocena();
            krajProdukcjiLabel.Content = producent.GetKraj();
            wymaganiaLabel.Content = ocenianaGra.GetWymagania().Replace(';', ' ');
            List<Gra> ocenione = DbConnection.GetRatedGames(zalogowanyUzytkownik);
            if (ocenione!=null && ocenione.Any(x => x.GetGra_Id() == ocenianaGra.GetGra_Id()))
            {
                ocenianie.Visibility = Visibility.Hidden;
                Oceniona.Visibility = Visibility.Visible;
                foreach(var item in ocenione)
                {
                    if(item.GetGra_Id() == ocenianaGra.GetGra_Id())
                    {
                        ocenaLabel.Content = zalogowanyUzytkownik.GetRateByGameId(ocenianaGra.GetGra_Id());
                    }
                }
            }
            else
            {
                ocenianie.Visibility = Visibility.Visible;
                Oceniona.Visibility = Visibility.Hidden;
            }
        }

        private void NextGame(object sender, MouseButtonEventArgs e)
        {
            if (ocenianaGra.GetGra_Id() < iloscGier)
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new OcenianieGier(DbConnection.GetGraById(ocenianaGra.GetGra_Id() + 1), zalogowanyUzytkownik, iloscGier);
            else
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
        }

        private void SetOcena(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            ocena = Int32.Parse(radio.Tag.ToString());
        }

        private void Ocen(object sender, RoutedEventArgs e)
        {
            DbConnection.OcenGre(zalogowanyUzytkownik, ocenianaGra, ocena);
            ocenianie.Visibility = Visibility.Hidden;
            Oceniona.Visibility = Visibility.Visible;
            ocenaLabel.Content = ocena + "";

            zalogowanyUzytkownik = DbConnection.VerifyUser(zalogowanyUzytkownik.GetLogin(), zalogowanyUzytkownik.GetHaslo());
            ocenianaGra = DbConnection.GetGraById(ocenianaGra.GetGra_Id());
            sredniaOcenaLabel.Content = ocenianaGra.GetSrednia_ocena();
        }

        private void DeleteOcena(object sender, RoutedEventArgs e)
        {
            DbConnection.UsunOcene(zalogowanyUzytkownik, ocenianaGra);
            ocenianaGra = DbConnection.GetGraById(ocenianaGra.GetGra_Id());
            sredniaOcenaLabel.Content = ocenianaGra.GetSrednia_ocena();
            ocenianie.Visibility = Visibility.Visible;
            Oceniona.Visibility = Visibility.Hidden;
            zalogowanyUzytkownik = DbConnection.VerifyUser(zalogowanyUzytkownik.GetLogin(), zalogowanyUzytkownik.GetHaslo());
            MessageBox.Show("Pomyślnie usunięto ocenę!");
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
        }

        private void ocenianaGraHyperLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
