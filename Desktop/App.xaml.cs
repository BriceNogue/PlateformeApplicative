
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
            
            MainWindow mainWindow = new MainWindow();

            Thread.Sleep(splashTime);
            
            splashScreen.Close();
            mainWindow.Show();
        }
    }

}
