using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CreateByteArray
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Document doc = new Document(PageSize.LETTER, 72, 72, 144, 144);
			//			Document doc = new Document(PageSize.LETTER);
			//			Document doc = new Document(PageSize.LETTER.Rotate());

			MemoryStream ms =new MemoryStream();
			PdfWriter.GetInstance(doc, ms);// new FileStream(@"HelloWorld.pdf", FileMode.Create));

			doc.AddTitle("Hello world example");
			doc.AddSubject("This example shows how to add metadata");
			doc.AddKeywords("Metadata, iText, step 3, tutorial");
			doc.AddCreator("My program using iText");
			doc.AddAuthor("Bruno Lowagie");
			doc.AddHeader("Expires", "0");
			doc.Open();

			doc.Add(new Paragraph("Hello World. Here I come."));
			doc.Close();

			string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			string fileName = string.Concat(appPath, "\\", @"HelloWorld.pdf");

			byte[] bytes = ms.GetBuffer();
			File.WriteAllBytes(@"HelloWorld.pdf", bytes);
			webBrowser1.Navigate(fileName);
		}
	}
}
