using Frontend.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
	public sealed class UserDataModel
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
		public string FristName { get; set; }
		public string LastName { get; set; }
		[Required]
		public string UserKind { get; set; }
	}
}
