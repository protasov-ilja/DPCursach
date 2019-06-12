using System;

namespace Frontend.Models
{
	public sealed class UserHistoryDto
	{
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
	}
}
