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

namespace iText615
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
			PdfWriter.GetInstance(document, new FileStream("PersistantHeader.pdf", FileMode.Create));

			document.Open();

			/* chapter06/PdfPTableMemoryFriendly.java */
			PdfPTable table = new PdfPTable(2);
			table.WidthPercentage = 100;
			table.HeaderRows = 1;
			PdfPCell h1 = new PdfPCell(new Paragraph("Header 1"));
			h1.GrayFill = .7f;
			table.AddCell(h1);
			PdfPCell h2 = new PdfPCell(new Paragraph("Header 2"));
			h2.GrayFill = .7f;
			table.AddCell(h2);
			PdfPCell cell;

			for (int row = 1; row <= 2000; row++)
			{
				if (row % 50 == 0)
				{
					document.Add(table);
					table.DeleteBodyRows();
					table.SkipFirstHeader = true;
				}
				cell = new PdfPCell(new Paragraph(row.ToString()));
				table.AddCell(cell);
				cell = new PdfPCell(new Paragraph("Quick brown fox jumps over the lazy dog."));
				table.AddCell(cell);
			}

			document.Add(table);

			document.Close();

			webBrowser1.Navigate(string.Concat(appPath, "\\", @"PersistantHeader.pdf"));
		}
	}
}
