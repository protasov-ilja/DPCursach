namespace Frontend.Models.Response
{
	public class AuthorizationResponse
	{
		public string AccessToken { get; set; }
		public string UserName { get; set; }
		public int ErrorCode { get; set; }
		public string ErrorDescription { get; set; }
	}
}
