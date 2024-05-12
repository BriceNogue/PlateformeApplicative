using Mobile.Domain.Repositories;
using Mobile.Views;

namespace Mobile
{
    public partial class MainPage : ContentPage
    {
        UserSessionRepository userSR;
        public MainPage()
        {
            InitializeComponent();

            userSR = new UserSessionRepository();

            _ = CheckUserSession();
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

        public async Task<bool> CheckUserSession()
        {
            var res = await userSR.GetUserSession();

            if (res == null)
            {
                return false;
            }
            else
            {
                await Shell.Current.GoToAsync("//Dashboard");
                return true;
            }
        }
    }

}
