using Mobile.ViewModels;
using Mobile.Services;
using Shareds.Modeles;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using UserSession = Mobile.Domain.Entities.UserSession;
using ParcSession = Mobile.Domain.Entities.ParcSession;

namespace Mobile.Views;

public partial class vLogin : ContentPage
{
	public LoginViewModel LoginVM;

	public UserService userService;
    public ParkService parkService;

    //public UserSessionRepository userSR;
    //public ParcSessionRepository parcSR;

    public UserSession userSession;
    public ParcSession parcSession;
    private string userToken;

    public vLogin()
	{
		InitializeComponent();

		LoginVM = (LoginViewModel)BindingContext;
        LoginVM.IsBorderOneVisible = true;
        LoginVM.IsBorderTwoVisible = false;

        userService = new UserService();
        parkService = new ParkService();

        //userSR = new UserSessionRepository();
        //parcSR = new ParcSessionRepository();
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
                    userSession = new UserSession();
                    userSession = userService.SetUserSession(res.Token);

                    var userParcs = await parkService.GetAllByUser(userSession.UserId, userSession.Token);
                    if (userParcs.Count > 1)
                    {
                        LoginVM.Parks = userParcs;
                        LoginVM.IsBorderOneVisible = false;
                        LoginVM.IsBorderTwoVisible = true;

                        userToken = res.Token;
                    }
                    else
                    {
                        //await userSR.CreateUserSession(userSession);
                        
                        userService.SaveUserPreferences(res.Token);

                        var park = userParcs.FirstOrDefault();
                        if (park != null)
                        {
                            ParcSession parkSession = new ParcSession()
                            {
                                Id = park.Id,
                                ParcId = park.Id,
                                Name = park.Nom
                            };

                            parkService.SaveParkPreferences(parkSession);
                        }

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

    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        EtablissementModele parc = ((Picker)sender).SelectedItem as EtablissementModele;

        if (parc != null)
        {
            parcSession = new ParcSession();
            parcSession.Id = parc.Id;
            parcSession.ParcId = parc.Id;
            parcSession.Name = parc.Nom;

            DisplayToat($"{parcSession.Name} {parcSession.ParcId}");
        }
    }

    private async void OnValidSelectedPark(object sender, EventArgs e)
    {
        if (parcSession != null && !string.IsNullOrEmpty(userToken))
        {
            //await parcSR.Create(parcSession);
            //await userSR.CreateUserSession(userSession);

            userService.SaveUserPreferences(userToken);
            parkService.SaveParkPreferences(parcSession);
            await Shell.Current.GoToAsync($"//{nameof(vDashboard)}");
        }
        else
        {
            DisplayToat("Veillez sélectionner un établissement.");
        }
    }

    private void OnCancelSelectedPark(object sender, EventArgs e)
    {
        LoginVM.IsBorderOneVisible = true;
        LoginVM.IsBorderTwoVisible = false;
    }
}