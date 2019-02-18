using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FontPreviewer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{

		}

		private void PreviewFonts()
		{
			var fonts = new List<FontFamily>();

			foreach (FontFamily font in System.Drawing.FontFamily.Families)
			{
				fonts.Add(font);
			}
			richTextBox.Clear();

			foreach (var fontFamily in fonts)
			////for (var i = 0; i < 5; i++)
			{
				//var fontFamily = fonts[i];
				richTextBox.SelectionFont = new Font(fontFamily, 20f);
				richTextBox.AppendText($"{fontFamily.Name}\n\t{textBox.Text}\n");
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			PreviewFonts();
		}
	}
}
