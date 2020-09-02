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

		private int _ScalePercentage = 100;
		private Point CurrentMousePosition = Point.Empty;
		private readonly Brush RedBrush = new SolidBrush(Color.Red);
		private KeyEventArgs keyEventArgs = null;
		private ScrollableControl ScrollableParent = null;
		private Point PositionOnGrid;
		private bool AdjustPostion = false;

		/// <summary>
		/// The backgrond of the <see cref="Canvas"/>
		/// </summary>
		public Grid Grid { get; set; } = new Grid(Color.White, Color.Aqua, 10);

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
			graphics.FillEllipse(RedBrush, new Rectangle(point.Subtract(new Point(2, 2)), new Size(4, 4)));

			if (AdjustPostion) RepositionToAlignWithMouse(PointToClient(Cursor.Position), graphics);

			base.OnPaint(e);
		}

		private void RepositionToAlignWithMouse(Point canvasPosition, Graphics graphics)
		{
			var prevPositionOnGrid = PositionOnGrid;
			PositionOnGrid = canvasPosition.ToGridCoordinates(graphics);
			var prevCanvasPosition = prevPositionOnGrid.ToCanvasCoordinates(graphics);
			var xDelta = prevPositionOnGrid.X - PositionOnGrid.X;
			var yDelta = prevPositionOnGrid.Y - PositionOnGrid.Y;
			Debug.WriteLine($@"prevPositionOnGrid: {prevPositionOnGrid} PositionOnGrid: {PositionOnGrid}  prevCanvasPosition: {prevCanvasPosition} canvasPosition: {canvasPosition}  xDelta: {xDelta}  yDelta: {yDelta}");
			ScrollableParent.HorizontalScroll.ChangeBy(xDelta);
			ScrollableParent.VerticalScroll.ChangeBy(yDelta);
			AdjustPostion = false;
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
			var cursorPosition = Cursor.Position;
			var graphics = GetTransformedGraphics();
			var origPoint = new Point(e.X, e.Y);
			var prevPositionOnGrid = PositionOnGrid;
			PositionOnGrid = origPoint.ToGridCoordinates(graphics);

			if (keyEventArgs?.KeyCode == Keys.Space)
			{
				var xDelta = CurrentMousePosition.X - cursorPosition.X;
				var yDelta = CurrentMousePosition.Y - cursorPosition.Y;
				ScrollableParent.HorizontalScroll.ChangeBy(xDelta);
				ScrollableParent.VerticalScroll.ChangeBy(yDelta);
			}

			CurrentMousePosition.X = cursorPosition.X;
			CurrentMousePosition.Y = cursorPosition.Y;

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
				AdjustPostion = true;
				var graphics = GetTransformedGraphics();

				var positionOnCanvas = new Point(e.X, e.Y);
				PositionOnGrid = positionOnCanvas.ToGridCoordinates(graphics);
				Debug.WriteLine($@"[OnMouseWheel] positionOnCanvas: {positionOnCanvas}  PositionOnGrid: {PositionOnGrid}");
				ScalePercentage += detents;
				(e as HandledMouseEventArgs)?.Let(h => h.Handled = true);
			}

			//Debug.WriteLine($"X: {e.X}  Y: {e.Y}  change: {change}  detents: {detents}  Delta: {e.Delta}  WHEEL_DELTA: {wheelDelta}  Location: {e.Location}  ScalePercentage: {ScalePercentage}");
		}

		/// <summary>
		/// Redraws <see cref="Canvas"/> when the parent's size changes.
		/// </summary>
		private void OnParentResize(object sender, EventArgs e) => Refresh();

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
