using System;
using System.Linq;
using TNT.Utilities.Console;

namespace PNToText
{
	public class Parameters : TNT.Utilities.Console.Parameters
	{
		protected const string NUMBER = "n";
		protected const string VOWEL = "v";

		public int Number { get { return (this[NUMBER] as IntParameter).Value; } }
		public int[] NumberArray { get { return Array.ConvertAll(Number.ToString().ToArray(), x => (int)x - 48); } }
		public bool MustContainVowel { get { return this.FlagExists(VOWEL); } }

		public Parameters()
			: base()
		{
			this.Add(new IntParameter(NUMBER, "Number to convert", true));
			this.Add(new FlagParameter(VOWEL, "Only show words containing vowels"));
		}
	}
}
