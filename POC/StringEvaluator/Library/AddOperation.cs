
namespace Library
{
	public class AddOperation : Operation
	{
		public AddOperation(double lhDouble, double rhDouble)
			: base(lhDouble, rhDouble)
		{
		}

		public AddOperation(Operation lhOperation, double rhDouble)
			: base(lhOperation, rhDouble)
		{
		}

		public AddOperation(double lhDouble, Operation rhOperation)
			: base(lhDouble, rhOperation)
		{
		}

		public AddOperation(Operation lhOperation, Operation rhOperation)
			: base(lhOperation, rhOperation)
		{
		}

		public override double Evaluate()
		{
			if (base.LHOperation != null && base.RHOperation != null)
			{
				return base.LHOperation.Evaluate() + base.RHOperation.Evaluate();
			}
			else if (base.LHOperation!=null)
			{
				return base.LHOperation.Evaluate() + base.RHDouble;
			}
			else if (base.RHOperation != null)
			{
				return base.LHDouble + base.RHOperation.Evaluate();
			}
			else
			{
				return base.LHDouble + base.RHDouble;
			}
		}
	}
}
