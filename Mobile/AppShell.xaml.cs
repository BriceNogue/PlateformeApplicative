using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;
using Mobile.Services;

namespace Mobile
{
    public partial class AppShell : Shell
    {
        UserSessionRepository userSR;
        ParcSessionRepository parcSR;

        UserSession userSession;
        ParcSession parcSession;

        public AppShell()
        {
            InitializeComponent();

            userSR = new UserSessionRepository();
            parcSR = new ParcSessionRepository();

            GetSessionInfo();
        }

        public void GetSessionInfo()
        {
            if (UserService.userSession != null)
            {
                txt_user_name.Text = UserService.userSession.Name;
                txt_user_role.Text = UserService.userSession.Role;
            }

            if (ParkService.parcSession != null)
            {
                parcSession = ParkService.parcSession;
            }
        }

        public async void Logout(object sender, EventArgs e)
        {
            parcSession = await parcSR.Get();
            userSession = await userSR.GetUserSession();

            if (parcSession != null)
            {
                await parcSR.Delete();
            }

            if (userSession != null)
            {
                await userSR.DeleteUserSession();
            }

            parcSession = await parcSR.Get();
            userSession = await userSR.GetUserSession();

            if (parcSession == null && userSession == null)
            {
                //await Navigation.PushAsync(new MainPage());
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }         
        }
    }
}
