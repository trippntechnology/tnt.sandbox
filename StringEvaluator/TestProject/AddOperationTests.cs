using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
	[TestClass]
	public class AddOperationTests
	{
		[TestMethod]
		public void TwoDoubles()
		{
			Operation op = new AddOperation(7, 11);
			Assert.AreEqual(18, op.Evaluate());
		}

		[TestMethod]
		public void LHDoubleRHOperation()
		{
			Operation rh = new AddOperation(7, 11);
			Operation op = new AddOperation(13, rh);
			Assert.AreEqual(31, op.Evaluate());
		}

		[TestMethod]
		public void LHOPerationRHDouble()
		{
			Operation lh = new AddOperation(7, 11);
			Operation op = new AddOperation(lh, 13);
			Assert.AreEqual(31, op.Evaluate());
		}

		[TestMethod]
		public void TwoOperations()
		{
			Operation lh = new AddOperation(7, 11);
			Operation rh = new AddOperation(13, 19);
			Operation op = new AddOperation(lh, rh);
			Assert.AreEqual(50, op.Evaluate());
		}
	}
}
