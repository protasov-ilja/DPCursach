using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
	public sealed class ProfileViewModel
	{
		[Required(ErrorMessage = "Login is requared")]
		[Display(Name = "Login")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Password is requared")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Password is requared")]
		[DataType(DataType.Password)]
		[Display(Name = "Confrim Password")]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		public string FristName { get; set; }

		public string LastName { get; set; }

		public decimal Cash { get; set; }
	}
}
