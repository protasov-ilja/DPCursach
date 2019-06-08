using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Login is requared")]
		[Display(Name = "Login")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Password is requared")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Passwrod { get; set; }
	}
}
