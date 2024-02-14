using Desktop.Infrastructure.Services;
using Shared.Modeles;
using Infrastructure.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PosteServiceDekstop = Desktop.Infrastructure.Services.PosteService;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DeviceInfoService _deviceInfoService;

        private PosteServiceDekstop _posteServiceD;

        private PosteLoginModele _loginModele;


        public MainWindow()
        {
            InitializeComponent();

            _posteServiceD = new PosteServiceDekstop();

            _deviceInfoService = new DeviceInfoService();
            

            //txt_b_os.Text = _deviceInfoService.GetOperatingSystem();

            int FreeSpace = 80;
            int UsedSpace = 20;

            txt_b_adr_ip.Text = _deviceInfoService.GetIPAddress();
            txt_b_adr_mac.Text = _deviceInfoService.GetMACAddress();
            txt_b_nom_poste.Text = _deviceInfoService.GetMachineName();
            txt_b_marque.Text = _deviceInfoService.GetComputerManufacturer();

            GetPost();

        }

        private async void GetPost()
        {
            PosteModele poste = await _posteServiceD.GetOne();

            if (poste != null)
            {
                Console.WriteLine("Poste service: " + poste.LibellePoste);
                txt_b_os.Text = poste.LibellePoste;
            }
        }


    }
}