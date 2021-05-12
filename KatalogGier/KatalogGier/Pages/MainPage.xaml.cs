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
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        int iter = 0;
        public Uzytkownik zalogowanyUzytkownik { get; set; }
        public List<Gra> listaGier { get; set; }
        public MainPage()
        {
            InitializeComponent();
            listaGier = DbConnection.GetGraList();
        }
        public MainPage(Uzytkownik uzytkownik)
        {
            InitializeComponent();
            listaGier = DbConnection.GetGraList();
            zalogowanyUzytkownik = uzytkownik;
        }

        private void Loader(object sender, RoutedEventArgs e)
        {
            searchType.SelectedIndex = 0;
            gameList.ItemsSource = listaGier;
            nazwaUzytkownika.Content = zalogowanyUzytkownik.GetWyswietlana_nazwa();
        }
        private void Search(object sender, MouseButtonEventArgs e)
        {
            string szukanaFraza = searchTextBox.Text;
            List<Gra> przefiltrowaniaListaGier = null;
            przefiltrowaniaListaGier = listaGier.FindAll((x) =>
            {
                var item = searchType.SelectedItem as ComboBoxItem;
                if (item == null)
                {
                    if (x.Nazwa.Contains(szukanaFraza) || x.GetTyp().ToLower().Contains(szukanaFraza.ToLower()) || (x.GetSrednia_ocena() + "").ToLower().Contains(szukanaFraza.ToLower()))
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.Name == "Nazwa" && x.Nazwa.ToLower().Contains(szukanaFraza.ToLower()))
                    {
                        return true;
                    }
                    else if(item.Name=="Typ" && x.GetTyp().ToLower().Contains(szukanaFraza.ToLower()))
                    {
                        return true;
                    }
                    else if(item.Name=="Ocena" && (x.GetSrednia_ocena()+"").ToLower().Contains(szukanaFraza.ToLower()))
                    {
                        return true;
                    }
                }
                return false;
            });
            ListViewUpdateSource(przefiltrowaniaListaGier);
        }
        private void ListViewUpdateSource(List<Gra> lista)
        {
            gameList.ItemsSource = null;
            gameList.ItemsSource = lista;
        }
        private void searchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var selectedItem = combo.SelectedItem as ComboBoxItem;
            if(selectedItem.Name == "Nazwa")
            {
                listaGier.Sort((x, y) => String.Compare(x.Nazwa, y.Nazwa));
            }else if(selectedItem.Name == "Typ")
            {
                listaGier.Sort((x, y) => String.Compare(x.GetTyp(), y.GetTyp()));
            }
            else
            {
                listaGier.Sort((x, y) =>
                {
                    if (x.GetSrednia_ocena() >= y.GetSrednia_ocena())
                    {
                        return 1;
                    }
                    else { return -1; }
                });
            }
            ListViewUpdateSource(listaGier);
        }

        private void Logout(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Logowanie(zalogowanyUzytkownik.GetLogin(), zalogowanyUzytkownik.GetHaslo());
        }

        private void Ustawienia(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Ustawienia(zalogowanyUzytkownik,listaGier.Count);
        }

        private void WybranaGra(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var game_id = Int32.Parse(grid.Tag.ToString());
            Gra wybranaGra =  DbConnection.GetGraById(game_id);
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new OcenianieGier(wybranaGra,zalogowanyUzytkownik,listaGier.Count);
        }
    }
}
