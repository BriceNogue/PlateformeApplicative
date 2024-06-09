using Mobile.Domain.Repositories;
using Mobile.Services;

namespace Mobile.Views;

public partial class vDashboard : ContentPage
{
	UserService userService;
    ParkService parkService;

    //UserSessionRepository userSR;
    //ParcSessionRepository parcSR;

    public vDashboard()
	{
		InitializeComponent();

		userService = new UserService();
        parkService = new ParkService();

        //userSR = new UserSessionRepository();
        //parcSR = new ParcSessionRepository();

        _ = GetSessionInfo();
        _ = GetParcDetails();
    }

    public async Task GetSessionInfo()
    {
        /*var userS = await userSR.GetUserSession();
        var parcS = await parcSR.Get();

        if (userS != null)
        {
            UserService.userSession = new Domain.Entities.UserSession();
            UserService.userSession = userS;
        }

        if (parcS != null)
        {
            ParkService.parcSession = new Domain.Entities.ParcSession();
            ParkService.parcSession = parcS;
        }*/
    
    }

    public async Task GetParcDetails()
	{
		try
		{
            //var pakcS = await parcSR.Get();

            var parkS = parkService.GetParkPreferences();

            if (parkS != null)
            {
                var users = await userService.GetAllByParc(parkS.ParcId);

                lb_nbr_user.Text = users.Count.ToString();
                if (users.Count > 1)
                {
                    lb_user.Text = "Utilisateurs";
                }
            }
        }
        catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}