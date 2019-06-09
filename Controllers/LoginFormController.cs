using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Config;
using Frontend.Models;
using Frontend.Models.Response;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class LoginFormController : Controller
    {
		private UserDataModel _userModel = new UserDataModel();
		private IConstants _constants;

		public LoginFormController(IConstants constants)
		{
			_constants = constants;
		}

		[HttpGet]
		public IActionResult Index()
        {
			var name = HttpContext.Session.GetString("name");
			var viewModel = new LoginViewModel();
			viewModel.Login = name;

			return View(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model) 
		{
			HttpClient client = new HttpClient();
	
			var userData = new UserDataModel
			{
				Login = model.Login,
				Password = model.Password,
			};
		
			HttpResponseMessage response = await client.PostAsJsonAsync($"{_constants.BackendBaseUrl}/api/account/authorize", userData);
			string json = await response.Content.ReadAsStringAsync();

			var dataResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(json);

			if (dataResponse.AccessToken != null)
			{
				HttpContext.Session.SetString(_constants.SessionTokenKey, dataResponse.AccessToken);
				HttpContext.Session.SetString(_constants.SessionUserKey, dataResponse.UserName);

				return RedirectToRoute("Home/Index", dataResponse);
			}
			
			return RedirectToAction("Index");
		}
    }
}