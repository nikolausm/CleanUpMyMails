using System;
using System.Diagnostics;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class ImapConnection : IImapConnection
	{
		private readonly IImapConfiguration imapConfiguration;
		private bool connected;

		public ImapConnection(IImapConfiguration imapConfiguration)
		{
			this.imapConfiguration = imapConfiguration ?? throw new ArgumentNullException(nameof(imapConfiguration));
			Connection = new Chilkat.Imap();
			// Turn on session logging:
			Connection.KeepSessionLog = imapConfiguration.KeepSessionLog;

			// Connect to GMail
			// Use TLS
			Connection.Ssl = imapConfiguration.Ssl;
			Connection.Port = imapConfiguration.Port;
		}

		public Imap Connection { get; }
		public void Connect()
		{
			if(!Connection.Connect("imap.gmail.com"))
			{
				throw new ImapConnectionException(Connection.LastErrorText);
			}

			// Login
			// Your login is typically your GMail email address.
			if(!Connection.Login(imapConfiguration.Username, imapConfiguration.Password))
			{
				throw new ImapConnectionException(Connection.LastErrorText);
			}

			// Select an IMAP mailbox
			if(!Connection.SelectMailbox("Inbox"))
			{
				throw new ImapConnectionException(Connection.LastErrorText);
			}

			// Show the session log.
			Debug.WriteLine(Connection.SessionLog);

			// Disconnect from the IMAP server.
			connected = true;
		}

		public void Dispose()
		{
			if(connected && !Connection.Disconnect())
			{
				throw new ImapConnectionException(Connection.LastErrorText);
			}
		}
	}
}
