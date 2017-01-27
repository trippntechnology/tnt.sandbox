using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caching
{
	class Program
	{
		// Creates in-memory cache. Parameter can be used to set properties from configuration
		static MemoryCache m_MemCache = new MemoryCache("MemoryCache");

		static string Value
		{
			get
			{
				string value = ToString(m_MemCache["key"]);

				if (string.IsNullOrEmpty(value))
				{
					// AbsoluteExpiration expires at a given time. In this case, 30 seconds from Now
					m_MemCache.Add("key", "the value", new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30) });
					value = ToString(m_MemCache["key"]);
				}

				return value;
			}
		}

		static void Main(string[] args)
		{
			string value = Value;

			Thread.Sleep(1000);
			value = Value;

			Thread.Sleep(1000);
			value = Value;

			Thread.Sleep(1000);
			value = Value;

			Thread.Sleep(1000);
			value = Value;

			Thread.Sleep(1000);
			value = Value;

			Thread.Sleep(1000);
			value = Value;
		}

		static string ToString(object obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
