
namespace TNT.Updater
{
	/// <summary>
	/// Settings used by the Updater 
	/// </summary>
	public class Settings
	{
		/// <summary>
		/// User name used to authenicate to FTP site
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Password used to authenticate to FTP site
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// FTP host URI
		/// </summary>
		public string FtpHostUri { get; set; }

		/// <summary>
		/// Relative path to update information (*.ui) file
		/// </summary>
		public string ConfigPath { get; set; }

		/// <summary>
		/// Relative path to application that may have an update
		/// </summary>
		public string Application { get; set; }

		/// <summary>
		/// Caption used by the Updater
		/// </summary>
		public string Caption { get; set; }

		public Settings()
		{
			this.UserName = string.Empty;
			this.Password = string.Empty;
		}
	}
}
