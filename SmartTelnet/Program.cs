using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartTelnet
{
	class Program
	{
		enum State { Default, Data, DataPreExit };

		static State _State;

		static void Main(string[] args)
		{
			System.Threading.CancellationToken token = new System.Threading.CancellationToken();
			PrimS.Telnet.Client tc = new PrimS.Telnet.Client(args[0], Convert.ToInt32(args[1]), token);
			string cmd = string.Empty;

			do
			{
				//string result = await tc.ReadAsync();
				Task<string> task = tc.ReadAsync();
				Console.WriteLine(task.Result);

				if (cmd.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
				{
					break;
				}

				//Console.WriteLine(result);

				cmd = Console.ReadLine();
				tc.WriteLine(cmd);
			} 
			while (true);
		}

		static void OldMain(string[] args)
		{
			TcpClient client = new TcpClient();
			client.Connect(args[0], Convert.ToInt32(args[1]));

			NetworkStream netStream = client.GetStream();
			StreamReader sr = new StreamReader(netStream);
			string cmd = string.Empty;

			do
			{
				if (_State == State.Default)
				{
					Console.WriteLine(sr.ReadLine());
				}

				System.Diagnostics.Debugger.Launch();
				cmd = Console.ReadLine();
				byte[] writeBuffer = new byte[1024];
				ASCIIEncoding enc = new ASCIIEncoding();

				writeBuffer = enc.GetBytes(cmd + "\r\n");
				netStream.Write(writeBuffer, 0, writeBuffer.Length);

				switch (_State)
				{
					case State.Default:
						if (cmd.ToLower() == "data")
						{
							_State = State.Data;
						}
						break;
					case State.Data:
						if (cmd.ToLower() == ".")
						{
							_State = State.DataPreExit;
						}
						break;
					case State.DataPreExit:
						if (cmd.ToLower() == "")
						{
							_State = State.Default;
						}
						else
						{
							_State = State.Data;
						}
						break;
				}
			}
			while (cmd.ToLower() != "quit");

		}
	}
}
