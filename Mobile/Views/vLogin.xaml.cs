using Mobile.ViewModels;
using Mobile.Views.Users;

namespace Mobile.Views;

public partial class vLogin : ContentPage
{
	public LoginViewModel LoginVM;
	public vLogin()
	{
		InitializeComponent();

		LoginVM = (LoginViewModel)BindingContext;
	}

	private async void Login(object sender, EventArgs e)
	{		
		await Shell.Current.GoToAsync("//Dashboard");
	}
}