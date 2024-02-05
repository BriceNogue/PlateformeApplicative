using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;

namespace Desktop.Services
{
    public class DeviceInfoService
    {
        public DeviceInfoService() { }

        public string GetMachineName()
        {
            return System.Environment.MachineName;
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
    }
}
