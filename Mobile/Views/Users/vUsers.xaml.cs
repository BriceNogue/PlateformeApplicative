using Mobile.Services;
using Mobile.ViewModels;
using Shareds.Modeles;

namespace Mobile.Views.Users;

public partial class vUsers : ContentPage
{
    UserService userService;
    ParkService parkService;
    TypeService typeService;

    UserViewModel UserVM;

    public vUsers()
	{
		InitializeComponent();

        userService = new UserService();
        parkService = new ParkService();
        typeService = new TypeService();

        UserVM = (UserViewModel)BindingContext;
        UserVM.IsLabelVisible = true;
        UserVM.IsContentVisible = false;

        _ = GetAllUsers();
    }

    public async Task GetAllUsers()
    {
        try
        {
            var userSession = userService.GetUserPreferences();
            var parkSession = parkService.GetParkPreferences();

            if (userSession != null && parkSession != null)
            {
                List<UserModele> usersToDisplay = new();

                await Task.Delay(1000);
                var users = await userService.GetAllByParc(parkSession.ParcId);
                var types = await typeService.GetAll();

                foreach (var type in types)
                {
                    foreach (var user in users)
                    {
                        if (user.IdType == type.Id)
                        {
                            user.UserRole = type.Libelle;
                            usersToDisplay.Add(user);
                        }
                    }
                }

                if (usersToDisplay != null)
                {
                    UserVM.Users = usersToDisplay;

                    UserVM.IsLabelVisible = false;
                    UserVM.IsContentVisible = true;
                }
                else
                {
                    lbl_loading.Text = "Merci de vous rendre sur notre plateforme web pour créer des utilisateurs.";
                }
            }
            else
            {
                lbl_loading.Text = "Merci de vous rendre sur notre plateforme web pour créer un établissement.";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}