using Mobile.Services;

namespace Mobile.Views;

public partial class vDashboard : ContentPage
{
	UserService userService;
	public vDashboard()
	{
		InitializeComponent();

		userService = new UserService();
		_ = GetParcDetails();
    }

	public async Task GetParcDetails()
	{
		try
		{
            var users = await userService.GetAllByParc(1);

            lb_nbr_user.Text = users.Count.ToString();
            if (users.Count > 1)
			{
                lb_user.Text = "Utilisateurs";
            }
        }
        catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}