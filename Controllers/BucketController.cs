using Frontend.Clients;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class BucketController : BaseController
    {
		private IConstants _constants;
		private BackendClient _client;

		public BucketController(IConstants constants, BackendClient client)
			: base(constants)
		{
			_client = client;
			_constants = constants;
		}

		public async Task<IActionResult> Index()
		{
			var idList = new List<int>();
			var phones = new List<PhoneDto>();
			if (HttpContext.Session.TryGetValue(_constants.SessionIdListKey, out var data))
			{
				var json = HttpContext.Session.GetString(_constants.SessionIdListKey);
				idList = JsonConvert.DeserializeObject<List<int>>(json);

				var response = await _client.PostAsync<List<PhoneDto>, List<int>>(idList, $"/api/purchase/phones");
				if (response.IsSuccessStatusCode)
				{
					phones = response.Result;
				}
			}

			var model = new BucketViewModel
			{
				Phones = phones
			};

			return LayoutView(model);
        }

		public async Task<IActionResult> Order()
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			if (HttpContext.Session.TryGetValue(_constants.SessionIdListKey, out var data))
			{
				var json = HttpContext.Session.GetString(_constants.SessionIdListKey);
				var	idList = JsonConvert.DeserializeObject<List<int>>(json);

				var response = await _client.PostAsync<Response<bool>, List<int>>(idList, $"/api/purchase/payOrder", HttpContext.Session.GetString(_constants.SessionTokenKey));
			}

			return RedirectToAction("Index");
		}

		public IActionResult ClearBucket()
		{
			var idList = new List<int>();
			var str = JsonConvert.SerializeObject(idList);
			HttpContext.Session.SetString(_constants.SessionTokenKey, str);

			return RedirectToAction("Index");
		}
    }
}