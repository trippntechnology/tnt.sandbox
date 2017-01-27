using System.Runtime.Serialization;

namespace RestfulService
{
	public class ComplexParameter
	{
		public string Parameter1 { get; set; }

		public string Parameter2 { get; set; }

		public override string ToString()
		{
			string ret = string.Format("{{\"Parameter1\":\"{0}\",\"Parameter2\":\"{1}\"}}", this.Parameter1, this.Parameter2);
			return ret;
		}
	}
}
