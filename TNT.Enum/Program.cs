using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNT.Enum
{
	class Program
	{
		static void Main(string[] args)
		{
			var enum1 = EnumTest.Enum1;
			var enum2 = EnumTest.Enum2;
			var enum3 = EnumTest.Enum3;
			var enum4 = EnumTest.Enum4;

			var name = EnumTest.Enum3.ToString();

			var found = EnumTest.ToEnum<EnumTest>("Enum0");
			found = EnumTest.ToEnum<EnumTest>("Enum1");
		}
	}
}
