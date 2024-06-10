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
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}