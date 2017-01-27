
namespace TNT.Updater
{
	/// <summary>
	/// Information provided from the FTP site
	/// </summary>
	public class UpdateInfo
	{
		/// <summary>
		/// Version of update
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Application's name
		/// </summary>
		public string ApplicationName { get; set; }

		/// <summary>
		/// Publisher
		/// </summary>
		public string Publisher { get; set; }

		/// <summary>
		/// Date the update was released
		/// </summary>
		public string ReleaseDate { get; set; }

		/// <summary>
		/// Relative path of installation within the FTP site
		/// </summary>
		public string InstallationPath { get; set; }
	}
}
