using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNToText
{
	public class Key
	{
		protected char[] m_Characters;
		protected int m_CharIndex = 0;
		protected Key m_DependantKey;

		public int Number { get; set; }
		public char Character { get; set; }
		public bool RolledOver { get; set; }

		public Key(int number, char[] characters, Key dependantKey)
		{
			this.Number = number;
			this.m_Characters = characters;
			this.m_DependantKey = dependantKey;
		}

		public Key(int number, char[] characters)
			: this(number, characters, null)
		{
		}


		//public Key(Key key)
		//{
		//	this.Number = key.Number;
		//	this.m_Characters = key.m_Characters;
		//	this.m_CharIndex = key.m_CharIndex;
		//}

		public static Key operator ++(Key key)
		{
			key.m_CharIndex++;

			if (key.m_CharIndex >= key.m_Characters.Length)
			{
				if (key.m_DependantKey != null)
				{
					key.m_DependantKey++;
				}

				key.m_CharIndex = 0;
				key.RolledOver = true;
			}

			return key;
		}

		public override string ToString()
		{
			string rtnValue = string.Empty;

			if (this.m_CharIndex < this.m_Characters.Length)
			{
				rtnValue = this.m_Characters[this.m_CharIndex].ToString();
			}

			return rtnValue;
		}
	}
}
