using Mobile.Views.Users;

namespace Mobile.Views;

public partial class vLogin : ContentPage
{
	public vLogin()
	{
		InitializeComponent();
	}

	private async void Login(object sender, EventArgs e)
	{		
		await Shell.Current.GoToAsync("//Dashboard");
	}
}