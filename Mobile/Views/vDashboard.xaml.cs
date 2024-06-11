using Mobile.Domain.Repositories;
using Mobile.Services;
using Mobile.ViewModels;

namespace Mobile.Views;

public partial class vDashboard : ContentPage
{
	UserService userService;
    ParkService parkService;
    RoomService roomService;
    PostService postService;

    DashboardViewModel DashbordVM;

    //UserSessionRepository userSR;
    //ParcSessionRepository parcSR;

    public vDashboard()
	{
		InitializeComponent();

		userService = new UserService();
        parkService = new ParkService();
        roomService = new RoomService();
        postService = new PostService();

        DashbordVM = (DashboardViewModel)BindingContext;
        DashbordVM.IsLabelVisible = true;
        DashbordVM.IsContentVisible = false;

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

            var userSession = userService.GetUserPreferences();
            var parkSession = parkService.GetParkPreferences();

            if (userSession != null && parkSession != null)
            {
                await Task.Delay(1000);
                var rooms = await roomService.GetAllByParc(parkSession.ParcId, userSession.Token);
                var postes = await postService.GetAllByParc(parkSession.ParcId, userSession.Token);
                var users = await userService.GetAllByParc(parkSession.ParcId);

                if (rooms != null || postes != null || users != null)
                {
                    DashbordVM.IsLabelVisible = false;
                    DashbordVM.IsContentVisible = true;
                }
                else
                {
                    lbl_loading.Text = "Aucun élément à afficher.";
                }

                lb_nbr_room.Text = rooms.Count.ToString();
                if (rooms.Count > 1)
                {
                    lb_room.Text = "Salles";
                }

                lb_nbr_post.Text = postes.Count.ToString();
                if (postes.Count > 1)
                {
                    lb_post.Text = "Postes";
                }

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