using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace HardwareInfo
{
	class Program
	{
		static void Main(string[] args)
		{
			ManagementObjectSearcher mos = new ManagementObjectSearcher(string.Format("{0}", args[0]));
			string value = string.Empty;

			foreach (ManagementObject mo in mos.Get())
			{
				Console.WriteLine(mo["Name"]);

				foreach (PropertyData pd in mo.Properties)
				{
					if (pd.Value != null && pd.Value.ToString() != "")
					{
						value = pd.Value.ToString();
					}
					else
					{
						value = string.Empty;
					}

					Console.WriteLine(string.Format("{0}: {1}", pd.Name, value));
				}
			}
		}
	}
}
