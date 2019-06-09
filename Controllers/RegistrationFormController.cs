using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
	public class RegistrationFormController : Controller
	{
		private IConstants _constants;

		public RegistrationFormController(IConstants constants)
		{
			_constants = constants;
		}

		public IActionResult Index()
		{
			var registrationViewModel = new SignInViewModel();

			return View(registrationViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Register(SignInViewModel model)
		{
			HttpClient client = new HttpClient();

			var userData = new UserDataModel
			{
				Login = model.Login,
				Password = model.Password,
				FristName = model.FristName,
				LastName = model.LastName,
				UserKind = model.IsAdmin ? "Admin" : "Buyer"
			};

			HttpResponseMessage response = await client.PostAsJsonAsync($"{_constants.BackendBaseUrl}/api/account/registration", userData);
			string json = await response.Content.ReadAsStringAsync();

			var dataResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(json);

			if (dataResponse.AccessToken != null)
			{
				return RedirectToRoute("Home/Index", dataResponse);
			}

			return RedirectToAction("Index");
		}
	}
}
