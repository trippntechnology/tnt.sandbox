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

namespace iText61
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

			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"HelloWorld.pdf", FileMode.Create));

			document.Open();

			/* chapter06/MyFirstPdfPTable.java */
			PdfPTable table = new PdfPTable(3);
			table.WidthPercentage = 100;
			table.DefaultCell.Border = PdfPCell.NO_BORDER;
			table.DefaultCell.BackgroundColor = BaseColor.RED;
			PdfPCell cell = new PdfPCell(new Paragraph("header with colspan 3"));
			cell.Colspan = 3;
			table.AddCell(cell);
			table.AddCell("1.1");
			table.AddCell("2.1");
			table.AddCell("3.1");
			table.AddCell("1.2");
			table.AddCell("2.2");
			table.AddCell("3.2");
			document.Add(table);

			table.WidthPercentage = 50;
			table.HorizontalAlignment = Element.ALIGN_LEFT;
			document.Add(table);

			table.WidthPercentage = 50;
			table.HorizontalAlignment = Element.ALIGN_RIGHT;
			float[] widths = { 2f, 1f, 1f };
			table.SetWidths(widths);
			table.SpacingBefore = 1f;
			document.Add(table);

			/* chapter06/PdfPTableCellAlignment.java */
			Paragraph p = new Paragraph("Quick brown fox jumps over the lazy dog. Quick brown fox jumps over the lazy dog.");
			table.AddCell("centered alignment");
			cell = new PdfPCell(p);
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			table.AddCell(cell);
			table.AddCell(cell);
			document.Add(table);

			document.Close();

			webBrowser1.Navigate(string.Concat(appPath, "\\", @"HelloWorld.pdf"));


		}
	}
}
