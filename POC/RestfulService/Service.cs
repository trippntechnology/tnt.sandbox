using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RestfulService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class Service : IService
	{
		public string SimpleMethod(string parameter)
		{
			Console.WriteLine("SampleMethod called with '{0}'", parameter);
			Console.WriteLine("Please enter text that should be returned by SampleMethod");
			return Console.ReadLine();

			//return string.Format("SampleMethod was passed {0}", parameter);
		}


		public string ComplexMethod(ComplexParameter parameter)
		{
			Console.WriteLine("ComplexMethod called with '{0}'", parameter.ToString());
			string response = string.Format("You sent me two values, {0} and {1}", parameter.Parameter1, parameter.Parameter2);
			Console.WriteLine("Sending: {0}", response);
			return response;

			//Console.WriteLine("Please enter text that should be returned by ComplexMethod");
			//return Console.ReadLine();
		}


		public ReturnParameter ComplexReturn(ComplexParameter parameter)
		{
			return new ReturnParameter() { Success = true, Message = string.Format("You sent me two values, {0} and {1}", parameter.Parameter1, parameter.Parameter2) };
		}
	}
}
