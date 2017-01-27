using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linq
{
	class Program
	{
		static void Main(string[] args)
		{
			// Example of returning non-matches
			string[] set1 = { "One", "Two", "Three", "Four", "Five", "Six" };
			string[] set2 = { "Two", "Four", "Six" };

			var result = (from s1 in set1 join s2 in set2 on s1 equals s2 into joinedset from js in joinedset.DefaultIfEmpty() where js == null select s1).ToList<string>();

			List<Integer> c1List = new List<Integer>();
			List<Integer> c2List = new List<Integer>();

			c1List.Add(new Integer() { NumericValue = 1, StringValue = "One" });
			c1List.Add(new Integer() { NumericValue = 2, StringValue = "Two" });
			c1List.Add(new Integer() { NumericValue = 3, StringValue = "Three" });
			c1List.Add(new Integer() { NumericValue = 4, StringValue = "Four" });
			c1List.Add(new Integer() { NumericValue = 5, StringValue = "Five" });
			c1List.Add(new Integer() { NumericValue = 6, StringValue = "Six" });

			c2List.Add(new Integer() { NumericValue = 2, StringValue = "Two" });
			c2List.Add(new Integer() { NumericValue = 4, StringValue = "Four" });
			c2List.Add(new Integer() { NumericValue = 6, StringValue = "Six" });

			List<Integer> results = (from c1 in c1List
															join c2 in c2List on
															new { c1.NumericValue, c1.StringValue } equals new { c2.NumericValue, c2.StringValue }
															into set
															from s in set.DefaultIfEmpty()
															where s == null
															select c1).ToList();

		}
	}
}
