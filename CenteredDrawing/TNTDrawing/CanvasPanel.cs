﻿using System.Drawing;
using System.Windows.Forms;

namespace TNTDrawing
{
	/// <summary>
	/// Parent <see cref="Panel"/> for the <see cref="Canvas"/>. This was added in order to control scrolling 
	/// reset when focused
	/// </summary>
	public class CanvasPanel : Panel
	{
		/// <summary>
		/// The <see cref="Canvas"/>
		/// </summary>
		public Canvas Canvas { get; set; }

		/// <summary>
		/// Initializes <see cref="CanvasPanel"/>
		/// </summary>
		public CanvasPanel(Control parent)
			: base()
		{
			Dock = DockStyle.Fill;
			Canvas = new Canvas(this, 0, 0, Width, Height);
			Canvas.BackColor = Color.Yellow;
			//Canvas.MouseMove += _MyControl_MouseMove;
		}

		/// <summary>
		/// Prevents scroll from resetting when it gets focus
		/// </summary>
		protected override Point ScrollToControl(Control activeControl) => DisplayRectangle.Location;
	}
}
