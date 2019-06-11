using Frontend.Clients;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class AdminController : BaseController
    {
		private IConstants _constants;
		private BackendClient _client;

		public AdminController(IConstants constants, BackendClient client)
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

		public IActionResult EditPhone(PhoneDto editedPhone)
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			return LayoutView(editedPhone);
		}

		public async Task<IActionResult> Edit(PhoneDto editedPhone)
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var response = await _client.PostAsync<Response<bool>, PhoneDto>(editedPhone, $"/api/purchase/updatePhone", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data)
				{
					return RedirectToAction("EditPhone", editedPhone);
				}
			}

			return RedirectToAction("EditPhone", editedPhone);
		}

		//public async Task<IActionResult> AddPhone(PhoneDto editedPhone)
		//{


		//	if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
		//	{
		//		return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
		//	}

		//	var response = await _client.PostAsync<Response<bool>, PhoneDto>(editedPhone, $"/api/purchase/update", HttpContext.Session.GetString(_constants.SessionTokenKey));
		//	if (response.IsSuccessStatusCode)
		//	{
		//		var dataResponse = response.Result;
		//		if (dataResponse.Data != null)
		//		{
		//			HttpContext.Session.SetString(_constants.SessionTokenKey, dataResponse.Data);
		//			HttpContext.Session.SetString(_constants.SessionUserKey, dataResponse.UserName);

		//			return RedirectToAction("ShowProfile");
		//		}
		//	}
		//}

		public async Task<IActionResult> Remove(PhoneDto editedPhone)
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var response = await _client.PostAsync<Response<bool>, int>(editedPhone.Id, $"/api/purchase/deletePhone", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data)
				{
					return RedirectToAction("Index");
				}
			}

			return RedirectToAction("EditPhone", editedPhone);
		}
	}
}