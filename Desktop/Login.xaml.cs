using Desktop.Infrastructure.Services;
using MaterialDesignThemes.Wpf;
using Shareds.Modeles;
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
using System.Windows.Shapes;
using static Shareds.Modeles.ResponsesModels;

namespace Desktop
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); // Pour la gestion des themes
        private readonly UserService _userService;

        public static UserSession userSession = new UserSession (null, null, null, null);

        public Login()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
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

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private async void login(object sender, RoutedEventArgs e)
        {
            UserLoginModele loginM = new UserLoginModele();
            var res = await _userService.Login(loginM);

            if (!res.Flag)
            {
                MessageBoxResult messageBox = MessageBox.Show(res.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Thread.Sleep(300);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
