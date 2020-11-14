using System;
using System.Diagnostics;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class ImapConnection : IDisposable
	{
		private readonly IImapConfiguration imapConfiguration;
		private readonly Imap imap;
		private bool connected;

		public ImapConnection(IImapConfiguration imapConfiguration)
		{
			this.imapConfiguration = imapConfiguration ?? throw new ArgumentNullException(nameof(imapConfiguration));
			imap = new Chilkat.Imap();
			// Turn on session logging:
			imap.KeepSessionLog = imapConfiguration.KeepSessionLog;

			// Connect to GMail
			// Use TLS
			imap.Ssl = imapConfiguration.Ssl;
			imap.Port = imapConfiguration.Port;
		}

		public void Connect()
		{
			if(!imap.Connect("imap.gmail.com"))
			{
				throw new ImapConnectionException(imap.LastErrorText);
			}

			// Login
			// Your login is typically your GMail email address.
			if(!imap.Login(imapConfiguration.Username, imapConfiguration.Password))
			{
				throw new ImapConnectionException(imap.LastErrorText);
			}

			// Select an IMAP mailbox
			if(!imap.SelectMailbox("Inbox"))
			{
				throw new ImapConnectionException(imap.LastErrorText);
			}

			// Show the session log.
			Debug.WriteLine(imap.SessionLog);

			// Disconnect from the IMAP server.
			connected = true;
		}

		public void Dispose()
		{
			if(connected && !imap.Disconnect())
			{
				throw new ImapConnectionException(imap.LastErrorText);
			}
		}
	}
}
