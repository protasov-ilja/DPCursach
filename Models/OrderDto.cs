using System;
using System.Collections.Generic;

namespace Frontend.Models
{
	public sealed class OrderDto
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public int IdUser { get; set; }
		public IEnumerable<PhoneDto> Phones { get; set; }
	}
}
