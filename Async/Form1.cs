using System.Diagnostics;

namespace Async
{
  public partial class Form1 : Form
  {
    private const string Category = "Debouncer";
    CancellationTokenSource? cts = null;
    object _Lock = new object();

    public Form1()
    {
      InitializeComponent();
    }

    private async void button1_ClickAsync(object sender, EventArgs e)
    {
      tb.AppendText("Button clicked\n");
      await WaitAsynchronouslyAsync();
      tb.AppendText($"WaitAsynchronouslyAsync returned\n");// ({result})");
                                                           //var result = await CallThread();
                                                           //listBox1.Items.Add($"ThreadCall {result}");
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

      await Task.Run(() =>
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

    public string CallThread()
    {
      Debug.WriteLine("CallThread() called");

      var result = Task.Run<string>(() =>
      {
        Debug.WriteLine("Task started");
        Thread.Sleep(5000);
        return "done";
      });


      Debug.WriteLine("Waiting for task to complete");
      result.Wait();
      Debug.WriteLine("Returning from CallThread()");
      return result.Result;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      cts?.Cancel();
    }

    private void textBox1_KeyUp(object sender, KeyEventArgs e)
    {
      Debug.WriteLine("Called", "Debounce");

      IProgress<string> progress = new Progress<string>(text =>
      {
        try
        {
          tb.AppendText($"{text}\n");
        }
        catch { }
      });

      Debounce((sender as TextBox)!.Text, (text, token) =>
        {
          if (token.IsCancellationRequested)
          {
            Debug.WriteLine("Token was cancelled", Category);
          }
          else
          {
            Debug.WriteLine("Doing Work", Category);
            progress.Report(text);
          }
        });
    }

    private void Debounce(string text, Action<string, CancellationToken> action)
    {
      if (cts != null)
      {
        Debug.Write("Canceling previous task", Category);
        cts.Cancel();
        Debug.WriteLine(" ... Done");
        //cts = null;
      }

      while (cts?.IsCancellationRequested == false)
      {
        Debug.WriteLine("Waiting for task to cancel", Category);
      }

      //lock (_Lock)
      //{
      cts = new CancellationTokenSource();

      //try
      //{
      var task = Task.Run(async () =>
      {
        try
        {
          Debug.WriteLine("Entered Run", Category);

          //try
          //{
          await Task.Delay(1000, cts.Token);
          //}
          //catch (Exception ex)
          //{
          //	Debug.WriteLine($"Delay {ex.Message}", Category);
          //}

          if (!cts.Token.IsCancellationRequested)
          {
            action(text, cts.Token);
          }
          else
          {
            Debug.WriteLine("Token cancelled", Category);
            cts.Token.ThrowIfCancellationRequested();
          }

          Debug.WriteLine("Task complete", Category);
        }
        catch (Exception ex)
        {
          Debug.WriteLine($"Exception: {ex.Message}", Category);
        }
      }, cts.Token);
      //}
      //catch (Exception ex)
      //{
      //	Debug.WriteLine($"Do Work {ex.Message}", Category);
      //}
      //}
    }
  }
}
