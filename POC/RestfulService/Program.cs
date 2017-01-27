using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulService
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri uri = new Uri(ConfigurationManager.AppSettings["URI"]);

			Service service = new Service();
			using (WebServiceHost serviceHost = new WebServiceHost(service, uri))
			{
				var secureWebHttpBinding = new WebHttpBinding(WebHttpSecurityMode.Transport) { Name = "secureHttpWeb" };
				serviceHost.AddServiceEndpoint(typeof(IService), secureWebHttpBinding, "");
				serviceHost.Open();

				Console.WriteLine("Service started. Listening at {0}. Press any key to terminate.", uri.ToString());
				Console.WriteLine();
				//Thread.Sleep(10000);
				Console.ReadKey();
			}
		}
	}
}
