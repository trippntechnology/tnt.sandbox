using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;
using TNT.Utilities;

namespace CenteredDrawing
{
	public partial class Form1 : Form
	{
		private ApplicationRegistry applicationRegistery = null;

		private const int WIDTH = 850;
		private const int HEIGHT = 1100;
		private Brush BlackBrush = new SolidBrush(Color.Black);
		private MyControl _MyControl = null;

		public Form1()
		{
			InitializeComponent();
			applicationRegistery = new ApplicationRegistry(this, Registry.CurrentUser, "Tripp'n Technology", "CenteredDrawing");
			_MyControl = new MyControl(splitContainer1.Panel1, 0, 0, splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
			_MyControl.BackColor = Color.Yellow;
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			var width = trackBar1.Value;
			var height = trackBar2.Value;
			_MyControl.SetCanvasSize(width, height);
		}
	}
}
