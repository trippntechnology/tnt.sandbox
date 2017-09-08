using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;

namespace OAuthClient
{
	class Program
	{
		static string BASE_URL = "http://localhost:5000";
		//static string BASE_URL = "http://52.168.13.72:5000";

		static void Main(string[] args)
		{
			TokenResponse tr = GetClientToken();
			Console.WriteLine(tr.Json);
			Console.ReadKey();

			tr = GetWINClientToken();
			Console.WriteLine(tr.Json);
			Console.ReadKey();

			CallApi(tr);
			Console.ReadKey();
			tr = GetUserToken();
			Console.WriteLine(tr.Json);
			CallApi(tr);
		}

		static TokenResponse GetUserToken()
		{
			var client = new TokenClient(
					$"{BASE_URL}/connect/token",
					"carbon",
					"21B5F798-BE55-42BC-8AA8-0025B903DC3B");

			

			return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
		}

		static TokenResponse GetClientToken()
		{
			var client = new TokenClient(
					$"{BASE_URL}/connect/token",
					"silicon",
					"F621F470-9731-4A25-80EF-67A6F7C5F4B8");

			return client.RequestClientCredentialsAsync("api1").Result;
		}

		static TokenResponse GetWINClientToken()
		{
			var client = new TokenClient(
					$"{BASE_URL}/connect/token",
					"367af82b-5c8e-47f9-b881-c84401632655",
					"");

			return client.RequestClientCredentialsAsync("api1").Result;
		}

		static void CallApi(TokenResponse response)
		{
			var client = new HttpClient();
			client.SetBearerToken(response.AccessToken);

			Console.WriteLine(client.GetStringAsync("http://localhost:2675/test").Result);
		}
	}
}
