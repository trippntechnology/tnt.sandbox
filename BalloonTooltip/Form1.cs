using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BalloonTooltip
{
	public partial class Form1 : Form
	{
		ToolTip tt = new ToolTip();
		Point location = Point.Empty;
		bool show = true;

		public Form1()
		{
			InitializeComponent();

			//tt.ToolTipTitle = "Button Tooltip";
			tt.UseFading = true;
			tt.UseAnimation = true;
			tt.IsBalloon = true;

			tt.ShowAlways = true;

			//tt.AutoPopDelay = 1000;
			tt.InitialDelay = 500;
			tt.ReshowDelay = 500;
			tt.SetToolTip(button1, "Click me to execute.\nNew line");
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			location = e.Location;
			if (e.X < 50 || e.X > Width - 50)
			{
				if (show)
				{
					tt.Show(string.Empty, this, 0);
					tt.Show(string.Format("({0}, {1})", e.X, e.Y), this, e.X, e.Y - 20);
					show = false;
				}
			}
			else if (e.X > 100 && e.X < 120 && e.Y > 100 && e.Y < 120)
			{
				if (show)
				{
					tt.Show(string.Empty, this, 110, 110);
					tt.Show("Balloon hint", this, 110, 110);
					show = false;
				}
			}
			else
			{
				tt.Hide(this);
				show = true;
			}

			System.Diagnostics.Debug.WriteLine(string.Format("{0}: Form1_MouseMove({1})", DateTime.Now.Ticks, show.ToString()));
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.Black), 100, 100, 20, 20);
			e.Graphics.DrawEllipse(new Pen(Color.Black), 108, 108, 4, 4);
		}
	}
}
