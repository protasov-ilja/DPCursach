using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Frontend.Config;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Frontend.Clients;

namespace Frontend.Controllers
{
	public class HomeController : BaseController
	{
		private IConstants _constants;
		private BackendClient _client;

		public HomeController(IConstants constants, BackendClient client)
			: base(constants)
		{
			_client = client;
			_constants = constants;
		}

		public async Task<IActionResult> Index()
		{
			var response = await _client.GetAsync<List<PhoneDto>>($"/api/purchase/allPhones");
			List<PhoneDto> dataResponse = null;
			if (!response.IsSuccessStatusCode)
			{
				dataResponse = new List<PhoneDto>();

				return LayoutView(dataResponse);
			}

			dataResponse = response.Result;

			return LayoutView(dataResponse);
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
