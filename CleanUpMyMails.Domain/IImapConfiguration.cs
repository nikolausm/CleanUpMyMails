namespace CleanUpMyMails.Domain
{
	public interface IImapConfiguration
	{
		bool Ssl { get; }
		int Port { get; }
		bool KeepSessionLog { get; }
		string Username { get; }
		string Password { get; }
	}
}