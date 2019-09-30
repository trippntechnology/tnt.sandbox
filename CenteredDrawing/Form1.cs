using System;
using System.Drawing;
using System.Windows.Forms;

namespace CenteredDrawing
{
	public partial class Form1 : Form
	{
		private const int WIDTH = 850;
		private const int HEIGHT = 1100;
		private Brush BlackBrush = new SolidBrush(Color.Black);
		private MyControl _MyControl = new MyControl();

		public Form1()
		{
			_MyControl.Parent = this;
			_MyControl.Left = 0;
			_MyControl.Top = 0;
			_MyControl.Width = this.Width;
			_MyControl.Height = this.Height;
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var g = e.Graphics;

			var leftPos = Width < WIDTH ? 0 : Width / 2 - WIDTH / 2;
			var topPos = Height < HEIGHT ? 0 : Height / 2 - HEIGHT / 2;

			//g.FillRectangle(Background, leftPos, topPos, WIDTH, HEIGHT);
			//g.FillRectangle(BlackBrush, leftPos, topPos, WIDTH, HEIGHT);
		}

		private void Form1_Resize(object sender, EventArgs e) => _MyControl.OnParentResize(sender, e);
	}
}
