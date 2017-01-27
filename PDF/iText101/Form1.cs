using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iText = iTextSharp.text;

namespace iText101
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

			Document document = new Document();

			/* chapter06/PdfPTableSplit.java */
			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Graphics.pdf", FileMode.Create));

			document.Open();

			/* chapter10/InvisibleRectangles.java */
			PdfContentByte cb = writer.DirectContent;
			cb.MoveTo(30, 700);
			cb.LineTo(490, 700);
			cb.LineTo(490, 800);
			cb.LineTo(30, 800);
			cb.ClosePath();
			cb.Stroke();
			//cb.Rectangle(30, 700, 460, 100);
			//cb.Stroke();

			document.Close();

			webBrowser1.Navigate(string.Concat(appPath, "\\", @"Graphics.pdf"));
		}
	}
}
