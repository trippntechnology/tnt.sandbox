using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace ListProperty
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			propertyGrid1.SelectedObject = new ClassWithListProperty();
		}
	}

	public class ClassWithListProperty
	{
		private List<string> m_StringList = new List<string>();
		private string m_Comment = string.Empty;

//		[Editor( "System.Windows.Forms.Design.ListControlStringCollectionEditor,System.Design, Version=2.0.0.0, Culture=neutral,PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public List<string> StringList { get { return m_StringList; } set { m_StringList = value; } }

		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		public string Comment 
		{ 
			get { return m_Comment; } 
			set { m_Comment = value; } 
		}

		public ClassWithListProperty()
		{
			m_StringList.Add("One");
		}
		private int test1;
		[ReadOnly(true)]
		public int Test1
		{
			get { return test1; }
			set { test1 = value; }
		}

		private string test2;
		[TypeConverter(typeof(StringListConverter))]
		public string Test2
		{
			get { return test2; }
			set 
			{
				test2 = value;

				Test1 = m_StringList.IndexOf(test2);
			}
		}
	}

	public class StringListConverter : TypeConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		//public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		//{
		//  return true; // drop-down vs combo
		//}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ClassWithListProperty cwlp = context.Instance as ClassWithListProperty;
			return new StandardValuesCollection(cwlp.StringList.ToArray());
		}
	}

}
