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

        public double GetDiskCapacity()
        {
            string query = "SELECT * FROM Win32_DiskDrive";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject obj in results)
            {
                ulong capacityBytes = (ulong)obj["Size"];
                double capacityGB = capacityBytes / (1024 * 1024 * 1024); // Convertir en gigaoctets

                
                return capacityGB;
            }

            return 0.0000;
        }

        /*public double GetFreeDiskCapacity()
        {
            string query = "SELECT * FROM Win32_LogicalDisk";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            Dictionary<string, string> freeCapacities = new Dictionary<string, string>();
            double freeCapacityGB = 0;

            foreach (ManagementObject obj in results)
            {
                string driveLetter = obj["DeviceID"].ToString()!;
                ulong freeSpaceBytes = (ulong)obj["FreeSpace"];

                double freeSpaceGB = freeSpaceBytes / (1024 * 1024 * 1024); // Convertir en gigaoctet

                freeCapacities.Add(driveLetter, freeSpaceGB.ToString("F2"));
            }

            foreach (string disk in freeCapacities.Values)
            {
                freeCapacityGB += Double.Parse(disk);
            }

            return freeCapacityGB;
        }*/

        // Pour obtenir la marque du poste
        public string GetComputerManufacturer()
        {
            string manufacturer = string.Empty;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Manufacturer FROM Win32_ComputerSystem");
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject obj in results)
            {
                manufacturer = obj["Manufacturer"].ToString()!;
                break;
            }

            return manufacturer;
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

        public string GetMACAddress()
        {
            string macAddress = string.Empty;
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in interfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    macAddress = networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddress;
        }
    }
}
