using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			tb.AppendText("Button clicked");
			await WaitAsynchronouslyAsync();
		}

		// The following method runs asynchronously. The UI thread is not
		// blocked during the delay. You can move or resize the Form1 window 
		// while Task.Delay is running.
		public async Task<string> WaitAsynchronouslyAsync()
		{
			tb.AppendText("\r\nWaiting 10 seconds ...");
			await Task.Delay(10000);
			tb.AppendText("done");
			return "Finished";
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
	}
}
