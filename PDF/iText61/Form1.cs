using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Web.WebView2.WinForms;
using System.Reflection;

namespace iText61
{
    public partial class Form1 : Form
    {
        private WebView2 webView2;

        public Form1()
        {
            InitializeComponent();

            webView2 = new WebView2() { Dock = DockStyle.Fill };
            this.Controls.Add(webView2);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string? appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (appPath == null) return;

            string pdfPath = Path.Combine(appPath, "HelloWorld.pdf");

            using var writer = new PdfWriter(pdfPath);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            // Table with 3 columns
            Table table = new Table(3).UseAllAvailableWidth();
            table.SetWidth(UnitValue.CreatePercentValue(100));
            // Header cell with colspan 3
            Cell headerCell = new Cell(1, 3)
                .Add(new Paragraph("header with colspan 3"))
                .SetBackgroundColor(ColorConstants.RED)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
            table.AddHeaderCell(headerCell);

            table.AddCell("1.1");
            table.AddCell("2.1");
            table.AddCell("3.1");
            table.AddCell("1.2");
            table.AddCell("2.2");
            table.AddCell("3.2");

            document.Add(table);

            // Table with 50% width, left aligned
            Table tableLeft = new Table(3)
                .SetWidth(UnitValue.CreatePercentValue(50))
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);
            document.Add(tableLeft);

            // Table with 50% width, right aligned, custom column widths
            Table tableRight = new Table(new float[] { 2f, 1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(50))
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT)
                .SetMarginTop(10);
            document.Add(tableRight);

            // Add cell with centered alignment
            Paragraph p = new Paragraph("Quick brown fox jumps over the lazy dog. Quick brown fox jumps over the lazy dog.");
            table.AddCell("centered alignment");
            Cell centeredCell = new Cell().Add(p).SetTextAlignment(TextAlignment.CENTER);
            table.AddCell(centeredCell);
            table.AddCell(centeredCell.Clone(true));
            document.Add(table);

            await webView2.EnsureCoreWebView2Async(null);
            webView2.Source = new Uri(pdfPath);
        }
    }
}
