﻿using System;
using System.Windows.Forms;

namespace Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Launches the updater
			System.Diagnostics.Process.Start("TNT.Updater.exe");
		}
	}
}
