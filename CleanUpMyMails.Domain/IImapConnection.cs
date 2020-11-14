using System;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public interface IImapConnection : IDisposable
	{
		Imap Connection { get; }

		void Connect();
	}
}