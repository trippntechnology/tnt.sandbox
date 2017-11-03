using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumBitwise
{
	class Program
	{
		static void Main(string[] args)
		{
			Week week1 = Week.Monday | Week.Wedndesday | Week.Friday;

			bool result1 = week1.HasFlag(Week.Wedndesday);
		}
	}
}
