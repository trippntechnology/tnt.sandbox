using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;

namespace iTextDemo;

internal abstract class BaseEventHandler(Document document) : IEventHandler
{
  protected readonly Document document = document;

  public virtual void HandleEvent(Event @event)
  {
    PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    HandleEvent(docEvent, docEvent.GetPage(), docEvent.GetDocument());
  }

  protected abstract void HandleEvent(PdfDocumentEvent pdfDocumentEvent, PdfPage pdfPage, PdfDocument pdfDocument);
}
