using iText.Commons.Actions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Event;
using iText.Layout;

namespace iTextDemo;

internal abstract class BaseEventHandler(Document document) : AbstractPdfDocumentEventHandler
{
  protected readonly Document document = document;

  override public void OnEvent(IEvent @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    HandleEvent(docEvent, docEvent.GetPage(), docEvent.GetDocument());
  }

  protected abstract void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage? pdfPage, PdfDocument pdfDocument);
}
