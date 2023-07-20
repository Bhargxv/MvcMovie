using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Msg"] = "Hello " + name;
        ViewData["number"] = numTimes;
        return View();
    }
	public IActionResult Add(int a, int b)
	{

		ViewData["a"] = a;
		ViewData["b"] = b;
		ViewData["c"] = 22;
		return View();

	}

	/*public IActionResult Adder(int a, int b)
	{
		
		return View();
	}*/
	[HttpPost]
	public IActionResult Adder([Bind] int a, int b)
	{
		int res = a + b;
		return View(res);
	}

}