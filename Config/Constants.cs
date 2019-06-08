using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Config
{
	public class Constants : IConstants
	{
		public string BackendBaseUrl { get; private set; }

		public Constants(IConfiguration configuration)
		{
			BackendBaseUrl = configuration.GetConnectionString("BackendBaseUrl");
		}
	}
}
