using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Frontend.Controllers
{
	public class RegistrationFormController : Controller
	{
		public IActionResult Index()
		{
			var registrationViewModel = new SignInViewModel();

			return View(registrationViewModel);
		}

		[HttpPost]
		public IActionResult Register(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				string loginString = Request.Form.FirstOrDefault(p => p.Key == "login").Value;
				string passwordString = Request.Form.FirstOrDefault(p => p.Key == "password").Value;
				string passwordFirstName = Request.Form.FirstOrDefault(p => p.Key == "first-name").Value;
				string passwordLastName = Request.Form.FirstOrDefault(p => p.Key == "last-name").Value;
				string isAdminString = Request.Form.FirstOrDefault(p => p.Key == "is-admin").Value;
				bool isAdmin = bool.Parse(isAdminString);
				// do something

				return View();
			}
			else
			{
				return View();
			}
		}
	}
}
