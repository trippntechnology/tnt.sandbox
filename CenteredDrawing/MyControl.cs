using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CenteredDrawing
{
	class MyControl : Control
	{
		private Brush FillBrush = new SolidBrush(Color.Red);
		private Pen DrawPen = new Pen(Color.Black);
		private Brush ShadowBrush = new SolidBrush(Color.LightGray);

		private int LeftOffset = 0;
		private int TopOffset = 0;
		private int ShadowOffset = 10;
		private int CanvasWidth = 500;
		private int CanvasHeight = (int)(500 * 8.5 / 11);
		private int Zoom = 100;

		private int ZoomedWidth { get { return (int)(CanvasWidth * Zoom / 100); } }
		private int ZoomedHeight { get { return (int)(CanvasHeight * Zoom / 100); } }

		private KeyEventArgs keyEventArgs = null;

		protected override void OnPaint(PaintEventArgs e)
		{
			var shadowLeft = LeftOffset + ShadowOffset;
			var shadowTop = TopOffset + ShadowOffset;
			var graphics = e.Graphics;
			graphics.FillRectangle(ShadowBrush, shadowLeft, shadowTop, ZoomedWidth, ZoomedHeight);
			graphics.FillRectangle(FillBrush, LeftOffset, TopOffset, ZoomedWidth, ZoomedHeight);
			graphics.DrawRectangle(DrawPen, LeftOffset, TopOffset, ZoomedWidth, ZoomedHeight);
			graphics.DrawRectangle(DrawPen, LeftOffset + 10, TopOffset + 10, ZoomedWidth - 20, ZoomedHeight - 20);
			Debug.WriteLine($"LeftOffset: {LeftOffset}  TopOffset: {TopOffset}  WIDTH: {ZoomedWidth}  HEIGHT: {ZoomedHeight}");
			base.OnPaint(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			keyEventArgs = e;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			keyEventArgs = e;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			var wheelDelta = SystemInformation.MouseWheelScrollDelta;
			var detents = e.Delta / wheelDelta * 10;

			if (keyEventArgs?.Control == true)
			{
				Zoom += detents;
				Position(Parent);
				Refresh();
			}

			Debug.WriteLine($"X: {e.X}  Y: {e.Y}  Delta: {e.Delta}  WHEEL_DELTA: {wheelDelta}  Location: {e.Location}  Zoom: {Zoom}");
		}

		protected override void OnClick(EventArgs e)
		{
			Debug.WriteLine("OnClick");
			base.OnClick(e);
		}

		public void OnParentResize(object sender, EventArgs e)
		{
			Position(sender);
			Refresh();
		}

		private void Position(object sender)
		{
			var clientRect = (sender as Control).ClientRectangle;
			var form = sender as Form;

			if (clientRect.Width <= ZoomedWidth)
			{
				LeftOffset = 0;
			}
			else
			{
				LeftOffset = clientRect.Width / 2 - ZoomedWidth / 2;
			}

			Width = LeftOffset * 2 + ZoomedWidth;

			if (clientRect.Height <= ZoomedHeight)
			{
				TopOffset = 0;
			}
			else
			{
				TopOffset = clientRect.Height / 2 - ZoomedHeight / 2;
			}

			Height = TopOffset * 2 + ZoomedHeight;

			var result = (sender as Form).Let(f =>
			{
				f.AutoScroll = LeftOffset == 0 || TopOffset == 0;
				return 10;
			}
			);
		}
	}
}
