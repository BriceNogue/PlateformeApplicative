using Mobile.Services;
using Mobile.ViewModels;

namespace Mobile.Views.Parks;

public partial class vParks : ContentPage
{
	UserService userService;
	ParkService parkService;

	public ParkViewModel ParkVM;

	public vParks()
	{
		InitializeComponent();

		userService = new UserService();
		parkService = new ParkService();

		ParkVM = (ParkViewModel)BindingContext;

        _ = GetParkInfos();
    }

    private async Task GetParkInfos()
	{
		try
		{
            var userSession = userService.GetUserPreferences();
            var parkSession = parkService.GetParkPreferences();

            if (userSession != null && parkSession != null)
            {
                var park = await parkService.GetById(parkSession.ParcId, userSession.Token);
                if (park != null)
                {
					ParkVM.Nom = park.Nom;
                    ParkVM.Telephone = park.Telephone;
                    ParkVM.Email = park.Email;
                    ParkVM.Pays = park.Pays;
                    ParkVM.CodePostal = park.CodePostal;
                    ParkVM.Ville = park.Ville;
                    ParkVM.NumeroRue = park.NumeroRue;
                    ParkVM.LibelleRue = park.LibelleRue;
                    ParkVM.DateCreation = park.DateCreation;
                }
            }
        }
		catch(Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}