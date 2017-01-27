
namespace Library
{
	public abstract class Operation
	{
		public double LHDouble { get; set; }
		public double RHDouble { get; set; }

		public Operation LHOperation { get; set; }
		public Operation RHOperation { get; set; }

		public Operation(double lhDouble, double rhDouble)
		{
			this.LHDouble = lhDouble;
			this.RHDouble = rhDouble;
		}

		public Operation(Operation lhOperation, double rhDouble)
		{
			this.LHOperation = lhOperation;
			this.RHDouble = rhDouble;
		}

		public Operation(double lhDouble, Operation rhOperation)
		{
			this.LHDouble = lhDouble;
			this.RHOperation = rhOperation;
		}

		public Operation(Operation lhOperation, Operation rhOperation)
		{
			this.LHOperation = lhOperation;
			this.RHOperation = RHOperation;
		}

		public abstract double Evaluate();
	}
}
