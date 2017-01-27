using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using iText = iTextSharp.text;

namespace iText51
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

			iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(216f, 1000f);
			Document document = new Document();

			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"HelloWorld.pdf", FileMode.Create));

			document.Open();

			/* chapter05/FoxDogImageTypes.java */
			iText.Image img1 = iText.Image.GetInstance(iText.Image.GetInstance(string.Concat(appPath, "\\", "fox.jpg")));
			img1.Alignment = iText.Image.ALIGN_MIDDLE;
			img1.ScaleAbsolute(100f, 100f);
			document.Add(img1);

			/* chapter05/Barcodes.java */
			PdfContentByte cb = writer.DirectContent;
			document.Add(new Paragraph("Barcode 3 of 9"));
			Barcode39 code39 = new Barcode39();
			code39.Code = "ITEXT IN ACTION";
			document.Add(code39.CreateImageWithBarcode(cb, null, null));

			document.Close();

			webBrowser1.Navigate(string.Concat(appPath, "\\", @"HelloWorld.pdf"));

		}
	}
}
