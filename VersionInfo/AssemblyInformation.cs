using System.IO;
using System.Reflection;
using TNT.Utilities;

namespace VersionInfo
{
	public class AssemblyInformation
	{
		public string AssemblyName { get; set; }

		public string Product { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string FileVersion { get; set; }

		public string InformationalVersion { get; set; }

		public AssemblyInformation(string assemblyPath)
		{
			Assembly assembly = Assembly.LoadFile(assemblyPath);

			this.AssemblyName = Path.GetFileName(assemblyPath);
			this.Description = Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly)?.Description;
			this.FileVersion = Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly)?.Version;
			this.InformationalVersion = Utilities.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(assembly)?.InformationalVersion;
			this.Title = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(assembly)?.Title;
			this.Product = Utilities.GetAssemblyAttribute<AssemblyProductAttribute>(assembly)?.Product;
		}

	}
}
