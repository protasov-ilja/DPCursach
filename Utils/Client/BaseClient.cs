using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Frontend.Utils.Client
{
	public class BaseClient : IClient
	{
		const int InternalServerError = 500;

		private readonly HttpClient _client;

		protected BaseClient(HttpClient client, string baseUrl)
		{
			_client = client;
			_client.BaseAddress = new Uri(baseUrl);
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		private BaseClient()
		{
		}

		public async Task<ClientResponse<R>> GetAsync<R>(string url, string token = null)
		{
			var result = new ClientResponse<R>();
			HttpResponseMessage httpResponse = null;
			if (token != null)
			{
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			
			try
			{
				httpResponse = await _client.GetAsync(url);
				var responseData = httpResponse.IsSuccessStatusCode;
				if (httpResponse.IsSuccessStatusCode)
				{
					result.Result = await httpResponse.Content.ReadAsAsync<R>();
				}

				result.StatusCode = (int)httpResponse.StatusCode;
				result.IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				result.StatusCode = InternalServerError;
			}
			finally
			{
				if (httpResponse != null)
				{
					httpResponse.Dispose();
				}
			}

			return result;
		}

		public async Task<ClientResponse<R>> PostAsync<R, D>(D data, string path, string token = null)
		{
			var result = new ClientResponse<R>();
			HttpResponseMessage httpResponse = null;
			if (token != null)
			{
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}

			try
			{
				httpResponse = await _client.PostAsJsonAsync(path, data);
				result.IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
				if (httpResponse.IsSuccessStatusCode)
				{
					result.Result = await httpResponse.Content.ReadAsAsync<R>();
				}

				result.StatusCode = (int)httpResponse.StatusCode;
			}
			catch (Exception)
			{
				result.StatusCode = InternalServerError;
			}
			finally
			{
				if (httpResponse != null)
				{
					httpResponse.Dispose();
				}
			}

			return result;
		}

		public async Task<ClientResponse<object>> PostAsync<D>(D data, string path, string token = null)
		{
			return await PostAsync<object, D>(data, path, token);
		}
	}
}
