using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iText = iTextSharp.text;

namespace iText41
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			iText.Rectangle pageSize = new iText.Rectangle(216f, 1000f);
			pageSize.BackgroundColor = BaseColor.BLUE;
			Document document = new Document();
			//			Document document = new Document(pageSize, 72, 72, 144, 144);
			//			Document doc = new Document(PageSize.LETTER);
			//			Document doc = new Document(PageSize.LETTER.Rotate());

			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"HelloWorld.pdf", FileMode.Create));
			writer.PageEvent = new FoxDogGeneric1();

			document.AddTitle("Hello world example");
			document.AddSubject("This example shows how to add metadata");
			document.AddKeywords("Metadata, iText, step 3, tutorial");
			document.AddCreator("My program using iText");
			document.AddAuthor("Bruno Lowagie");
			document.AddHeader("Expires", "0");
			document.Open();

			/* chapter04/FoxDogChunk1.java */
			document.Add(new Paragraph("This is an example of chunks"));
			iText.Font font = new iText.Font(iText.Font.FontFamily.COURIER, 10, iText.Font.BOLD);
			font.SetColor(0xFF, 0xFF, 0xFF);
			Chunk fox = new Chunk("quick brown fox", font);
			fox.SetBackground(new BaseColor(0xa5, 0x2a, 0x2a));
			Chunk jumps = new Chunk(" jumps over ", new iText.Font());
			Chunk dog = new Chunk("the lazy dog", new iText.Font(iText.Font.FontFamily.TIMES_ROMAN, 14, iText.Font.ITALIC));
			document.Add(fox);
			document.Add(jumps);
			document.Add(dog);

			document.Add(new Paragraph("This is an example of a Phrase"));

			/* chapter04/FoxDogPhrase.java */
			Phrase phrase = new Phrase(30);
			Chunk space = new Chunk(' ');
			phrase.Add(fox);
			phrase.Add(jumps);
			phrase.Add(dog);
			phrase.Add(space);

			for (int i = 0; i < 10; i++)
				document.Add(phrase);

			document.Add(new Paragraph("This is an example of a Paragraph"));

			/* chapter04/FoxDogParagraph.java */
			String text = "Quick brown fox jumps over the lazy dog.";
			Phrase phrase1 = new Phrase(text);
			Phrase phrase2 = new Phrase(new Chunk(text, new iText.Font(iText.Font.FontFamily.TIMES_ROMAN)));
			Phrase phrase3 = new Phrase(text, new iText.Font(iText.Font.FontFamily.COURIER));
			Paragraph paragraph = new Paragraph();
			paragraph.Add(phrase1);
			paragraph.Add(space);
			paragraph.Add(phrase2);
			paragraph.Add(space);
			paragraph.Add(phrase3);
			paragraph.Alignment = Element.ALIGN_RIGHT;
			paragraph.IndentationLeft = 20;
			document.Add(paragraph);
			document.Add(paragraph);

			document.Add(new Paragraph("This is an example of a link. Click the text below."));

			/* chapter04/FoxDogAnchor1.java */
			Anchor anchor = new Anchor("Quick brown fox jumps over the lazy dog.");
			anchor.Reference = "http://en.wikipedia.org/wiki/The_quick_brown_fox_jumps_over_the_lazy_dog";
			document.Add(anchor);

			document.Add(new Paragraph("This is an example of some lists"));

			/* chapter04/FoxDogList1.java */
			List list1 = new List(List.ORDERED, 20);
			list1.Add(new ListItem("the lazy dog"));
			document.Add(list1);
			List list2 = new List(List.UNORDERED, 10);
			list2.Add("the lazy cat");
			document.Add(list2);
			List list3 = new List(List.ORDERED, List.ALPHABETICAL, 20);
			list3.Add(new ListItem("the fence"));
			document.Add(list3);
			List list4 = new List(List.UNORDERED, 30);
			list4.SetListSymbol("----->");
			list4.IndentationLeft = 10;
			list4.Add("the lazy dog");
			document.Add(list4);
			List list5 = new List(List.ORDERED, 20);
			list5.First = 11;
			list5.Add(new ListItem("the lazy cat"));
			document.Add(list5);
			List list = new List(List.UNORDERED, 10);
			list.SetListSymbol("*");
			list.Add(list1);
			list.Add(list3);
			list.Add(list5);
			document.Add(list);

			/* chapter04/FoxDogChapter1.java */
			Chapter chapter1 = new Chapter(new Paragraph("This is a sample sentence:", font), 1);
			chapter1.Add(new Paragraph(text));
			Section section1 = chapter1.AddSection("Quick", 0);
			section1.Add(new Paragraph(text));
			document.Add(chapter1);

			/* chapter04/FoxDogScale.java */
			Chunk c = new Chunk("quick brown fox jumps over the lazy dog");
			float w = c.GetWidthPoint();
			Paragraph p = new Paragraph("The width of the chunk: '");
			p.Add(c);
			p.Add("' is ");
			p.Add(w.ToString());
			p.Add(" points or ");
			p.Add((w / 72f).ToString());
			p.Add(" inches or ");
			p.Add((w / 72f * 2.54f).ToString());
			p.Add(" cm.");
			document.Add(p);

			document.Add(new Paragraph("SetGenericTag Example"));

			/* chapter04/FoxDogGeneric1.java */
			p = new Paragraph();
			fox = new Chunk("Quick brown fox");
			fox.SetGenericTag("box");
			p.Add(fox);
			p.Add(" jumps over ");
			dog = new Chunk("the lazy dog.");
			dog.SetGenericTag("ellipse");
			p.Add(dog);
			document.Add(p);

			document.Close();

			string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			webBrowser1.Navigate(string.Concat(appPath, "\\", @"HelloWorld.pdf"));
		}
	}

	/* chapter04/FoxDogGeneric1.java */
	public class FoxDogGeneric1 : PdfPageEventHelper
	{
		public override void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
		{
			if (text.Equals("ellipse"))
			{
				PdfContentByte cb = writer.DirectContent;
				cb.SetRGBColorStroke(0xFF, 0x00, 0x00);
				cb.Ellipse(rect.Left, rect.Bottom - 5f, rect.Right, rect.Top);
				cb.Stroke();
				cb.ResetRGBColorStroke();
			}
			else if (text.Equals("box"))
			{
				PdfContentByte cb = writer.DirectContent;
				rect.BackgroundColor = new BaseColor(0xa5, 0x2a, 0x2a);
				cb.Rectangle(rect);
			}
		}
	}
}
