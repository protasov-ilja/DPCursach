namespace Frontend.Config
{
	public interface IConstants
	{
		string BackendBaseUrl { get; }
		string SessionTokenKey { get; }
		string SessionUserKey { get; }
		string SessionIdListKey { get; }
	}
}
