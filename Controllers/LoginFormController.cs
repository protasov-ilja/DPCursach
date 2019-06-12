using System.Threading.Tasks;
using Frontend.Clients;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class LoginFormController : BaseController
	{
		private IConstants _constants;
		private BackendClient _client;

		public LoginFormController(IConstants constants, BackendClient client)
			: base (constants)
		{
			_client = client;
			_constants = constants;
		}

		[HttpGet]
		public IActionResult Index()
        {
			var viewModel = new LoginViewModel();

			return LayoutView(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model) 
		{
			var userData = new UserDto
			{
				Login = model.Login,
				Password = model.Password,
			};

			var response = await _client.PostAsync<Response<string>, UserDto>(userData, $"/api/account/authorize");
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = response.Result;
				if (dataResponse.Data != null)
				{
					HttpContext.Session.SetString(_constants.SessionTokenKey, dataResponse.Data);
					HttpContext.Session.SetString(_constants.SessionUserKey, dataResponse.UserName);

					return RedirectToRoute(new { controller = "Home", action = "Index" });
				}
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Logout(LoginViewModel model)
		{
			HttpContext.Session.Remove(_constants.SessionTokenKey);
			HttpContext.Session.Remove(_constants.SessionUserKey);
			HttpContext.Session.Remove(_constants.SessionIdListKey);

			return RedirectToRoute(new { controller = "Home", action = "Index" });
		}
	}
}