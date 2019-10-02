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
			applicationRegistery = new ApplicationRegistry(this, Registry.CurrentUser, "Tripp'n Technology", "CenteredDrawing");
			_MyControl = new MyControl(this, 0, 0, this.Width, this.Height);
			InitializeComponent();
		}
	}
}
