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
		private float ScalePercentage = 100.0f;

		private int ScaledWidth { get { return (int)(CanvasWidth * ScalePercentage / 100); } }
		private int ScaledHeight { get { return (int)(CanvasHeight * ScalePercentage / 100); } }

		private KeyEventArgs keyEventArgs = null;

		public MyControl(Control parent, int left, int top, int width, int height)
			: base(parent, string.Empty, left, top, width, height)
		{
			parent.SizeChanged += OnParentResize;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var shadowLeft = LeftOffset + ShadowOffset;
			var shadowTop = TopOffset + ShadowOffset;
			var graphics = e.Graphics;
			graphics.FillRectangle(ShadowBrush, shadowLeft, shadowTop, ScaledWidth, ScaledHeight);
			graphics.FillRectangle(FillBrush, LeftOffset, TopOffset, ScaledWidth, ScaledHeight);
			graphics.DrawRectangle(DrawPen, LeftOffset, TopOffset, ScaledWidth, ScaledHeight);
			graphics.DrawRectangle(DrawPen, LeftOffset + 10, TopOffset + 10, ScaledWidth - 20, ScaledHeight - 20);
			Debug.WriteLine($"LeftOffset: {LeftOffset}  TopOffset: {TopOffset}  WIDTH: {ScaledWidth}  HEIGHT: {ScaledHeight}");
			base.OnPaint(e);
		}

		protected override void OnKeyDown(KeyEventArgs e) => keyEventArgs = e;
		protected override void OnKeyUp(KeyEventArgs e) => keyEventArgs = e;

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			var wheelDelta = SystemInformation.MouseWheelScrollDelta;
			var detents = e.Delta / (wheelDelta);// * 10.0);
			var amp = keyEventArgs?.Shift == true ? 2 : 1;

			if (keyEventArgs?.Control == true && ScalePercentage + detents > 0)
			{
				ScalePercentage += detents * amp;
				Position(Parent);
				Refresh();
			}

			Debug.WriteLine($"X: {e.X}  Y: {e.Y}  detents: {detents}  Delta: {e.Delta}  WHEEL_DELTA: {wheelDelta}  Location: {e.Location}  Zoom: {ScalePercentage}");
		}

		protected override void OnClick(EventArgs e)
		{
			Debug.WriteLine("OnClick");
			base.OnClick(e);
		}

		private void OnParentResize(object sender, EventArgs e)
		{
			Position(sender);
			Refresh();
		}

		private void Position(object sender)
		{
			var clientRect = (sender as Control).ClientRectangle;
			var form = sender as Form;

			if (clientRect.Width <= ScaledWidth)
			{
				LeftOffset = 0;
			}
			else
			{
				LeftOffset = clientRect.Width / 2 - ScaledWidth / 2;
			}

			Width = LeftOffset * 2 + ScaledWidth;

			if (clientRect.Height <= ScaledHeight)
			{
				TopOffset = 0;
			}
			else
			{
				TopOffset = clientRect.Height / 2 - ScaledHeight / 2;
			}

			Height = TopOffset * 2 + ScaledHeight;

			var result = (sender as Form).Let(f =>
			{
				f.AutoScroll = LeftOffset == 0 || TopOffset == 0;
				return 10;
			}
			);
		}
	}
}
