using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;

/// Taken from https://msdn.microsoft.com/en-us/library/zt39148a(v=vs.110).aspx
namespace MyNewService
{
	public enum ServiceState
	{
		SERVICE_STOPPED = 0x01,
		SERVICE_START_PENDING = 0x02,
		SERVICE_STOP_PENDING = 0x03,
		SERVICE_RUNNING = 0x04,
		SERVICE_CONTINUE_PENDING = 0x05,
		SERVICE_PAUSE_PENDING = 0x06,
		SERVICE_PAUSED = 0x07,
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ServiceStatus
	{
		public int dwServiceType;
		public int dwCurrentState;
		public int dwControlsAccepted;
		public int dwWin32ExitCode;
		public int dwServiceSpecificExitCode;
		public int dwCheckPoint;
		public int dwWaitHint;
	};

	public partial class MyNewService : ServiceBase
	{
		protected int eventId = 0;

		static void Main(string[] args)
		{
			MyNewService service = new MyNewService();

			if (Environment.UserInteractive)
			{
				service.OnStart(args);
				Console.WriteLine("Install as service: installutil MyNewService.exe");
				Console.WriteLine("Press any key to stop program");
				Console.Read();
				service.OnStop();
			}
			else
			{
				ServiceBase.Run(service);
			}
		}

		public MyNewService()
		{
			InitializeComponent();
			eventLog1 = new System.Diagnostics.EventLog();

			System.Diagnostics.EventLog.DeleteEventSource("MySource");
			if (!System.Diagnostics.EventLog.SourceExists("MySource"))
			{
				System.Diagnostics.EventLog.CreateEventSource("MySource", "MyNewLog");
			}
			eventLog1.Source = "MySource";
			eventLog1.Log = "MyNewLog";
		}

		protected override void OnStart(string[] args)
		{
			System.Diagnostics.Debugger.Launch();
			// Update the service state to Start Pending.
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = (int)ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 100000;
			serviceStatus.dwServiceType = 0x10;
			serviceStatus.dwControlsAccepted = 0x01;

			bool result = SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			eventLog1.WriteEntry("In OnStart");
			// Set up a timer to trigger every minute.
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Interval = 10000; // 60 seconds
			timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
			timer.Start();

			// Update the service state to Running.
			//serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			//result = SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
		{
			// TODO: Insert monitoring activities here.
			eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
		}

		protected override void OnStop()
		{
			eventLog1.WriteEntry("In OnStop.");
		}

		protected override void OnContinue()
		{
			eventLog1.WriteEntry("In OnContinue.");
		}

		protected override void OnPause()
		{
			eventLog1.WriteEntry("In OnPause.");
		}

		protected override void OnShutdown()
		{
			eventLog1.WriteEntry("In OnShutdown.");
		}

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

	}
}
