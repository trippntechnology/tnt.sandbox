using System;
using System.Threading;

namespace ThreadPooling
{
	public class UserWorkItem
	{
		private CountdownEvent _CountdownEvent = null;

		public UserWorkItem(CountdownEvent countdownEvent)
		{
			_CountdownEvent = countdownEvent;
		}

		public void DoWork(object obj)
		{
			_CountdownEvent.AddCount();
			State state = obj as State;

			while (DateTime.Now < state.RunUntil )
			{
			}

			Console.WriteLine("Thread #{0}: Doing work", state.ThreadID);

			_CountdownEvent.Signal();
		}
	}
}
