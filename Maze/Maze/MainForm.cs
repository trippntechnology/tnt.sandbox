using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Maze
{
	public partial class MainForm : Form
	{
		private Point START_POINT = new Point(10, 10);
		private Point? END_POINT = null;

		const int BOUNDARY_PADDING = 10;
		int LEFT_BOUNDARY = 10;
		int TOP_BOUNDARY = 10;
		int RIGHT_BOUNDARY = 1000;
		int BOTTOM_BOUNDARY = 1000;
		const int POINT_SPACING = 10;
		const int PATH_WIDTH = POINT_SPACING - 2;

		private Pen _Pen = new Pen(Color.White, PATH_WIDTH) { StartCap = LineCap.Round, EndCap = LineCap.Round };
		private Pen PathPen = new Pen(Color.Red, PATH_WIDTH - 4) { StartCap = LineCap.Round, EndCap = LineCap.Round };
		private Graphics _Graphics = null;
		private Random _Random = new Random();
		private Bitmap _bitmap;
		private Graphics bGraphics;

		Dictionary<Point, int> VisitedPoints = new Dictionary<Point, int>();
		Dictionary<Point, List<Point>> PointMap = new Dictionary<Point, List<Point>>();

		public MainForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_Graphics = panel1.CreateGraphics();
			_bitmap = new Bitmap(panel1.Width, panel1.Height);
			bGraphics = Graphics.FromImage(_bitmap);
			RIGHT_BOUNDARY = panel1.Width - BOUNDARY_PADDING;
			BOTTOM_BOUNDARY = panel1.Height - BOUNDARY_PADDING;
			bGraphics.FillRectangle(new SolidBrush(Color.Black), panel1.Bounds);
			_Graphics.FillRectangle(new SolidBrush(Color.Black), panel1.Bounds);
			VisitedPoints.Clear();
			PointMap.Clear();
			VisitedPoints.Add(START_POINT, 0);

			END_POINT = new Point(RIGHT_BOUNDARY / POINT_SPACING * POINT_SPACING, BOTTOM_BOUNDARY / POINT_SPACING * POINT_SPACING);

			label4.Text = END_POINT.ToString();

			DrawNextPoint(START_POINT);
		}

		private void DrawNextPoint(Point currentPoint)
		{
			if (!PointMap.TryGetValue(currentPoint, out List<Point> pointList))
			{
				pointList = new List<Point>();
				PointMap[currentPoint] = pointList;
			}

			var nextPoint = GetNextPoint(currentPoint);
			while (nextPoint != null)
			{
				pointList.Add((Point)nextPoint);
				//_Graphics.DrawLine(_Pen, currentPoint, (Point)nextPoint);
				bGraphics.DrawLine(_Pen, currentPoint, (Point)nextPoint);
				_Graphics.DrawLine(_Pen, currentPoint, (Point)nextPoint);
				Debug.WriteLine($"DrawTo: {nextPoint}");

				//Thread.Sleep(10);
				DrawNextPoint((Point)nextPoint);
				nextPoint = GetNextPoint(currentPoint);
			}

			//_Graphics.DrawImage(_bitmap, new Point(0, 0));
		}

		private Point? GetNextPoint(Point point)
		{
			if (END_POINT?.X == point.X && END_POINT?.Y == point.Y) return null;

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

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			var point = normalizePoint(e.Location);
			label1.Text = $"X = {point.X}   Y = {point.Y}";
		}

		private Point normalizePoint(Point point)
		{
			var x = ((point.X + (POINT_SPACING / 2)) / POINT_SPACING) * POINT_SPACING;
			var y = ((point.Y + (POINT_SPACING / 2)) / POINT_SPACING) * POINT_SPACING;
			return new Point(x, y);
		}

		private void panel1_Resize(object sender, EventArgs e)
		{
			RIGHT_BOUNDARY = panel1.Width - BOUNDARY_PADDING;
			BOTTOM_BOUNDARY = panel1.Height - BOUNDARY_PADDING * 2;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			// Get start points children
			var points = PointMap[START_POINT];
			_Graphics.DrawImage(_bitmap, new Point(0, 0));
			FindEndPoint(START_POINT);
		}

		private bool FindEndPoint(Point currentPoint)
		{
			if (currentPoint == this.END_POINT)
			{
				return true;
			}
			else
			{
				var foundEndPoint = false;

				PointMap[currentPoint].ForEach(p =>
				{
					if (!foundEndPoint && FindEndPoint(p))
					{
						_Graphics.DrawLine(PathPen, currentPoint, p);
						foundEndPoint = true;
					}
				});

				return foundEndPoint;
			}
		}
	}
}
