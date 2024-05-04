using Shareds.Modeles;
using System.Windows;
using System.Windows.Controls;
using Desktop.Services;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour PostIndexPage.xaml
    /// </summary>
    public partial class PostIndexPage : Page
    {
        PosteModele posteModele = new PosteModele();

        private PosteService posteServiceD;
        private DeviceManagerService deviceMS;
        private DeviceInfoService deviceIS;

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public PostIndexPage()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(GetProcessorUsage!);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();

            posteServiceD = new PosteService();
            deviceMS = new DeviceManagerService();
            deviceIS = new DeviceInfoService();

            _ = GetPost();
        }

        private async Task GetPost()
        {
            PosteModele poste = await posteServiceD.GetOne();
            if (poste is null)
            {       
                txt_post.Text = "Inconnu...";
            }
            {
                this.posteModele = poste!;
                txt_post.Text = poste?.Numero.ToString();
            }
        }

        private void GetProcessorUsage(object sender, EventArgs e)
        {
            rpbCPU.Value = (int)deviceIS.GetUsedRAM();
            rpbCPU.PercentText = deviceIS.GetUsedRAM().ToString();
        }
    }
}
