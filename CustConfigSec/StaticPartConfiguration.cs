using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CustConfigSec
{
	public class StaticPartConfiguration : ConfigurationSection
	{
		private static string sConfigurationSectionConst = "StaticPartSection";

		/// <summary>
		/// Returns an shiConfiguration instance
		/// </summary>
		public static StaticPartConfiguration GetConfig()
		{
			StaticPartConfiguration spc = (StaticPartConfiguration)System.Configuration.ConfigurationManager.GetSection(StaticPartConfiguration.sConfigurationSectionConst);

			if (spc == null)
			{
				spc = new StaticPartConfiguration();
			}

			return spc;

		}
		[System.Configuration.ConfigurationProperty("StaticPartSettings")]
		public StaticPartCollection StaticPartSettings
		{
			get
			{
				return (StaticPartCollection)this["StaticPartSettings"] ??
					 new StaticPartCollection();
			}
		}

	}
}
