namespace Frontend.Utils.Client
{
	public class ClientResponse<R>
	{
		public int StatusCode { get; set; }
		public bool IsSuccessStatusCode { get; set; }
		public R Result { get; set; }
	}
}
