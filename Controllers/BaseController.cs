using Frontend.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
	public class BaseController: Controller
	{
		private const string AuthorizedUserLayout = "~/Views/Shared/_AuthorizedLayout.cshtml";
		private const string NotAuthorizedUserLayout = "~/Views/Shared/_Layout.cshtml";

		private bool _isAuth => HttpContext.Session.TryGetValue(_constants.SessionUserKey, out var key);
		private readonly IConstants _constants;

		public BaseController(IConstants constants)
		{
			_constants = constants;
		}

		public ViewResult LayoutView<T>(T viewModel)
		{
			var view = View(viewModel);
			if (_isAuth)
			{
				view.ViewData["layout"] = AuthorizedUserLayout;
			}
			else
			{
				view.ViewData["layout"] = NotAuthorizedUserLayout;
			}

			return view;
		}
	}
}
