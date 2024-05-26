using Mobile.ViewModels;
using Mobile.Services;
using Shareds.Modeles;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Mobile.Domain.Repositories;
using Mobile.Domain.Entities;
using Newtonsoft.Json;
using System.Security.Claims;
using UserSession = Mobile.Domain.Entities.UserSession;
using System.IdentityModel.Tokens.Jwt;

namespace Mobile.Views;

public partial class vLogin : ContentPage
{
	public LoginViewModel LoginVM;

	public UserService userService;
    public ParkService parkService;

    public UserSessionRepository userSR;
    public ParcSessionRepository parcSR;

    public vLogin()
	{
		InitializeComponent();

		LoginVM = (LoginViewModel)BindingContext;

		userService = new UserService();
        parkService = new ParkService();

        userSR = new UserSessionRepository();
        parcSR = new ParcSessionRepository();
    }

    // Pour afficher une notification toast
    private async void DisplayToat(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(message, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }

    private async void Login(object sender, EventArgs e)
	{
        UserLoginModele loginM = new UserLoginModele();
        loginM.Email = LoginVM.Email;
        loginM.Password = LoginVM.Password;

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(loginM, serviceProvider: null, items: null);

        bool isValid = Validator.TryValidateObject(loginM, validationContext, validationResults, validateAllProperties: true);
        
        if (isValid)
        {
            try
            {
                var res = await userService.Login(loginM);
                DisplayToat(res.Message);

                if (res.Flag)
                {
                    UserSession userSession = new UserSession();
                    userSession = SetUserSession(res.Token);
                    await userSR.CreateUserSession(userSession);

                    var userParcs = await parkService.GetAllByUser(userSession.Id, userSession.Token);
                    if (userParcs.Count > 1)
                    {
                        LoginVM.IsBorderOneVisible = false;
                        LoginVM.IsBorderTwoVisible = true;
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(vDashboard)}");
                    }                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }

    // Récupère le Token à la connexion et crée une userSession
    public UserSession SetUserSession(string token)
    {
        UserSession userSession = new UserSession();

        if (token is not null)
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

                userSession = new UserSession()
                {
                    Id = int.Parse(id),
                    Name = name,
                    Role = role,
                    Token = token,
                };
                string jsonUserS = JsonConvert.SerializeObject(userSession);
            }
        }

        return userSession;
    }
}