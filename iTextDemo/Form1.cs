namespace iTextDemo;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();
    string uri = Path.Combine(AppContext.BaseDirectory, "PDFGeneratorTest.pdf");
    new MyPdfGenerator().Generate(uri);
    Browser.Navigate(uri);
  }
}
