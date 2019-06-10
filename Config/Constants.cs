using Microsoft.Extensions.Configuration;

namespace Frontend.Config
{
	public class Constants : IConstants
	{
		public string BackendBaseUrl { get; private set; }

		public string SessionTokenKey { get; }
		public string SessionUserKey { get; }

		public Constants(IConfiguration configuration)
		{
			BackendBaseUrl = configuration.GetConnectionString("BackendBaseUrl");
			SessionTokenKey = configuration.GetConnectionString("TokenKey");
			SessionUserKey = configuration.GetConnectionString("UserKey");
		}
	}
}
