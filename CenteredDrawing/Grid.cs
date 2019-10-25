using System.Diagnostics;
using System.Drawing;

namespace CenteredDrawing
{
	public class Grid
	{
		public int Pixels { get; set; }

		public Pen LinePen { get; set; }
		public Brush BackgroundBrush { get; set; }

		public Bitmap Image { get; set; }

		public Grid(Color backgroundColor, Color lineColor, int pixelsBetweenLines)
		{
			this.BackgroundBrush = new SolidBrush(backgroundColor);
			this.LinePen = new Pen(lineColor);
			this.Pixels = pixelsBetweenLines;
		}

		public void Draw(Graphics graphics, Rectangle canvas)
		{
			var canvasWidth = canvas.Width;
			var canvasHeight = canvas.Height;

			if (this.Image == null || this.Image.Width != canvasWidth || this.Image.Height != canvasHeight)
			{
				Debug.WriteLine($"Drawing image");
				this.Image = new Bitmap(canvasWidth, canvasHeight);
				var imageGraphics = Graphics.FromImage(this.Image);

				imageGraphics.FillRectangle(BackgroundBrush, canvas);

				for (int x = 0; x < canvasWidth; x += Pixels)
				{
					LinePen.Width = (x % 100 == 0) ? 3 : 1;
					imageGraphics.DrawLine(LinePen, x, 0, x, canvasHeight);
				}

				for (int y = 0; y < canvasHeight; y += Pixels)
				{
					LinePen.Width = (y % 100 == 0) ? 3 : 1;
					imageGraphics.DrawLine(LinePen, 0, y, canvasWidth, y);
				}
			}

			graphics.DrawImage(this.Image, canvas);
		}
	}
}
