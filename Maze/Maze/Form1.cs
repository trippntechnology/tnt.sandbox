using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Maze
{
	public partial class Form1 : Form
	{
		const int BOUNDARY_PADDING = 10;
		int LEFT_BOUNDARY = 10;
		int TOP_BOUNDARY = 10;
		int RIGHT_BOUNDARY = 1000;
		int BOTTOM_BOUNDARY = 1000;
		const int POINT_SPACING = 10;
		const int PATH_WIDTH = POINT_SPACING - 2;

		private Pen _Pen = new Pen(Color.White, PATH_WIDTH) { StartCap = LineCap.Round, EndCap = LineCap.Round };
		private Graphics _Graphics = null;
		private Random _Random = new Random();
		private Bitmap _bitmap;
		private Graphics bGraphics;

		Dictionary<Point, int> VisitedPoints = new Dictionary<Point, int>();

		public Form1()
		{
			InitializeComponent();
			_Graphics = panel1.CreateGraphics();
			_bitmap = new Bitmap(panel1.Width, panel1.Height);
			bGraphics = Graphics.FromImage(_bitmap);
			RIGHT_BOUNDARY = panel1.Width - BOUNDARY_PADDING;
			BOTTOM_BOUNDARY = panel1.Height - BOUNDARY_PADDING;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var point = new Point(10, 10);
			VisitedPoints.Add(point, 0);
			DrawNextPoint(point);
		}

		private void DrawNextPoint(Point currentPoint)
		{
			var nextPoint = GetNextPoint(currentPoint);
			while (nextPoint != null)
			{
				//_Graphics.DrawLine(_Pen, currentPoint, (Point)nextPoint);
				bGraphics.DrawLine(_Pen, currentPoint, (Point)nextPoint);
				Debug.WriteLine($"DrawTo: {nextPoint}");
				//Thread.Sleep(10);
				DrawNextPoint((Point)nextPoint);
				nextPoint = GetNextPoint(currentPoint);
			}

			_Graphics.DrawImage(_bitmap, new Point(0, 0));
		}

		private Point? GetNextPoint(Point point)
		{
			var availableDirections = new List<Direction>(new Direction[] { Direction.DOWN, Direction.LEFT, Direction.RIGHT, Direction.UP });

			while (availableDirections.Count > 0)
			{
				var direction = availableDirections[_Random.Next(availableDirections.Count)];
				Debug.WriteLine($"Direction = {direction}");

				var newPoint = MoveByDirection(point, direction);

				availableDirections.Remove(direction);

				if (newPoint.X < LEFT_BOUNDARY || newPoint.Y < TOP_BOUNDARY || newPoint.X > RIGHT_BOUNDARY || newPoint.Y > BOTTOM_BOUNDARY)
				{
					Debug.WriteLine("Point is out of bounds");
				}
				else if (VisitedPoints.ContainsKey(newPoint))
				{
					Debug.WriteLine($"Point, {newPoint}, already visited");
				}
				else //if (!VisitedPoints.ContainsKey(newPoint))
				{
					VisitedPoints.Add(newPoint, 0);
					return newPoint;
				}
			}

			return null;
		}

		private Point MoveByDirection(Point point, Direction direction)
		{
			switch (direction)
			{
				case Direction.LEFT:
					return new Point(point.X - POINT_SPACING, point.Y);
				case Direction.UP:
					return new Point(point.X, point.Y - POINT_SPACING);
				case Direction.RIGHT:
					return new Point(point.X + POINT_SPACING, point.Y);
				default: // Direction.DOWN
					return new Point(point.X, point.Y + POINT_SPACING);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				_bitmap.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
			}
		}
	}
}
