using iText.Kernel.Pdf;
using iText.Layout;

namespace iTextDemo
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // Create a PdfWriter object
      using (PdfWriter writer = new PdfWriter("output.pdf"))
      {
        // Create a PdfDocument object
        using (PdfDocument pdf = new PdfDocument(writer))
        {
          // Create a Document object
          using (Document document = new Document(pdf))
          {
            // Add content to the document
            document.Add(new iText.Layout.Element.Paragraph("Hello, iText!"));
          }
        }
      }
    }
  }
}
