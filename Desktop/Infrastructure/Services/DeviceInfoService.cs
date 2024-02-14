using System.Net.NetworkInformation;
using System.Management;

namespace Desktop.Infrastructure.Services
{
    public class DeviceInfoService
    {
        public DeviceInfoService() { }

        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        public string GetOperatingSystem()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject obj in results)
            {
                return obj["Caption"].ToString()!;
            }

            return "N/A";
        }

        public string GetProcessor()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject obj in results)
            {
                return obj["Name"].ToString()!;
            }

            return "N/A";
        }

        public string GetDiskInfo()
        {
            string query = "SELECT * FROM Win32_LogicalDisk";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject obj in results)
            {
                return obj[""].ToString()!;
            }

            return "N/A";
        }

        public string GetIPAddress()
        {
            string ipAddress = string.Empty;
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in interfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                    foreach (UnicastIPAddressInformation ipAddressInfo in ipProperties.UnicastAddresses)
                    {
                        if (ipAddressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = ipAddressInfo.Address.ToString();
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(ipAddress))
                    break;
            }

            return ipAddress;
        }
    }
}
