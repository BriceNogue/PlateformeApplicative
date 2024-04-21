using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int splashTime = 1500; //Milisecondes

        public App()
        {
           
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            
            Login login = new Login();

            Thread.Sleep(splashTime);
            
            splashScreen.Close();
            login.Show();
        }

        private void SetNotificationIcon()
        {
            //Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
        }
    }

}
