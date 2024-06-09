using Mobile.Domain.Repositories;
using Mobile.Services;
using Mobile.Views;

namespace Mobile
{
    public partial class MainPage : ContentPage
    {
        //UserSessionRepository userSR;

        UserService userService;

        public MainPage()
        {
            InitializeComponent();

            //userSR = new UserSessionRepository();
            userService = new UserService();

            _ = CheckUserSession();
        }

        public async Task<bool> CheckUserSession()
        {
            //var res = await userSR.GetUserSession();

            var res = userService.GetUserPreferences();

            if (res == null)
            {
                return false;
            }
            else
            {
                await Shell.Current.GoToAsync("//vDashboard");
                return true;
            }
        }

        private async void GoToLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vLogin());

            //SemanticScreenReader.Announce("");
        }

        private async void GoToRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vRegister());
        }
    }

}
