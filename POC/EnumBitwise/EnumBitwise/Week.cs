using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumBitwise
{
	[Flags]
	enum Week
	{
		Sunday = 1,
		Monday = 2,
		Tuesday = 4,
		Wedndesday = 8,
		Thursday = 16,
		Friday = 32,
		Saturday = 64
	}
}
