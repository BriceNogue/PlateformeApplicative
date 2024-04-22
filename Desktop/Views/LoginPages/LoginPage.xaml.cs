
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using Shareds.Modeles;
using Desktop.Services;
using Desktop.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Desktop.Presentation.Views;

namespace Desktop.Views.LoginPages
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Login loginW;
        private readonly UserService _userService;
        private readonly PosteService _posteService;
        private ParcService parcService;

        public List<EtablissementModele> userParcs = new();
        private int parcId = 0;

        public LoginPage(Login loginW)
        {
            InitializeComponent();
            this.loginW = loginW;

            _userService = new UserService();
            _posteService = new PosteService();
            parcService = new ParcService();

            DataContext = new LoginViewModel();
        }

        private async void login(object sender, RoutedEventArgs e)
        {
            UserLoginModele loginM = new UserLoginModele();
            loginM.Email = txt_b_email.Text;
            loginM.Password = txt_b_password.Password;

            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
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

                    int userId = GetUserId(res.Token!);
                    userParcs = await GetUserParcs(userId);

                    bool isPostExist = await CheckExistPost();

                    if (userParcs.Count > 1 && !isPostExist)
                    {
                        loginW.LoadLoginPages.Navigate(new SelectParcPage(userParcs, loginW));
                    }
                    else
                    {
                        _userService.SetUserSession();
                        if (userParcs.Count != 0)
                        {
                            await parcService!.SetParcSession(userParcs[0].Id);
                        }
                        Thread.Sleep(300);
                        //MainWindow mainWindow = new MainWindow();
                        loginW.Close();
                        //mainWindow.Show();
                    }
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

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            loginW.Close();
        }

        public int GetUserId(string token)
        {
            int userId = 0;
            string userToken = "";
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
                userToken = token;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDecoded = tokenHandler.ReadJwtToken(userToken);

            var claims = tokenDecoded.Claims;

            if (claims is not null)
            {
                string id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

                userId = int.Parse(id);
            }

            return userId;
        }

        public Task<List<EtablissementModele>> GetUserParcs(int id)
        {
            var res = parcService!.GetAllByUser(id);
            return res;
        }

        private async Task<bool> CheckExistPost()
        {
            var res = await _posteService.GetOne();
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
