using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CustConfigSec
{
	public class StaticPartElement : ConfigurationElement
	{
		/// <summary>
		/// Returns the key value.
		/// </summary>
		[System.Configuration.ConfigurationProperty("key", IsRequired = true)]
		public string Key
		{
			get
			{
				return this["key"] as string;
			}
		}

		/// <summary>
		/// Returns the setting value for the production environment.
		/// </summary>
		[System.Configuration.ConfigurationProperty("prod", IsRequired = true)]
		public string Prod
		{
			get
			{
				return this["prod"] as string;
			}
		}

		/// <summary>
		/// Returns the setting value for the development environment.
		/// </summary>
		[System.Configuration.ConfigurationProperty("dev", IsRequired = true)]
		public string Dev
		{
			get
			{
				return this["dev"] as string;
			}
		}

		/// <summary>
		/// Returns the setting description.
		/// </summary>
		[System.Configuration.ConfigurationProperty("desc", IsRequired = false)]
		public string Desc
		{
			get
			{
				return this["desc"] as string;
			}
		}
	}
}
