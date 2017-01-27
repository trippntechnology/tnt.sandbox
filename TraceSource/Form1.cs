using System.Diagnostics;
using System.Windows.Forms;

namespace TraceTest
{
	public partial class Form1 : Form
	{
		TraceSource m_TraceSource = new TraceSource("TraceTest");

		public Form1()
		{
			InitializeComponent();

			//MyTextWriterTraceListener mtw = new Medicity.HISP.Common.MyTextWriterTraceListener("foo.txt");
			//m_TraceSource.Listeners.Add(mtw);

			ErrorText.Tag = TraceEventType.Error;
			WarningText.Tag = TraceEventType.Warning;
			InformationText.Tag = TraceEventType.Information;
			VerboseText.Tag = TraceEventType.Verbose;

			m_TraceSource.TraceInformation("m_TraceSource.TraceInformation");

			m_TraceSource.TraceData(TraceEventType.Verbose, 10, 1, 2, 3, "four", "five");
			m_TraceSource.TraceData(TraceEventType.Verbose, 10, new MyPoint(20, 30));

			m_TraceSource.TraceEvent(TraceEventType.Verbose, 10, "Verbose message");
			m_TraceSource.TraceEvent(TraceEventType.Information, 10, "Informational message");
			m_TraceSource.TraceEvent(TraceEventType.Warning, 10, "Warning message");
			m_TraceSource.TraceEvent(TraceEventType.Error, 10, "Error message");
			m_TraceSource.TraceEvent(TraceEventType.Critical, 10, "Critical message");

			m_TraceSource.TraceData(TraceEventType.Information, 0, new string[][] { new string[] { "name", "value" }, new string[] { "name", "value" } });

			m_TraceSource.Flush();
		}

		private void Text_KeyUp(object sender, KeyEventArgs e)
		{
			TextBox tb = sender as TextBox;

			if (e.KeyCode == Keys.Enter && tb != null)
			{
				m_TraceSource.TraceEvent((TraceEventType)tb.Tag, 0, tb.Text);
				m_TraceSource.Flush();
			}
		}
	}

	public class MyPoint
	{
		public int X { get; set; }
		public int Y { get; set; }

		public MyPoint(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", X, Y);
		}
	}
}
