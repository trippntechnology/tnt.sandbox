using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontPreviewer
{
	public partial class Form1 : Form
	{
		CancellationTokenSource cts = null;

		public Form1()
		{
			InitializeComponent();
		}

		private async void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			Debug.WriteLine("textBox1_KeyUp called");
			await PreviewFontsAsync();
			Debug.WriteLine("textBox1_KeyUp exited");
		}
		private async Task PreviewFontsAsync()
		{
			richTextBox.Clear();
			if (String.IsNullOrWhiteSpace(textBox.Text)) return;

			IProgress<Font> progress1 = new Progress<Font>(font =>
			{
				richTextBox.SelectionFont = font;
			});

			IProgress<Tuple<string, string>> progress2 = new Progress<Tuple<string, string>>(tuple =>
				{
					richTextBox.AppendText($"{tuple.Item1}\n\t{tuple.Item2}\n");
				});

			if (cts != null)
			{
				Debug.WriteLine("Task canceled");
				cts.Cancel();
			}

			cts = new CancellationTokenSource();

			await Task.Run(() =>
			 {
				 Debug.WriteLine("Task started");
				 var token = cts.Token;
				 Thread.Sleep(300);

				 var fonts = new List<FontFamily>();

				 foreach (FontFamily font in System.Drawing.FontFamily.Families)
				 {
					 fonts.Add(font);
				 }

				 foreach (var fontFamily in fonts)
				 {
					 if (token.IsCancellationRequested) break;
					 progress1.Report(new Font(fontFamily, 20f));
					 progress2.Report(new Tuple<string, string>(fontFamily.Name, textBox.Text));
					 Thread.Sleep(10);
				 }

				 Debug.WriteLine("Task complete");
			 });
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// This is needed to cancel the Task before closing.
			cts?.Cancel();
		}
	}
}
