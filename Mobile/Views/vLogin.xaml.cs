using Mobile.ViewModels;
using Mobile.Services;
using Shareds.Modeles;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mobile.Views;

public partial class vLogin : ContentPage
{
	public LoginViewModel LoginVM;

	public UserService userService;

	public vLogin()
	{
		InitializeComponent();

		LoginVM = (LoginViewModel)BindingContext;

		userService = new UserService();
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
        DisplayToat("XXXX");
        if (isValid)
        {
            try
            {
                var res = await userService.Login(loginM);
                DisplayToat(res.Message);

                if (res.Flag)
                {
                    await Shell.Current.GoToAsync("//Dashboard");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}