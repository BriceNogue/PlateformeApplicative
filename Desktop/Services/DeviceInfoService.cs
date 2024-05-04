using System.Net.NetworkInformation;
using System.Management;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Microsoft.Win32;
using System.Diagnostics;

namespace Desktop.Services
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

        #region /// Processor

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

        public double GetProcessorClockSpeed()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor");
            ManagementObjectCollection results = searcher.Get();

            double speed = 0;

            foreach (ManagementObject obj in results)
            {
                speed = Convert.ToDouble(obj["CurrentClockSpeed"]!) / 1000;  
            }

            speed = Math.Round(speed, 2);
            return speed;
        }

        public int GetProcessorUsage()
        {
            PerformanceCounter perfCPU = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            int res = (int)perfCPU.NextValue();
            return res;
        }

        #endregion


        public double GetDiskCapacity()
        {
            string query = "SELECT * FROM Win32_DiskDrive";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            double diskCapacity = 0;

            foreach (ManagementObject obj in results)
            {
                ulong capacityBytes = (ulong)obj["Size"];
                diskCapacity = capacityBytes / (1024 * 1024 * 1024); // Convertir en gigaoctets
            }

            return diskCapacity;
        }

        public double GetFreeDiskCapacity()
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
        }

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

        #region /// Network card

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

            /*foreach (NetworkInterface networkInterface in interfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                    byte[] bytes = physicalAddress.GetAddressBytes();
                    macAddress = networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }*/

            foreach (NetworkInterface networkInterface in interfaces)
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                    byte[] bytes = physicalAddress.GetAddressBytes();
                    macAddress = BitConverter.ToString(bytes);

                    break;
                }
            }

            // Formater l'adresse MAC
            /*macAddress = string.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                macAddress.Substring(0, 2),
                macAddress.Substring(2, 2),
                macAddress.Substring(4, 2),
                macAddress.Substring(6, 2),
                macAddress.Substring(8, 2),
                macAddress.Substring(10, 2));*/

            //string formattedMacAddress = Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", "$1:$2:$3:$4:$5:$6");
            string formattedMacAddress = macAddress.Replace("-", ":");

            return formattedMacAddress;
        }

        #endregion

        #region /// Keyboard

        // Recuperer la disponibilitée du clavier
        public string GetKeyboardAvailability()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Keyboard");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                int keyboardStatus = Convert.ToInt16(provider["Availability"]);

                if (keyboardStatus == 0)
                    return "Autre";
                if (keyboardStatus == 1)
                    return "Inconnu";
                if (keyboardStatus == 2)
                    return "En cours d’exécution/Pleine alimentation";
                if (keyboardStatus == 3)
                    return "Avertissement";
                if (keyboardStatus == 4)
                    return "En test";
                if (keyboardStatus == 5)
                    return "Non applicable";
                if (keyboardStatus == 6)
                    return "Mise hors tension";
                if (keyboardStatus == 7)
                    return "Hors ligne";
                if (keyboardStatus == 8)
                    return "Hors service ";
                if (keyboardStatus == 9)
                    return "Détérioré";
                if (keyboardStatus == 10)
                    return "Non installé";
                if (keyboardStatus == 11)
                    return "Erreur d’installation";
                if (keyboardStatus == 12)
                    return "Power Save - Inconnu";
                if (keyboardStatus == 13)
                    return "Économie d’énergie - Mode faible consommation";
                if (keyboardStatus == 14)
                    return "Économie d’alimentation - Veille";
                if (keyboardStatus == 15)
                    return "Cycle d’alimentation";
                if (keyboardStatus == 16)
                    return "Power Save - Avertissement";
                if (keyboardStatus == 17)
                    return "Suspendu";
                if (keyboardStatus == 18)
                    return "Pas prêt";
                if (keyboardStatus == 19)
                    return "Non configuré";
                if (keyboardStatus == 20)
                    return "Silencieux";
            }

            return "NaN";
        }

        #endregion
    }
}
