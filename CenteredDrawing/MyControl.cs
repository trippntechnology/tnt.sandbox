using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CenteredDrawing
{
	class MyControl : Control
	{
		private readonly Brush RedBrush = new SolidBrush(Color.Red);
		private readonly Pen DrawPen = new Pen(Color.Black);
		private readonly Brush ShadowBrush = new SolidBrush(Color.LightGray);

		private Rectangle Canvas = new Rectangle(0, 0, 500, (int)(500 * 8.5 / 11));
		private Rectangle Shadow = new Rectangle(0, 0, 500 + 10, (int)(500 * 8.5 / 11) + 10);
		private float ScalePercentage = 1.0f;

		private KeyEventArgs keyEventArgs = null;

		private Grid Grid = new Grid(Color.White, Color.Aqua, 10);

		public MyControl(Control parent, int left, int top, int width, int height)
			: base(parent, string.Empty, left, top, width, height)
		{
			parent.SizeChanged += OnParentResize;
			Shadow = Rectangle.Inflate(Canvas, 0, 0).Also(r =>
			{
				r.Offset(10, 10);
				return r;
			});

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var graphics = e.Graphics;
			var scaledCanvas = Canvas.Scale(ScalePercentage);

			Debug.WriteLine($"ScaledPercentage: {ScalePercentage}  Width: {Width}  Height: {Height}");
			Debug.WriteLine($"Width: {scaledCanvas.Width}  Height: {scaledCanvas.Height}");

			var xTranslation = Math.Max((Width - scaledCanvas.Width) / 2, 0);
			var yTranslation = Math.Max((Height - scaledCanvas.Height) / 2, 0);

			Width = (int)scaledCanvas.Width > Parent.ClientRectangle.Width ? (int)scaledCanvas.Width : Parent.ClientRectangle.Width;
			Height = (int)scaledCanvas.Height > Parent.ClientRectangle.Height ? (int)scaledCanvas.Height : Parent.ClientRectangle.Height;

			(Parent as ScrollableControl)?.Let(f => f.AutoScroll = Width > f.ClientRectangle.Width || Height > f.ClientRectangle.Height);

			Debug.WriteLine($"xTranslation: {xTranslation}  yTranslation: {yTranslation}");
			graphics.ScaleTransform(ScalePercentage, ScalePercentage, MatrixOrder.Append);
			graphics.TranslateTransform(xTranslation, yTranslation, MatrixOrder.Append);

			graphics.FillRectangle(ShadowBrush, Shadow);
			Grid.Draw(graphics, Canvas);
			graphics.FillRectangle(RedBrush, 33, 33, 99, 99);

			base.OnPaint(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.Focus();
			base.OnMouseMove(e);
		}

		protected override void OnKeyDown(KeyEventArgs e) => keyEventArgs = e;
		protected override void OnKeyUp(KeyEventArgs e) => keyEventArgs = e;

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			var wheelDelta = SystemInformation.MouseWheelScrollDelta;
			var detents = e.Delta / (wheelDelta) / 10.0f;
			var amp = keyEventArgs?.Shift == true ? 2 : 1;

			if (keyEventArgs?.Control == true && ScalePercentage + detents > 0)
			{
				ScalePercentage += detents * amp;
				Refresh();
				(e as HandledMouseEventArgs)?.Let(h => h.Handled = true);
			}

			Debug.WriteLine($"X: {e.X}  Y: {e.Y}  detents: {detents}  Delta: {e.Delta}  WHEEL_DELTA: {wheelDelta}  Location: {e.Location}  Zoom: {ScalePercentage}");
		}

		private void OnParentResize(object sender, EventArgs e) => Refresh();

		public void SetCanvasSize(int width, int height)
		{
			this.Canvas.Width = width;
			this.Canvas.Height = height;
			Refresh();
		}
	}
}
