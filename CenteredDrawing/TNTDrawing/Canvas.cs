using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TNTDrawing
{
	/// <summary>
	/// <see cref="Control"/> that provides a grid that can be used to draw on
	/// </summary>
	public class Canvas : Control
	{
		private const int MINIMUM_PADDING = 1000;

		private readonly ScrollableControl ScrollableParent = null;
		private readonly Brush BlackBrush = new SolidBrush(Color.Black);

		private int _ScalePercentage = 100;
		private Point PreviousCursorPosition = Point.Empty;
		private KeyEventArgs keyEventArgs = null;
		private Point PreviousGridPosition;
		private bool AdjustPostion = false;

		/// <summary>
		/// The backgrond of the <see cref="Canvas"/>
		/// </summary>
		public Grid Grid { get; set; } = new Grid(Color.White, Color.Aqua, 10);

		public void Fit()
		{
			// Determine which is dimension is greater than parent
			var exceededWidth = ScrollableParent.Width - Grid.Width;
			var exceededHeight = ScrollableParent.Height - Grid.Height;

			if(exceededWidth < 0 && exceededHeight < 0)
			{
				// Both are bigger
			}

			ScalePercentage = 100;
		}

		/// <summary>
		/// The <see cref="ScalePercentage"/> represented as a <see cref="float"/>
		/// </summary>
		public float Zoom { get { return ScalePercentage / 100F; } }

		/// <summary>
		/// Amount the <see cref="Canvas"/> should be scaled
		/// </summary>
		[Category("Appearance")]
		public int ScalePercentage
		{
			get { return _ScalePercentage; }
			set
			{
				_ScalePercentage = value;
				Refresh();
			}
		}

		/// <summary>
		/// Scaled grid width
		/// </summary>
		public float ScaledWidth { get { return Grid.Width * Zoom; } }

		/// <summary>
		/// Scaled grid height
		/// </summary>
		public float ScaledHeight { get { return Grid.Height * Zoom; } }

		/// <summary>
		/// Initializes a <see cref="Canvas"/>
		/// </summary>
		public Canvas(Control parent, int left, int top, int width, int height)
			: base(parent, string.Empty, left, top, width, height)
		{
			parent.SizeChanged += OnParentResize;
			// Enable scrolling on parent
			ScrollableParent = Parent as ScrollableControl;
			ScrollableParent.AutoScroll = true;
			// Call Refresh when Grid requests it
			Grid.OnRefreshRequest = () => { Refresh(); };
		}

		/// <summary>
		/// Draws the grid on the canvas
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{
			Debug.WriteLine($"OnPaint AdjustPostion: {AdjustPostion}");
			UpdateClientDimensions();
			var graphics = GetTransformedGraphics(e.Graphics);

			Grid.Draw(graphics);

			var point = new Point(300, 300);
			graphics.FillEllipse(BlackBrush, new Rectangle(point.Subtract(new Point(4, 4)), new Size(8, 8)));

			if (AdjustPostion)
			{
				var previousCanvasPosition = PreviousGridPosition.ToCanvasCoordinates(graphics);
				var currentCanvasPosition = PointToClient(Cursor.Position);
				RepositionToAlignWithMouse(previousCanvasPosition, currentCanvasPosition);
				AdjustPostion = false;
			}

			base.OnPaint(e);
		}

		private void RepositionToAlignWithMouse(Point previousPosition, Point currentPosition)
		{
			var deltaPosition = previousPosition.Subtract(currentPosition);
			ScrollableParent.HorizontalScroll.ChangeBy(deltaPosition.X);
			ScrollableParent.VerticalScroll.ChangeBy(deltaPosition.Y);
		}

		/// <summary>
		/// Updates the client's dimensions
		/// </summary>
		private void UpdateClientDimensions()
		{
			// Adjust the width and height of the canvas to fit the drawing canvas
			var newWidth = (int)ScaledWidth > Parent.ClientRectangle.Width ? (int)ScaledWidth : Parent.ClientRectangle.Width;
			var newHeight = (int)ScaledHeight > Parent.ClientRectangle.Height ? (int)ScaledHeight : Parent.ClientRectangle.Height;

			Width = newWidth + MINIMUM_PADDING;
			Height = newHeight + MINIMUM_PADDING;
		}

		/// <summary>
		/// Focus this control when mouse movements are detected
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			var currentCursorPosition = Cursor.Position;
			var graphics = GetTransformedGraphics();
			var mousePosition = new Point(e.X, e.Y);
			PreviousGridPosition = mousePosition.ToGridCoordinates(graphics);

			if (keyEventArgs?.KeyCode == Keys.Space)
			{
				RepositionToAlignWithMouse(PreviousCursorPosition, currentCursorPosition);
			}

			PreviousCursorPosition = currentCursorPosition;

			Focus();
			base.OnMouseMove(e);
		}

		/// <summary>
		/// Sets <see cref="KeyEventArgs"/>
		/// </summary>
		protected override void OnKeyDown(KeyEventArgs e) => keyEventArgs = e;

		/// <summary>
		/// Sets <see cref="KeyEventArgs"/>
		/// </summary>
		protected override void OnKeyUp(KeyEventArgs e) => keyEventArgs = null;

		/// <summary>
		/// Changes <see cref="ScalePercentage"/>
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			// Size of a scroll delta (seems to be 120)
			var wheelDelta = SystemInformation.MouseWheelScrollDelta;
			// Change, smaller (-1) or larger (1)
			var change = e.Delta / wheelDelta;
			// Amount of change that should be applied to the scale percentage
			//var detents = change / (keyEventArgs?.Shift == true ? 100.0f : 10.0f);
			var detents = change * (keyEventArgs?.Shift == true ? 1 : 10);

			if (keyEventArgs?.Control == true && ScalePercentage + detents > 0)
			{
				// Adjust position when Paint() is called
				AdjustPostion = true;
				var graphics = GetTransformedGraphics();
				var positionOnCanvas = new Point(e.X, e.Y);
				PreviousGridPosition = positionOnCanvas.ToGridCoordinates(graphics);
				Debug.WriteLine($@"[OnMouseWheel] positionOnCanvas: {positionOnCanvas}  PositionOnGrid: {PreviousGridPosition}");
				ScalePercentage += detents;
				(e as HandledMouseEventArgs)?.Let(h => h.Handled = true);
			}
		}

		/// <summary>
		/// Redraws <see cref="Canvas"/> when the parent's size changes.
		/// </summary>
		private void OnParentResize(object sender, EventArgs e) => Refresh();

		/// <summary>
		/// Returns a <see cref="Graphics"/> that has been transformed
		/// </summary>
		private Graphics GetTransformedGraphics(Graphics graphics = null)
		{
			if (graphics == null) graphics = CreateGraphics();

			// X translation of the drawing  canvas
			var xTranslation = Math.Max((Width - ScaledWidth) / 2, 0);
			// Y translation of the drawing canvas
			var yTranslation = Math.Max((Height - ScaledHeight) / 2, 0);

			// Scale
			graphics.ScaleTransform(Zoom, Zoom, MatrixOrder.Append);
			// Transform
			graphics.TranslateTransform(xTranslation, yTranslation, MatrixOrder.Append);

			return graphics;
		}
	}
}
