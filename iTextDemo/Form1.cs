
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextImage = iText.Layout.Element.Image;

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

          addImage(document);

          // Add content to the document
          document.Add(new Paragraph("This is some sample content."));
        }
      }
    }
  }

  private void addImage(Document document, string imagePath = "70_80.jpg")
  {
    // Create text elements
    Text beforeImageText = new Text("This is some text before the image. ");
    Text afterImageText = new Text(" This is some text after the image.");

    // Load image
    ImageData imageData = ImageDataFactory.Create(imagePath);
    iTextImage image = new iTextImage(imageData);
    image.SetWidth(200 * image.GetImageWidth() / image.GetImageHeight());
    image.SetHeight(200);

    // Create a paragraph and add text and image inline
    Paragraph paragraph = new Paragraph()
        .Add(beforeImageText)
        .Add(image)
        .Add(afterImageText);

    // Add paragraph to the document
    document.Add(paragraph);
  }
}
