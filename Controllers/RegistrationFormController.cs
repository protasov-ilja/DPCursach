using Frontend.Clients;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
	public class RegistrationFormController : BaseController
	{
		private IConstants _constants;
		private BackendClient _client;

		public RegistrationFormController(IConstants constants, BackendClient client)
			: base(constants)
		{
			_client = client;
			_constants = constants;
		}

		public IActionResult Index()
		{
			var registrationViewModel = new SignInViewModel();

			return LayoutView(registrationViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Register(SignInViewModel model)
		{
			HttpClient client = new HttpClient();

			var userData = new UserDto
			{
				Login = model.Login,
				Password = model.Password,
				FirstName = model.FristName,
				LastName = model.LastName,
				UserKind = model.IsAdmin ? "Admin" : "Buyer"
			};

			var response = await _client.PostAsync<Response<string>, UserDto>(userData, $"/api/account/registration");
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
	}
}
