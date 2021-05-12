using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KatalogGier.Kontrolki
{

    /// <summary>
    /// Logika interakcji dla klasy HasloKontrolka.xaml
    /// </summary>
    public partial class HasloKontrolka : UserControl
    {
        bool isVisible { get; set; } = false;
        public string haslo { get; set; }
        public HasloKontrolka()
        {
            InitializeComponent();
        }

        private void ChangeVisibility(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            if (!isVisible)
            { 
                bi.UriSource = new Uri("/Icons/hide.png",UriKind.Relative);
                passwordTextBox.Visibility = Visibility.Visible;
                passwordPasswordBox.Visibility = Visibility.Hidden;
                isVisible = true;
            }
            else
            {
                bi.UriSource = new Uri("/Icons/visible.png", UriKind.Relative);
                passwordTextBox.Visibility = Visibility.Hidden;
                passwordPasswordBox.Visibility = Visibility.Visible;
                isVisible = false;
            }
            bi.EndInit();

            VisibilityIcon.Source = bi;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            
            var textBox = sender as TextBox;
            haslo = textBox.Text;
            passwordPasswordBox.Password = haslo;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            haslo = passwordBox.Password;
            passwordTextBox.Text = haslo;
            if(passwordPasswordBox.Password.Length!=0)
            SetSelection(passwordPasswordBox, passwordPasswordBox.Password.Length, 0);
            passwordPasswordBox.Focus();
        }

        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            passwordPasswordBox.Password = haslo;
            passwordTextBox.Text = haslo;
        }
    }
}
