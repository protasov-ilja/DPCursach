namespace Frontend.Models.Response
{
	public class Response<T>
	{
		public T Data { get; set; }
		public string UserName { get; set; }
		public int Code { get; set; }
		public string ErrorDescription { get; set; }
	}
}
