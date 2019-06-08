using System.Linq;
using System.Net.Http;
using Frontend.Config;
using Frontend.Models;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class LoginFormController : Controller
    {
		private UserDataModel _userModel = new UserDataModel();
		private IConstants _constants;

		public LoginFormController(IConstants constants)
		{

		}

		public IActionResult Index()
        {
			var viewModel = new LoginViewModel();

            return View(viewModel);
        }

		[HttpPost]
		public async IActionResult Login(LoginViewModel model) 
		{
			HttpResponseMessage response = null;
			HttpContent content = null;
			HttpClient client = new HttpClient();

			var userData = new UserDataModel
			{
				Login = model.Login,
				Password = model.Passwrod,
				UserKind = ""
			};

			await client.PostAsJsonAsync("https://localhost:")

			return View(model);
		}
    }
}