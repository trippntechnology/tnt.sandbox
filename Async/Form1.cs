using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async
{
	public partial class Form1 : Form
	{
		CancellationTokenSource cts = null;

		public Form1()
		{
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			tb.AppendText("Button clicked\n");
			await WaitAsynchronouslyAsync();
			tb.AppendText($"WaitAsynchronouslyAsync returned\n");// ({result})");
			var result = await CallThread();
			listBox1.Items.Add($"ThreadCall {result}");
		}

		// The following method runs asynchronously. The UI thread is not
		// blocked during the delay. You can move or resize the Form1 window 
		// while Task.Delay is running.
		public async Task WaitAsynchronouslyAsync()
		{
			IProgress<string> progress = new Progress<string>(text =>
		 {
			 try
			 {
				 tb.AppendText($"{text}\n");
			 }
			 catch { }
		 });

			if (cts != null)
			{
				cts.Cancel();
			}

			cts = new CancellationTokenSource();

			var task = Task.Run(() =>
			{
				var token = cts.Token;
				for (var i = 0; i < 1000 && !token.IsCancellationRequested; i++)
				{
					progress.Report(i.ToString());
					Thread.Sleep(10);
				}
				progress.Report("done");
			}, cts.Token);
		}

		// The following method runs synchronously, despite the use of async.
		// You cannot move or resize the Form1 window while Thread.Sleep
		// is running because the UI thread is blocked.
		public string WaitSynchronously()
		{
			// Add a using directive for System.Threading.
			Thread.Sleep(10000);
			return "Finished";
		}

		public async  Task<string> CallThread()
		{
			var result = await Task.Run<string>(() =>
			{
				Thread.Sleep(5000);
				return "done";
			});

			return result;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			cts.Cancel();
		}
	}
}
