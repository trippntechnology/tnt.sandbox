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
		private const int MINIMUM_PADDING = 100;

		private int _ScalePercentage = 100;
		private readonly Brush RedBrush = new SolidBrush(Color.Red);
		private KeyEventArgs keyEventArgs = null;

		/// <summary>
		/// The backgrond of the <see cref="Canvas"/>
		/// </summary>
		public Grid Grid { get; set; } = new Grid(Color.White, Color.Aqua, 10);

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
		/// Initializes a <see cref="Canvas"/>
		/// </summary>
		public Canvas(Control parent, int left, int top, int width, int height)
			: base(parent, string.Empty, left, top, width, height)
		{
			parent.SizeChanged += OnParentResize;
			// Enable scrolling on parent
			(Parent as ScrollableControl)?.Also(f => f.AutoScroll = true);
			// Call Refresh when Grid requests it
			Grid.OnRefreshRequest = () => { Refresh(); };
		}

		/// <summary>
		/// Draws the grid on the canvas
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{
			Debug.WriteLine($"OnPaint");

			var graphics = e.Graphics;

			// Convert scale to float
			var scale = ScalePercentage / 100F;

			// Get scaled dimensions of the drawing canvas
			var scaledGridWidth = Grid.Width * scale;
			var scaledGridHeight = Grid.Height * scale;

			UpdateClientDimensions(scaledGridWidth, scaledGridHeight);

			Debug.WriteLine($"ScaledPercentage: {ScalePercentage}  Width: {Width}  Height: {Height}");
			Debug.WriteLine($"ScaledWidth: {scaledGridWidth}  ScaledHeight: {scaledGridHeight}");

			// X translation of the drawing  canvas
			var xTranslation = Math.Max((Width - scaledGridWidth) / 2, 0);
			// Y translation of the drawing canvas
			var yTranslation = Math.Max((Height - scaledGridHeight) / 2, 0);

			Debug.WriteLine($"xTranslation: {xTranslation}  yTranslation: {yTranslation}");

			// Scale
			graphics.ScaleTransform(scale, scale, MatrixOrder.Append);
			// Transform
			graphics.TranslateTransform(xTranslation, yTranslation, MatrixOrder.Append);

			Grid.Draw(graphics);

			Debug.WriteLine($"Grid.Rect: {Grid.Rect}");

			graphics.FillRectangle(RedBrush, 33, 33, 99, 99);

			base.OnPaint(e);
		}

		/// <summary>
		/// Updates the client's dimensions
		/// </summary>
		private void UpdateClientDimensions(float scaledGridWidth, float scaledGridHeight)
		{
			// Adjust the width and height of the canvas to fit the drawing canvas
			var newWidth = (int)scaledGridWidth > Parent.ClientRectangle.Width ? (int)scaledGridWidth : Parent.ClientRectangle.Width;
			var newHeight = (int)scaledGridHeight > Parent.ClientRectangle.Height ? (int)scaledGridHeight : Parent.ClientRectangle.Height;

			Width = newWidth + MINIMUM_PADDING;
			Height = newHeight + MINIMUM_PADDING;
		}

		/// <summary>
		/// Focus this control when mouse movements are detected
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
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
		protected override void OnKeyUp(KeyEventArgs e) => keyEventArgs = e;

		/// <summary>
		/// Changes <see cref="ScalePercentage"/>
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			var scale = ScalePercentage / 100F;
			// Size of a scroll delta (seems to be 120)
			var wheelDelta = SystemInformation.MouseWheelScrollDelta;
			// Change, smaller (-1) or larger (1)
			var change = e.Delta / wheelDelta;
			// Amount of change that should be applied to the scale percentage
			var detents = change / (keyEventArgs?.Shift == true ? 100.0f : 10.0f);

			if (keyEventArgs?.Control == true && scale + detents > 0)
			{
				ScalePercentage = (int)((scale + detents) * 100);
				(e as HandledMouseEventArgs)?.Let(h => h.Handled = true);
			}

			Debug.WriteLine($"X: {e.X}  Y: {e.Y}  detents: {detents}  Delta: {e.Delta}  WHEEL_DELTA: {wheelDelta}  Location: {e.Location}  Zoom: {ScalePercentage}");
		}

		/// <summary>
		/// Redraws <see cref="Canvas"/> when the parent's size changes.
		/// </summary>
		private void OnParentResize(object sender, EventArgs e) => Refresh();
	}
}
