namespace TNT.Enum
{
	class EnumTest : TNTEnum
	{
		public int IntProperty { get; private set; }
		public string StringProperty { get; private set; }

		public EnumTest(string name, int intProperty, string stringProperty)
			: base(name)
		{
			IntProperty = intProperty;
			StringProperty = stringProperty;
		}

		static public EnumTest Enum1 { get; } = new EnumTest("Enum1", 1, "String1");
		static public EnumTest Enum2 { get; } = new EnumTest("Enum2", 2, "String2");
		static public EnumTest Enum3 { get; } = new EnumTest("Enum3", 3, "String3");
		static public EnumTest Enum4 { get; } = new EnumTest("Enum4", 4, "String4");

	}
}
