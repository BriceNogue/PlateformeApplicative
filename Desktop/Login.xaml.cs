using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;
using Desktop.Services;
using Desktop.Views.LoginPages;

namespace Desktop
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); // Pour la gestion des themes

        //public LoginViewModel loginVM;

        public Login()
        {
            InitializeComponent();

            LoadLoginPages.Navigate(new LoginPage(this));
        }

        public void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Pour pouvoir deplacer la fenetre avec la souris
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
