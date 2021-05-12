namespace KatalogGier.Pages
{
    using KatalogGier.Classes;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    /// <summary>
    /// Logika interakcji dla klasy Logowanie.xaml.
    /// </summary>
    public partial class Logowanie : Page
    {
        /// <summary>
        /// Sets the Login.
        /// </summary>
        public string Login { private get; set; }

        /// <summary>
        /// Sets the Haslo.
        /// </summary>
        public string Haslo { private get; set; }

        /// <summary>
        /// Defines the iter.
        /// </summary>
        internal int iter = 0;

        /// <summary>
        /// Defines the dispatcherTimer.
        /// </summary>
        internal DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logowanie"/> class..
        /// </summary>
        internal Uzytkownik zalogowanyUzytkownik;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logowanie"/> class.
        /// </summary>
        public Logowanie()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logowanie"/> class.
        /// </summary>
        /// <param name="l">The l<see cref="string"/>.</param>
        /// <param name="h">The h<see cref="string"/>.</param>
        public Logowanie(string l, string h)
        {
            Login = l;
            Haslo = h;
            InitializeComponent();
            loginTextBox.Text = l;
            hasloKontrolka.haslo = h;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// The Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Tick(object sender, EventArgs e)
        {
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            switch (iter)
            {
                case 0: { bi.UriSource = new Uri("/Icons/1.jpg", UriKind.Relative); iter++; break; }
                case 1: { bi.UriSource = new Uri("/Icons/8.jpg", UriKind.Relative); iter++; break; }
                case 2: { bi.UriSource = new Uri("/Icons/10.jpg", UriKind.Relative); iter = 0; break; }
            }
            bi.EndInit();
            MainIcon.Source = bi;
        }

        /// <summary>
        /// The Label_MouseLeftButtonUp.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="MouseButtonEventArgs"/>.</param>
        private void Rejestracja(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Rejestracja();
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// The Button_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void Zaloguj(object sender, RoutedEventArgs e)
        {

            Login = loginTextBox.Text;
            Haslo = hasloKontrolka.haslo;

            zalogowanyUzytkownik = DbConnection.VerifyUser(Login, Haslo);

            if (zalogowanyUzytkownik == null)
            {
                MessageBox.Show("Błędne dane!");
            }
            else
            {
                dispatcherTimer.Stop();
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new MainPage(zalogowanyUzytkownik);
            }
        }
    }
}
