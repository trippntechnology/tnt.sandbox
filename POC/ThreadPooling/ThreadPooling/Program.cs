using System;
using System.Threading;

namespace ThreadPooling
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime dt = DateTime.Now.AddMinutes(1);
			int threadCount = 25;
			int maxThreadCount = 1000;
			int wM, wA, cM, cA;

			bool result = ThreadPool.SetMaxThreads(maxThreadCount, maxThreadCount);
			Console.WriteLine("SetMaxThreads({0}, {0}) = {1}", maxThreadCount, result.ToString());

			using (CountdownEvent cde = new CountdownEvent(1))
			{
				for (int i = 0; i < threadCount; i++)
				{
					UserWorkItem uwi = new UserWorkItem(cde);
					ThreadPool.QueueUserWorkItem(uwi.DoWork, new State() { ThreadID = i, RunUntil = dt });
					ThreadPool.GetAvailableThreads(out wA, out cA);
					ThreadPool.GetMaxThreads(out wM, out cM);

					Console.WriteLine("[{0}] Thread count: {1}", i, wM-wA);
				}

				ThreadPool.GetAvailableThreads(out wA, out cA);
				ThreadPool.GetMaxThreads(out wM, out cM);
				Console.WriteLine("Thread count: {0}", wM - wA);
				Console.WriteLine("Will run at {0}", dt.ToString());

				cde.Signal();
				cde.Wait();
			}
		}
	}
}
