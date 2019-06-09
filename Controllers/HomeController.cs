using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Frontend.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Frontend.Controllers
{
	public class HomeController : Controller
	{
		private IConstants _constants;

		public HomeController(IConstants constants)
		{
			_constants = constants;
		}

		public async Task<IActionResult> Index()
		{
			HttpContext.Session.SetString("name", "The Doctor");
			//HttpClient client = new HttpClient();

			//HttpResponseMessage response = await client.GetAsync($"{_constants.BackendBaseUrl}/api/purchase/allPhones");
			//string json = await response.Content.ReadAsStringAsync();

			//var dataResponse = JsonConvert.DeserializeObject<List<ItemDataModel>>(json);
			var dataResponse = new List<ItemDataModel>();

			return View(dataResponse);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
