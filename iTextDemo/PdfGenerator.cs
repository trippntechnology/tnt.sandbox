using iText.Kernel.Pdf;
using iText.Layout;

namespace iTextDemo;

internal class PdfGenerator
{
  public virtual void Generate(string filename, Action<PdfDocument, Document> onGenerate)
  {
    using (PdfWriter pdfWriter = new PdfWriter(filename))
    {
      using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
      {
        using (Document document = new Document(pdfDocument))
        {
          onGenerate(pdfDocument, document);
        }
      }
    }
  }
}
