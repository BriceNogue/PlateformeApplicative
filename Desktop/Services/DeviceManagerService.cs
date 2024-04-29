using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Desktop.Services
{
    public class DeviceManagerService
    {
        // Pour la difusion d'ecran
        private Timer? _timer;
        private int _interval;
        private Screen? _screen;
        private Action<Image>? _callback;

        public DeviceManagerService() { }

        // Constructeur pour initialiser la capture d'écran
        public DeviceManagerService(int interval, Action<Image> callback) 
        {
            _interval = interval;
            _callback = callback;
        }

        #region /// Screen capture

        public void StartCapture()
        {
            _screen = Screen.PrimaryScreen;
            _timer = new Timer(CaptureScreen!, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_interval));
        }

        public void StopeCapture()
        {
            _timer?.Dispose();
        }

        private void CaptureScreen(object state)
        {
            var bitmap = new Bitmap(_screen!.Bounds.Width, _screen.Bounds.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(_screen.Bounds.X, _screen.Bounds.Y, 0, 0, _screen.Bounds.Size);
            }

            _callback?.Invoke(bitmap);
        }

        #endregion

        public void ShutDown()
        {
            Process.Start("shutdown", "/s /t 0");
        }
    }
}
