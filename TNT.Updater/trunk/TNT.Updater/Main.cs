using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TNT.Utilities;

namespace TNT.Updater
{
	public partial class Main : Form
	{
		protected Ftp m_Ftp = null;
		protected Settings m_Settings = null;
		protected UpdateInfo m_UpdateInfo = null;

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			// Load configuration
			m_Settings = TNT.Configuration.XmlSection<Settings>.Deserialize("TNT.Updater.Settings");

			// Set the caption
			this.Text = m_Settings.Caption;

			// Initialize FTP connector
			m_Ftp = new Ftp(m_Settings.FtpHostUri, m_Settings.UserName, m_Settings.Password);
			string statusDescription;

			// Obtain the update information (*.ui) from the site
			using (Stream responseStream = m_Ftp.Download(m_Settings.ConfigPath, out statusDescription))
			using (StreamReader reader = new StreamReader(responseStream))
			{
				m_UpdateInfo = Utilities.Utilities.Deserialize<UpdateInfo>(reader.ReadToEnd(), new Type[0]);

				// Populate corresponding fields in the updater
				ApplicationLabel.Text = m_UpdateInfo.ApplicationName;
				PublisherLabel.Text = m_UpdateInfo.Publisher;
				ReleasedLabel.Text = m_UpdateInfo.ReleaseDate;

				// Get the current version of the application
				string path = Path.GetDirectoryName(Application.ExecutablePath);
				Assembly ass = Assembly.LoadFile(Path.Combine(path, m_Settings.Application));

				AssemblyFileVersionAttribute afva = Utilities.Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(ass);

				// Set the current version
				InstalledVersionLabel.Text = afva.Version;

				// Set the updated version
				LatestVersionLabel.Text = m_UpdateInfo.Version;

				// Check if the latest version is newer than existing version
				Version installedVersion = new Version(afva.Version);
				Version latestVersion = new Version(m_UpdateInfo.Version);

				if (latestVersion > installedVersion)
				{
					// Latest version is newer. Provide option to update.
					ActionButton.Text = "Update";
					ActionButton.Click += ActionButton_Update;
				}
				else
				{
					// The installed version is up-to-date
					ActionButton.Text = "Ok";
					ActionButton.Click += ActionButton_Close;
				}
			}
		}

		private void ActionButton_Update(object sender, EventArgs e)
		{
			// Create a temporary file for the update
			string tempFile = Path.GetTempFileName().Replace(".tmp", ".exe");
			string statusDescription;

			// Download the update and save to temporary file location
			using (Stream stream = m_Ftp.Download(m_UpdateInfo.InstallationPath, out statusDescription))
			using (Stream file = File.OpenWrite(tempFile))
			{
				stream.CopyTo(file);
			}

			// Run the update
			System.Diagnostics.Process.Start(tempFile);

			ActionButton_Close(null, null);
		}

		private void ActionButton_Close(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
