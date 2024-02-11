using Desktop.Infrastructure.Services;
using Infrastructure.Modeles;
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


        public MainWindow()
        {
            InitializeComponent();

            _posteServiceD = new PosteServiceDekstop();

            _deviceInfoService = new DeviceInfoService();
            

            txt_b_os.Text = _deviceInfoService.GetOperatingSystem();

            int FreeSpace = 80;
            int UsedSpace = 20;

            LoadProducts();

        }

        private async void LoadProducts()
        {
            List<PosteModele> postes = await _posteServiceD.GetAll();

            if (postes != null)
            {
                // Faites quelque chose avec la liste des produits, par exemple, affichez-les dans une ListBox ou une DataGrid
                foreach (PosteModele poste in postes)
                {
                    Console.WriteLine("Poste service: " + poste.LibellePoste);
                }
            }
        }


    }
}