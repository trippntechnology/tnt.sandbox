using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TNT.Utilities;
using TNTDrawing;

namespace CenteredDrawing
{
	public partial class Form1 : Form
	{
		private ApplicationRegistry applicationRegistery = null;
		private CanvasPanel _MyControl = null;

		public Form1()
		{
			InitializeComponent();
			applicationRegistery = new ApplicationRegistry(this, Registry.CurrentUser, "Tripp'n Technology", "CenteredDrawing");
			//_MyControl = new Canvas(splitContainer1.Panel1, 0, 0, splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
			//_MyControl.BackColor = Color.Yellow;
			//_MyControl.MouseMove += _MyControl_MouseMove;
			_MyControl = new CanvasPanel(this)
			{
				Parent = splitContainer1.Panel1,
				Width = splitContainer1.Panel1.Width,
				Height = splitContainer1.Panel1.Height
			};
			propertyGrid1.SelectedObject = _MyControl;
		}

		private void _MyControl_MouseMove(object sender, MouseEventArgs e)
		{
			toolStripStatusLabel1.Text = $"{e.X}, {e.Y}";
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			//var width = trackBar1.Value;
			//var height = trackBar2.Value;
			//_MyControl.SetCanvasSize(width, height);
		}
	}
}
