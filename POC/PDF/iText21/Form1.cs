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

namespace iText21
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(216f, 1000f);
			pageSize.BackgroundColor = BaseColor.BLUE;
			Document doc = new Document(pageSize, 72, 72, 144, 144);
//			Document doc = new Document(PageSize.LETTER);
//			Document doc = new Document(PageSize.LETTER.Rotate());

			PdfWriter.GetInstance(doc, new FileStream(@"HelloWorld.pdf", FileMode.Create));

			doc.AddTitle("Hello world example");
			doc.AddSubject("This example shows how to add metadata");
			doc.AddKeywords("Metadata, iText, step 3, tutorial");
			doc.AddCreator("My program using iText");
			doc.AddAuthor("Bruno Lowagie");
			doc.AddHeader("Expires", "0");
			doc.Open();

			doc.Add(new Paragraph("Hello World"));
			doc.Close();

			string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			webBrowser1.Navigate(string.Concat(appPath, "\\", @"HelloWorld.pdf"));
		}
	}
}
