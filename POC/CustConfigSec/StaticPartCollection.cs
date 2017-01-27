using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CustConfigSec
{
	public class StaticPartCollection : ConfigurationElementCollection
	{
		public StaticPartElement this[string key]
		{
			get
			{
				return base.BaseGet(key) as StaticPartElement;
			}
		}

		public StaticPartElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as StaticPartElement;
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}
		protected
				override System.Configuration.ConfigurationElement CreateNewElement()
		{
			return new StaticPartElement();
		}

		protected override object GetElementKey(
				System.Configuration.ConfigurationElement element)
		{
			return ((StaticPartElement)element).Key;
		}
	}
}
