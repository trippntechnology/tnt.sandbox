
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace iTextDemo;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();
    string uri = Path.Combine(AppContext.BaseDirectory, "PDFGeneratorTest.pdf");
    createPdf(uri);
    Browser.Navigate(uri);
  }

  private void createPdf(string filename)
  {
    // Create a PdfWriter object
    using (PdfWriter writer = new PdfWriter(filename))
    {
      // Create a PdfDocument object
      using (PdfDocument pdf = new PdfDocument(writer))
      {
        // Create a Document object
        using (Document document = new Document(pdf))
        {
          document.SetTopMargin(72);
          document.SetBottomMargin(72);

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
