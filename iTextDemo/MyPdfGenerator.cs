using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using iTextBorder = iText.Layout.Borders.Border;
using iTextColorConstants = iText.Kernel.Colors.ColorConstants;
using iTextImage = iText.Layout.Element.Image;

namespace iTextDemo;

internal class MyPdfGenerator : PdfGenerator
{
  class StartPageHandler(Document document) : BaseEventHandler(document)
  {
    protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
    {
      PageSize pageSize = pdfDocument.GetDefaultPageSize();
      PdfCanvas pdfCanvas = new PdfCanvas(pdfPage);

      Table table = new Table(2);

      Cell cell1 = new Cell().Add(new Paragraph("Paragraph 1 Text"))
        .SetBorder(Border.NO_BORDER)
        .SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.RED, 2f));
      table.AddCell(cell1);

      Cell cell2 = new Cell().Add(new Paragraph("Paragraph 2 Text"))
        .SetBorder(Border.NO_BORDER)
        .SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.RED, 2f))
        .SetTextAlignment(TextAlignment.RIGHT);
      table.AddCell(cell2);

      // Add the table to the page
      float x = document.GetLeftMargin();
      float y = pageSize.GetHeight() - document.GetTopMargin();
      table.SetFixedPosition(x, y, pageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin());

      PdfCanvas canvas = new PdfCanvas(pdfPage.NewContentStreamBefore(), pdfPage.GetResources(), pdfDocument);
      new Canvas(canvas, new iText.Kernel.Geom.Rectangle(0, 0, x, y)).Add(table);
    }
  }

  class EndPageHandler(Document document) : BaseEventHandler(document)
  {
    protected override void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument)
    {
      PdfCanvas pdfCanvas = new PdfCanvas(pdfPage);
      int pageNumber = pdfDocument.GetPageNumber(pdfPage);

      // Add the paragraph at the bottom of the page
      Paragraph bottomParagraph = new Paragraph($"This is a paragraph at the bottom of the page({pageNumber}).");

      // Calculate the position for the bottom paragraph
      float x = document.GetLeftMargin();
      float y = document.GetBottomMargin();
      float width = pdfDocument.GetDefaultPageSize().GetWidth() - document.GetRightMargin();

      // Show text aligned at the bottom
      document.ShowTextAligned(bottomParagraph, x, y, pageNumber, TextAlignment.LEFT, VerticalAlignment.TOP, 0);

      // Draw a line in the header
      pdfCanvas.MoveTo(x, y)
        .LineTo(width, y)
        .ClosePathStroke();
    }
  }

  public void Generate(string filename)
  {
    base.Generate(filename, (pdfDocument, document) =>
    {
      document.SetTopMargin(72);
      document.SetBottomMargin(72);

      // Define the header
      pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new StartPageHandler(document));

      // Define the footer
      pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new EndPageHandler(document));

      // Add content to the document
      document.Add(new Paragraph("Hello, iText!"));

      addImage(document);

      // Add content to the document
      document.Add(new Paragraph("This is some sample content."));

      document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
      document.Add(new Paragraph());

      createTable(document);
    });
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
