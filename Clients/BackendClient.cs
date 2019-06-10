using Frontend.Config;
using Frontend.Utils.Client;
using System.Net.Http;

namespace Frontend.Clients
{
	public class BackendClient : BaseClient
	{
		public BackendClient(HttpClient client, IConstants settings)
		   : base(client, settings.BackendBaseUrl)
		{
		}
	}
}
