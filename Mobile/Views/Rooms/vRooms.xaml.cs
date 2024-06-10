using Mobile.Services;
using Mobile.ViewModels;
using Shareds.Modeles;

namespace Mobile.Views.Rooms;

public partial class vRooms : ContentPage
{
    UserService userService;
    ParkService parkService;
    RoomService roomService;

    RoomViewModel RoomVM;

    public vRooms()
	{
		InitializeComponent();

        userService = new UserService();
        parkService = new ParkService();
        roomService = new RoomService();

        RoomVM = (RoomViewModel)BindingContext;
        RoomVM.IsLabelVisible = true;
        RoomVM.IsContentVisible = false;

        _ = GetAllRooms();
    }

    public async Task GetAllRooms()
    {
        try
        {
            var userSession = userService.GetUserPreferences();
            var parkSession = parkService.GetParkPreferences();

            if (userSession != null && parkSession != null)
            {
                var rooms = await roomService.GetAllByParc(parkSession.ParcId, userSession.Token);

                if (rooms != null)
                {
                    RoomVM.Rooms = rooms;

                    RoomVM.IsLabelVisible = false;
                    RoomVM.IsContentVisible = true;
                }
                else
                {
                    lbl_loading.Text = "Merci de vous rendre sur notre plateforme web pour créer des salles.";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}