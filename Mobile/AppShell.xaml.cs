using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;

namespace Mobile
{
    public partial class AppShell : Shell
    {
        UserSessionRepository userSR;
        UserSession userSession;

        public AppShell()
        {
            InitializeComponent();

            userSR = new UserSessionRepository();

            GetUserSession();
        }

        public async void GetUserSession()
        {
            var res = await userSR.GetUserSession();
            if (res != null)
            {
                userSession = new UserSession();
                userSession = res;

                txt_user_name.Text = userSession.Name;
                txt_user_role.Text = userSession.Role;
            }
        }

        public async void Logout(object sender, EventArgs e)
        {
            if (userSession != null)
            {
                await userSR.DeleteUserSession(userSession);
                //await Navigation.PushAsync(new MainPage());
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
        }
    }
}
