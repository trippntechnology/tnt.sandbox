using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Encryption
{
	class Program
	{
		static void Main(string[] args)
		{
			Document document = new Document();

			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("HelloWorldEncrypted.pdf", FileMode.Create));

			writer.SetEncryption(PdfWriter.STRENGTH128BITS, "Hello", "World", PdfWriter.AllowCopy | PdfWriter.AllowPrinting);

			document.Open();

			document.Add(new Paragraph("This is the text"));
			document.Close();
		}
	}
}
