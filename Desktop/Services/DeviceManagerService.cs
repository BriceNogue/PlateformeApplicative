using System.Diagnostics;

namespace Desktop.Services
{
    public class DeviceManagerService
    {
        public DeviceManagerService() { }

        public void ShutDown()
        {
            Process.Start("shutdown", "/s /t 0");
        }

    }
}
