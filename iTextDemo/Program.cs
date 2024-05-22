using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

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
            // Define the header
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new StartPageHandler(document));

            // Define the footer
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageHandler(document));

            // Add content to the document
            document.Add(new Paragraph("Hello, iText!"));

            // Add content to the document
            document.Add(new Paragraph("This is some sample content."));
          }
        }
      }
    }
  }
}
