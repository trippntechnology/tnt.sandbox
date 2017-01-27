using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatedClone
{
	public class A
	{
		public int One { get; set; }

		public A Clone()
		{
			return Clone(false);
		}

		virtual public A Clone(bool isForUndo)
		{
			A obj = Activator.CreateInstance(GetType()) as A;

			if (isForUndo)
			{
				obj.One = this.One;
			}

			return obj;
		}
	}

	public class B : A
	{
		public int Two { get; set; }

		public override A Clone(bool isForUndo)
		{
			B obj = base.Clone(isForUndo) as B;

			if (isForUndo)
			{
				obj.Two = this.Two;
			}

			return obj;
		}
	}

	public class C : B
	{
		public int Three { get; set; }

		public override A Clone(bool isForUndo)
		{
			C obj = base.Clone(isForUndo) as C;

			if (isForUndo)
			{
				obj.Three = this.Three;
			}

			return obj;
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			A a = new A();
			B b = new B();
			C c = new C();

			a.One = 1;
			b.One = 1;
			b.Two = 2;
			c.One = 1;
			c.Two = 2;
			c.Three = 3;

			A a1 = a.Clone();
			A a2 = a.Clone(true);

			A b1 = b.Clone();
			A b2 = b.Clone(true);

			A c1 = c.Clone();
			A c2 = c.Clone(true);
		}
	}
}
