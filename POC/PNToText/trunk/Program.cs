using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PNToText
{
	class Program
	{
		static void Main(string[] args)
		{
			Parameters parms = new Parameters();

			if (!parms.ParseArgs(args))
			{
				return;
			}

			Dictionary<int, char[]> numberMap = new Dictionary<int, char[]>();

			numberMap[0] = KeyCharacters.Zero;
			numberMap[1] = KeyCharacters.One;
			numberMap[2] = KeyCharacters.Two;
			numberMap[3] = KeyCharacters.Three;
			numberMap[4] = KeyCharacters.Four;
			numberMap[5] = KeyCharacters.Five;
			numberMap[6] = KeyCharacters.Six;
			numberMap[7] = KeyCharacters.Seven;
			numberMap[8] = KeyCharacters.Eight;
			numberMap[9] = KeyCharacters.Nine;

			List<Key> keys = new List<Key>();
			Key previousKey = null;

			foreach (int value in parms.NumberArray)
			{
				keys.Add(new Key(value, numberMap[value], previousKey));
				previousKey = keys.Last();
			}

			Regex regex = new Regex("(A|E|I|O|U|Y)", RegexOptions.IgnoreCase);

			while (!keys.First().RolledOver)
			{
				StringBuilder sb = new StringBuilder();

				foreach (Key key in keys)
				{
					sb.Append(key);
				}

				if (!parms.MustContainVowel || regex.IsMatch(sb.ToString()))
				{
					Console.WriteLine(sb.ToString());
				}

				previousKey++;
			}
		}
	}
}
