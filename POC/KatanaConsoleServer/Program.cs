using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaConsoleServer
{
	using System.IO;
	using System.Web.Http;
	using AppFunc = Func<IDictionary<string, object>, Task>;

	//class Program
	//{
	//	static void Main(string[] args)
	//	{
	//		using (WebApp.Start<Startup>("http://localhost:8080"))
	//		{
	//			Console.WriteLine("Started");
	//			Console.ReadKey();
	//			Console.WriteLine();
	//		}
	//	}
	//}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//app.Use(async (env, next) =>
			//{
			//	foreach (var pair in env.Environment)
			//	{
			//		Console.WriteLine("{0}:{1}", pair.Key, pair.Value);
			//	}

			//	await next();
			//});

			app.Use(async (env, next) =>
			{
				Console.WriteLine("Requesting: " + env.Request.Path);
				await next();
				Console.WriteLine("Response:" + env.Response.StatusCode);
			});

			ConfigureWebApi(app);

			app.Use<HellowWorldComponent>();

			//app.UseWelcomePage();

			//app.Run(ctx =>
			//{
			//	return ctx.Response.WriteAsync("Hello world!");
			//});
		}

		private void ConfigureWebApi(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
					"DefaultApi",
					"api/{controller}/{id}",
					new { id = RouteParameter.Optional });
			app.UseWebApi(config);
		}
	}

	public class HellowWorldComponent
	{
		AppFunc _next;
		public HellowWorldComponent(AppFunc next)
		{
			_next = next;
		}

		public Task Invoke(IDictionary<string, object> environment)
		{
			var response = environment["owin.ResponseBody"] as Stream;
			using (var writer = new StreamWriter(response))
			{
				return writer.WriteAsync("Hello!");
			}

			//await _next(environment);
		}
	}
}
