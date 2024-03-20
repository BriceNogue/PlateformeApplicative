using Desktop.Infrastructure.Services;
using MaterialDesignThemes.Wpf;
using Shareds.Modeles;
using System.Windows;
using System.Windows.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Desktop.Presentation.ViewModels;

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

        public LoginViewModel loginVM = new LoginViewModel();
        public static UserSession? userSession;

        public Login()
        {
            InitializeComponent();
            _userService = new UserService();

            DataContext = loginVM;
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
            var res = await _userService.Login(loginM);

            if (!res.Flag)
            {
                MessageBoxResult messageBox = MessageBox.Show(res.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SetUserSession(res.Token!);

                Thread.Sleep(300);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private void SetUserSession(string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDecoded = tokenHandler.ReadJwtToken(token);

            // Accès aux revendications (données) du token JWT et èxtraction des infos
            var claims = tokenDecoded.Claims;

            if (claims is not null)
            {
                string name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!;
                string email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
                string role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!;
                string id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

                userSession = new UserSession(int.Parse(id), name, email, role);
            }
        }
    }
}
