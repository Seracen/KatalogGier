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
    /// Logika interakcji dla klasy Rejestracja.xaml.
    /// </summary>
    public partial class Rejestracja : Page
    {
        /// <summary>
        /// Gets or sets the Login.
        /// </summary>
        internal string Login { get; set; }

        /// <summary>
        /// Gets or sets the haslo.
        /// </summary>
        internal string haslo { get; set; }

        /// <summary>
        /// Gets or sets the nazwa.
        /// </summary>
        internal string nazwa { get; set; }

        /// <summary>
        /// Defines the iter.
        /// </summary>
        internal int iter = 0;

        /// <summary>
        /// Defines the dispatcherTimer.
        /// </summary>
        internal DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rejestracja"/> class.
        /// </summary>
        public Rejestracja()
        {
            InitializeComponent();
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
        /// The Button_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void Zarejestruj(object sender, RoutedEventArgs e)
        {
            Login = loginTextBox.Text;
            haslo = hasloTextBox.Text;
            nazwa = nazwaTextBox.Text;
            Uzytkownik registeredUser;
            if (haslo != powtorHasloTextBox.Text)
            {
                MessageBox.Show("Błędne haslo!");
            }
            else if (Login != "" && haslo != "" && nazwa != "")
            {
                DbConnection.RegisterUser(Login, haslo, nazwa);
                dispatcherTimer.Stop();
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Logowanie(Login, haslo);
            }
        }

        /// <summary>
        /// The Label_MouseLeftButtonUp.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="MouseButtonEventArgs"/>.</param>
        private void Logowanie(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new Logowanie();
        }
    }
}
