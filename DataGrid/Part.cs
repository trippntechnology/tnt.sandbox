using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
	public class Part
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public double Cost { get; set; }

		public Part()
		{

		}
		//public Part(string code, string description, double cost)
		//{
		//	this.Code = code;
		//	this.Description = description;
		//	this.Cost = cost;
		//}
	}
}
