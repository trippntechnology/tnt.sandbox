using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iTextDemo;

internal class StartPageHandler(Document document) : IEventHandler
{
  private Document document = document;

  public void HandleEvent(Event @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    PdfPage page = docEvent.GetPage();
    PdfDocument pdfDoc = docEvent.GetDocument();
    PageSize defaultPageSize = pdfDoc.GetDefaultPageSize();
    PdfCanvas pdfCanvas = new PdfCanvas(page);

    // Add the paragraph at the bottom of the page
    Paragraph bottomParagraph = new Paragraph("This is a paragraph at the top of the page.");

    // Calculate the position for the bottom paragraph
    float x = document.GetLeftMargin();
    float y = defaultPageSize.GetHeight() - document.GetTopMargin();
    float width = defaultPageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin();
    var pageNumber = pdfDoc.GetPageNumber(page);

    // Show text aligned at the bottom
    document.ShowTextAligned(bottomParagraph, x, y, pageNumber, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);

    // Draw a line in the header
    pdfCanvas.MoveTo(x, y)
      .LineTo(width, y)
      .ClosePathStroke();
  }
}
