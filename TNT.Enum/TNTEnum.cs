using System.Collections.Generic;

namespace TNT.Enum
{
	class TNTEnum
	{
		private static int ordinal = 0;
		private static List<TNTEnum> enumerations = new List<TNTEnum>();

		public string Name { get; private set; }
		public int Ordinal { get; private set; }

		public TNTEnum(string name)
		{
			Name = name;
			Ordinal = ordinal++;
			enumerations.Add(this);
		}

		public override string ToString() => Name;

		public static T ToEnum<T>(string name) where T : TNTEnum
		{
			return enumerations.Find(e => e.Name == name) as T;
		}
	}
}
