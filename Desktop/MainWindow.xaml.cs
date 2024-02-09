using Desktop.Infrastructure.Services;
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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DeviceInfoService _deviceInfoService;

        public MainWindow()
        {
            InitializeComponent();

            _deviceInfoService = new DeviceInfoService();

            txt_b_os.Text = _deviceInfoService.GetOperatingSystem();

            int FreeSpace = 80;
            int UsedSpace = 20;

        }


    }
}