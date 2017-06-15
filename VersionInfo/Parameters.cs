using TNT.Utilities;
using TNT.Utilities.Console;

namespace VersionInfo
{
	public class Parameters : TNT.Utilities.Console.Parameters
	{
		private static string ENV = "e";

		private Environment _Environment = null;

		public Environment Environment
		{
			get
			{
				if (_Environment == null)
				{
					_Environment = Utilities.DeserializeFromFile<Environment>((this[ENV] as FileParameter).Value);
				}

				return _Environment;
			}
		}

		public Parameters()
		{
			this.Add(new FileParameter(ENV, "File containing the evironent information", true) { MustExist = true });
		}
	}
}
