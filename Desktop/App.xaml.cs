using Desktop.Infrastructure.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private PosteService _posteService;

        public App()
        {
            _posteService = new PosteService();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Console.WriteLine("Poste service: " + _posteService.Get());

        }
    }

}
