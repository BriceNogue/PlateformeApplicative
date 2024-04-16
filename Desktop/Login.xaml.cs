using MaterialDesignThemes.Wpf;
using Shareds.Modeles;
using System.Windows;
using System.Windows.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Desktop.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Desktop.Services;

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

        //public LoginViewModel loginVM;

        public Login()
        {
            InitializeComponent();
            _userService = new UserService();

            DataContext = new LoginViewModel();
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
            loginM.Email = txt_b_email.Text;
            loginM.Password = txt_b_password.Password;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loginM, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(loginM, validationContext, validationResults, validateAllProperties: true);

            txt_connected.Text = "";

            if (isValid)
            {
                var res = await _userService.Login(loginM);

                if (!res.Flag)
                {
                    MessageBoxResult messageBox = MessageBox.Show(res.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txt_connected.Text = res.Message;

                    Thread.Sleep(300);
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();
                }
            }
            else
            {
                foreach (var result in validationResults)
                {
                    txt_connected.Text = txt_connected.Text + " " + result.ToString();
                }
            }
        }
    }
}
