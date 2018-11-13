using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMovie.Models
{
	public class Accounts
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public bool Enabled { get; set; }
	}
}
