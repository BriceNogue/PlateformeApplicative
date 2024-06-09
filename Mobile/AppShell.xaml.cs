using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;
using Mobile.Services;

namespace Mobile
{
    public partial class AppShell : Shell
    {
        //UserSessionRepository userSR;
        //ParcSessionRepository parcSR;

        UserService userService;
        ParkService parkService;

        UserSession userSession;
        ParcSession parcSession;

        public AppShell()
        {
            InitializeComponent();

            //userSR = new UserSessionRepository();
            //parcSR = new ParcSessionRepository();

            userService = new UserService();
            parkService = new ParkService();

            PropertyChanged += OnFlyoutOpened;
        }

        // Evénement a l'ouverture du flyout
        private void OnFlyoutOpened(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "FlyoutIsPresented")
            {
                if (FlyoutIsPresented)
                {
                    GetSessionInfo();
                }
            }
        }

        public void GetSessionInfo()
        {
            /*if (UserService.userSession != null)
            {
                txt_user_name.Text = UserService.userSession.Name;
                txt_user_role.Text = UserService.userSession.Role;
            }

            if (ParkService.parcSession != null)
            {
                parcSession = ParkService.parcSession;
            }*/


            var userS = userService.GetUserPreferences();

            if (userS != null)
            {
                UserSession userSession = new UserSession();
                userSession = userS;

                txt_user_name.Text = userSession.Name;
                txt_user_role.Text = userSession.Role;
            }   
        }

        public async void Logout(object sender, EventArgs e)
        {
            /*parcSession = await parcSR.Get();
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
            }*/
            
            userService.DeleteUserPreferences();
            parkService.DeleteParkPreferences();

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
