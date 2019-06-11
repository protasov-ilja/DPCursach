using Frontend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.ViewModels
{
	public class BucketViewModel
	{
		public List<PhoneDto> Phones { get; set; }

		public decimal TotalPrice => Phones.Sum(x => x.Price);
	}
}
