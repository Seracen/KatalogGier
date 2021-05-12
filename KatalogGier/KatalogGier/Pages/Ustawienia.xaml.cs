using KatalogGier.Classes;
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

namespace KatalogGier.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Ustawienia.xaml
    /// </summary>
    public partial class Ustawienia : Page
    {
        public Uzytkownik zalogowanyUzytkownik { get; set; }
        public int iloscGier { get; set; }
        public Ustawienia()
        {
            InitializeComponent();
        }
        public Ustawienia(Uzytkownik uzytkownik,int i)
        {
            InitializeComponent();
            zalogowanyUzytkownik = uzytkownik;
            iloscGier = i;
        }

        private void Powrot(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loginTextBox.Text = zalogowanyUzytkownik.GetLogin();
            hasloTextBox.Text = zalogowanyUzytkownik.GetHaslo();
            wyswietlanaTextBox.Text = zalogowanyUzytkownik.GetWyswietlana_nazwa();
            if (zalogowanyUzytkownik.GetOcenione_gry() == "")
            {
                brakLabel.Visibility = Visibility.Visible;
                listaOcenionychGierListView.Visibility = Visibility.Hidden;
            }
            else
            {
                
                brakLabel.Visibility = Visibility.Hidden;
                listaOcenionychGierListView.Visibility = Visibility.Visible;
                List<Gra> listaOcenionychGier = DbConnection.GetRatedGames(zalogowanyUzytkownik);
                listaOcenionychGierListView.ItemsSource = listaOcenionychGier;
            }
        }

        private void AktualizujDane(object sender, RoutedEventArgs e)
        {
            string l = loginTextBox.Text;
            string h = hasloTextBox.Text;
            string n = wyswietlanaTextBox.Text;

            if(l!=zalogowanyUzytkownik.GetLogin() || h!=zalogowanyUzytkownik.GetHaslo() || n != zalogowanyUzytkownik.GetWyswietlana_nazwa())
            {
                DbConnection.UpdateUser(zalogowanyUzytkownik, l, h, n);
                zalogowanyUzytkownik = DbConnection.VerifyUser(l, h);
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
            }
        }

        private void WybranaGra(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new OcenianieGier(DbConnection.GetGraById(Int32.Parse(grid.Tag+"")), zalogowanyUzytkownik, iloscGier);
        }

        private void DeleteUser(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Jesteś pewny, że chcesz usunąć konto?", "Usuwanie konta", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                DbConnection.UsunKonto(zalogowanyUzytkownik);
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Logowanie();
            }
        }
    }
}
