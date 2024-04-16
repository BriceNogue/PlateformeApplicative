using Desktop.Services;
using Desktop.ViewModels;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using PosteServiceDekstop = Desktop.Infrastructure.Services.PosteService;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour DeviceInfoPage.xaml
    /// </summary>
    public partial class DeviceInfoPage : Page
    {
        private readonly DeviceInfoService _deviceInfoService;

        //private PosteServiceDekstop _posteServiceD;

        private PosteLoginModele _loginModele;

        public DeviceViewModel deviceViewModel = new DeviceViewModel();

        //public PosteModele posteModele = new PosteModele();

        public DeviceInfoPage()
        {
            InitializeComponent();

            //_posteServiceD = new PosteServiceDekstop();
            _deviceInfoService = new DeviceInfoService();
            _loginModele = new PosteLoginModele();

            DataContext = deviceViewModel;

            //GetPost();
            DisplayPost();
        }

        /*private async void GetPost()
        {
            PosteModele poste = await _posteServiceD.GetOne();

            if (poste != null)
                this.posteModele = poste;
        }*/

        private void DisplayPost()
        {
            double total = _deviceInfoService.GetDiskCapacity();
            //double used = _deviceInfoService.GetFreeDiskCapacity();
            //double free = total - used;

            deviceViewModel.TotalROMSpace = total;
            //deviceViewModel.UsedROMSpace = used;
            //deviceViewModel.FreeROMSpace = free;

            txt_adr_ip.Text = _deviceInfoService.GetIPAddress();
            txt_adr_mac.Text = _deviceInfoService.GetMACAddress();
            txt_nom_poste.Text = _deviceInfoService.GetMachineName();
            txt_marque.Text = _deviceInfoService.GetComputerManufacturer();
            txt_os.Text = _deviceInfoService.GetOperatingSystem();
            txt_processeur.Text = _deviceInfoService.GetProcessor();
        }

    }
}
