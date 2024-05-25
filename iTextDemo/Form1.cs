
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBorder = iText.Layout.Borders.Border;
using iTextColorConstants = iText.Kernel.Colors.ColorConstants;
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

          document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
          document.Add(new Paragraph());

          createTable(document);
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

  private void createTable(Document document)
  {
    // Create a table with 3 columns
    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 1 })).UseAllAvailableWidth();
    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

    // Add header cells
    for (int col = 0; col < 3; col++)
    {
      var cell = new Cell()
        .Add(new Paragraph($"Header {col}")
          .SetFont(boldFont))
        .SetBackgroundColor(iTextColorConstants.GRAY);

      table.AddHeaderCell(cell);

    }

    // Add rows of cells
    for (int row = 0; row < 40; row++)
    {
      for (int col = 0; col < 3; col++)
      {
        var cell = new Cell()
          .Add(new Paragraph($"Row {row}, Col {col}"))
          .SetBorder(iTextBorder.NO_BORDER);

        if (row % 2 == 0)
        {
          cell?.SetBackgroundColor(iTextColorConstants.LIGHT_GRAY);
        }
        table.AddCell(cell);
      }
    }

    // Add table to the document
    document.Add(table);
  }
}
