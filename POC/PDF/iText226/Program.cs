using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iText226
{
	class Program
	{
		static void Main(string[] args)
		{
			PdfReader reader = new PdfReader(args[1]);
			Document document = new Document(reader.GetPageSizeWithRotation(1));
			PdfCopy copy = new PdfCopy(document, new FileStream(args[0], FileMode.Create));
			document.Open();

			copy.AddPage(copy.GetImportedPage(reader, 1));

			for (int i = 2; i < args.Length; i++)
			{
				reader = new PdfReader(args[i]);
				copy.AddPage(copy.GetImportedPage(reader, 1));
			}
			document.Close();
		}
	}
}
