using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iTextDemo;

class EndPageHandler(Document document) : IEventHandler
{
  private Document document = document;

  public void HandleEvent(Event @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    PdfPage page = docEvent.GetPage();
    PdfDocument pdfDoc = docEvent.GetDocument();
    PdfCanvas pdfCanvas = new PdfCanvas(page);
    int pageNumber = pdfDoc.GetPageNumber(page);

    // Add the paragraph at the bottom of the page
    Paragraph bottomParagraph = new Paragraph($"This is a paragraph at the bottom of the page({pageNumber}).");

    // Calculate the position for the bottom paragraph
    float x = document.GetLeftMargin();
    float y = document.GetBottomMargin();
    float width = pdfDoc.GetDefaultPageSize().GetWidth() - document.GetRightMargin();

    // Show text aligned at the bottom
    document.ShowTextAligned(bottomParagraph, x, y, pageNumber, TextAlignment.LEFT, VerticalAlignment.TOP, 0);

    // Draw a line in the header
    pdfCanvas.MoveTo(x, y)
      .LineTo(width, y)
      .ClosePathStroke();
  }
}
