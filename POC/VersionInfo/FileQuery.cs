using System.Collections.Generic;
using System.Xml.Serialization;

namespace VersionInfo
{
	public class FileQuery
	{
		public string OutputFileName { get; set; }

		[XmlArrayItem("FilePath")]
		public List<string> FilePaths { get; set; }
	}
}
