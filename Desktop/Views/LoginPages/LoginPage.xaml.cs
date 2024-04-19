
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using Shareds.Modeles;
using Desktop.Services;
using Desktop.ViewModels;

namespace Desktop.Views.LoginPages
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Login loginW;
        private readonly UserService _userService;

        public LoginPage(Login loginW)
        {
            InitializeComponent();
            this.loginW = loginW;

            _userService = new UserService();
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

                    Thread.Sleep(300);
                    MainWindow mainWindow = new MainWindow();
                    //CloseLoginWindows();
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
