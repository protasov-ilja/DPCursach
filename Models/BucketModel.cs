using System.Collections.Generic;

namespace Frontend.Models
{
	public sealed class BucketModel
	{
		public int Id { get; set; }
		public List<PhoneDto> Items { get; set; }
	}
}
