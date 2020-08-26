using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using TNTDrawing.Converters;

namespace TNTDrawing
{
	/// <summary>
	/// Represents the grid area of the drawing surface
	/// </summary>
	[TypeConverter(typeof(GridTypeConverter))]
	public class Grid
	{
		/// <summary>
		/// Indicates whether <see cref="Draw(Graphics)"/> should be forced to draw or not
		/// </summary>
		protected bool ForceDraw = true;

		/// <summary>
		/// Backing field for <see cref="PixelsPerSegment"/>
		/// </summary>
		protected int _PixelsPerSegment = 10;

		/// <summary>
		/// <see cref="SolidBrush"/> used the paint the background of the <see cref="Grid"/>
		/// </summary>
		protected SolidBrush BackgroundBrush { get; set; } = new SolidBrush(Color.White);

		/// <summary>
		/// <see cref="Pen"/> used to draw the lines on the <see cref="Grid"/>
		/// </summary>
		protected Pen LinePen { get; set; } = new Pen(Color.Black);

		/// <summary>
		/// <see cref="SolidBrush"/> used to paint the shadow
		/// </summary>
		protected SolidBrush ShadowBrush { get; set; } = new SolidBrush(Color.FromArgb(40, Color.Black));

		/// <summary>
		/// <see cref="Rectangle"/> that represents the area of the <see cref="Grid"/>
		/// </summary>
		protected Rectangle _Rect = new Rectangle(0, 0, 1024, 768);

		/// <summary>
		/// The <see cref="Image"/> that is drawn by the grid
		/// </summary>
		protected Bitmap Image { get; set; }

		/// <summary>
		/// Delegate that is called when the <see cref="Grid"/> needs to be refreshed. 
		/// </summary>
		public Action OnRefreshRequest = () => { };

		/// <summary>
		/// The number of pixels between the line in the grid
		/// </summary>
		[Category("Layout")]
		public int PixelsPerSegment
		{
			get { return _PixelsPerSegment; }
			set
			{
				_PixelsPerSegment = value;
				Refresh();
			}
		}

		/// <summary>
		/// The color of the grid lines
		/// </summary>
		[Category("Appearance")]
		public Color LineColor
		{
			get { return LinePen.Color; }
			set
			{
				LinePen.Color = value;
				Refresh();
			}
		}

		/// <summary>
		/// The background color of the grid
		/// </summary>
		[Category("Appearance")]
		public Color BackgroundColor
		{
			get { return BackgroundBrush.Color; }
			set
			{
				BackgroundBrush.Color = value;
				Refresh();
			}
		}

		/// <summary>
		/// The shadow color
		/// </summary>
		[Category("Appearance")]
		public Color ShadowColor
		{
			get { return ShadowBrush.Color; }
			set
			{
				ShadowBrush.Color = value;
				Refresh();
			}
		}

		/// <summary>
		/// A <see cref="Rectangle"/> that represents the area of this <see cref="Grid"/>
		/// </summary>
		[Browsable(false)]
		public Rectangle Rect { get { return _Rect; } }

		/// <summary>
		/// The width of the grid
		/// </summary>
		[Category("Layout")]
		public int Width
		{
			get { return _Rect.Width; }
			set
			{
				_Rect.Width = value;
				Refresh();
			}
		}

		/// <summary>
		/// The height of the grid
		/// </summary>
		[Category("Layout")]
		public int Height
		{
			get { return _Rect.Height; }
			set
			{
				_Rect.Height = value;
				Refresh();
			}
		}

		/// <summary>
		/// Initializes the <see cref="Grid"/>
		/// </summary>
		public Grid(Color backgroundColor, Color lineColor, int pixelsBetweenLines)
		{
			PixelsPerSegment = pixelsBetweenLines;
			LineColor = lineColor;
			BackgroundBrush.Color = backgroundColor;
		}

		/// <summary>
		/// Draws the <see cref="Grid"/>
		/// </summary>
		public void Draw(Graphics graphics)
		{
			var canvasWidth = Rect.Width;
			var canvasHeight = Rect.Height;

			if (ForceDraw)// || (Image == null || Image.Width != canvasWidth || Image.Height != canvasHeight))
			{
				Debug.WriteLine($"Drawing image");
				var largeSegment = PixelsPerSegment * 10;
				Image = new Bitmap(canvasWidth+10, canvasHeight+10);
				var imageGraphics = Graphics.FromImage(Image);

				var shadowRect = Rectangle.Inflate(this.Rect, 0, 0);
				shadowRect.Offset(10, 10);
				imageGraphics.FillRectangle(ShadowBrush, shadowRect);
				imageGraphics.FillRectangle(BackgroundBrush, Rect);

				for (int x = 0; x < canvasWidth; x += PixelsPerSegment)
				{
					LinePen.Width = (x % largeSegment == 0) ? 3 : 1;
					imageGraphics.DrawLine(LinePen, x, 0, x, canvasHeight);
				}

				for (int y = 0; y < canvasHeight; y += PixelsPerSegment)
				{
					LinePen.Width = (y % largeSegment == 0) ? 3 : 1;
					imageGraphics.DrawLine(LinePen, 0, y, canvasWidth, y);
				}

				ForceDraw = false;
			}

			graphics.DrawImage(Image, Rect);
		}
		private void Refresh()
		{
			ForceDraw = true;
			OnRefreshRequest();
		}
	}
}
