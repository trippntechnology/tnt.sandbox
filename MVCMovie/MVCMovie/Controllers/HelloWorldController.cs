using Microsoft.AspNetCore.Mvc;

namespace MVCMovie.Controllers
{
	public class HelloWorldController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public string Welcome(string param1, string param2)
		{
			return $"This is the Welcome action method...[{param1}:{param2}]";
		}
	}
}