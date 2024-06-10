using Mobile.Services;
using Mobile.ViewModels;

namespace Mobile.Views.Parks;

public partial class vParks : ContentPage
{
	UserService userService;
	ParkService parkService;

	ParkViewModel ParkVM;

	public vParks()
	{
		InitializeComponent();

		userService = new UserService();
		parkService = new ParkService();

		ParkVM = (ParkViewModel)BindingContext;
        ParkVM.IsLabelVisible = true;
        ParkVM.IsContentVisible = false;

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
                await Task.Delay(1000);
                var park = await parkService.GetById(parkSession.ParcId, userSession.Token);
                if (park != null)
                {
					ParkVM._Nom = park.Nom;
                    ParkVM._Telephone = park.Telephone;
                    ParkVM._Email = park.Email;
                    ParkVM._Pays = park.Pays;
                    ParkVM._CodePostal = park.CodePostal;
                    ParkVM._Ville = park.Ville;
                    ParkVM._NumeroRue = park.NumeroRue;
                    ParkVM._LibelleRue = park.LibelleRue;
                    ParkVM._DateCreation = park.DateCreation;

                    ParkVM.IsLabelVisible = false;
                    ParkVM.IsContentVisible = true;
                }
                else
                {
                    lbl_loading.Text = "Merci de vous rendre sur notre plateforme web pour créer votre premier établissement.";
                }
            }
        }
		catch(Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}