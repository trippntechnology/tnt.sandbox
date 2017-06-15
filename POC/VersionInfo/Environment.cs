using System.Collections.Generic;

namespace VersionInfo
{
	public class Environment
	{
		public List<FileQuery> FileQueries { get; set; }

		public List<DBQuery> DBQueries { get; set; }
	}
}
