using System;
namespace CleanUpMyMails.Domain
{
	public class ImapConfiguration : IImapConfiguration
	{

		public bool Ssl { get; set; }

		public int Port { get; set; }

		public bool KeepSessionLog { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
