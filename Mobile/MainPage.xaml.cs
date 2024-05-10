using Mobile.Views;

namespace Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
