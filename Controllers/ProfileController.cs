using Frontend.Clients;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
	public class ProfileController : BaseController
	{
		private IConstants _constants;
		private BackendClient _client;

		public ProfileController(IConstants constants, BackendClient client)
			: base(constants)
		{
			_client = client;
			_constants = constants; 
		}

		public async Task<IActionResult> ShowProfile()
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var response = await _client.GetAsync<Response<UserDto>>($"/api/account/user", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data != null)
				{
					UserDto data = dataResponse.Data;
					var viewModel = new ProfileViewModel
					{
						Login = data.Login,
						Password = data.Password,
						ConfirmPassword = data.Password,
						FirstName = data.FirstName,
						LastName = data.LastName,
						Cash = data.Cash
					};

					return LayoutView(viewModel);
				}
			}

			return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
		}

		public async Task<IActionResult> EditProfile()
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var response = await _client.GetAsync<Response<UserDto>>($"/api/account/user", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data != null)
				{
					UserDto data = dataResponse.Data;
					var viewModel = new ProfileViewModel
					{
						Login = data.Login,
						Password = data.Password,
						ConfirmPassword = data.Password,
						FirstName = data.FirstName,
						LastName = data.LastName,
						Cash = data.Cash
					};

					return LayoutView(viewModel);
				}
			}

			return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
		}

		public async Task<IActionResult> Edit(ProfileViewModel model)
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var user = new UserDto
			{
				Login = model.Login,
				Password = model.Password,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Cash = model.Cash
			};

			var response = await _client.PostAsync<Response<string>, UserDto>(user, $"/api/account/update", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data != null)
				{
					HttpContext.Session.SetString(_constants.SessionTokenKey, dataResponse.Data);
					HttpContext.Session.SetString(_constants.SessionUserKey, dataResponse.UserName);

					return RedirectToAction("ShowProfile");
				}
			}

			return RedirectToAction("EditProfile");
		}

		public async Task<IActionResult> ShowHistory()
		{
			if (!HttpContext.Session.TryGetValue(_constants.SessionTokenKey, out var token))
			{
				return RedirectToRoute(new { controller = "LoginForm", action = "Index" });
			}

			var viewModel = new List<UserHistoryDto>();
			var response = await _client.GetAsync<Response<List<UserHistoryDto>>>($"/api/account/history", HttpContext.Session.GetString(_constants.SessionTokenKey));
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data != null)
				{
					viewModel = dataResponse.Data;

					return LayoutView(viewModel);
				}
			}

			return LayoutView(viewModel);
		}
	}
}
