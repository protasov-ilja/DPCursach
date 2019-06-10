using System.Collections.Generic;

namespace Frontend.Models
{
	public sealed class BucketModel
	{
		public long Id { get; set; }
		public List<PhoneDto> Items { get; set; }
	}
}
