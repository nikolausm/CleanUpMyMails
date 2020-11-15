using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class UnreadEmails : IEnumerable<Email>
	{
		private readonly ImapConnection connection;
		private readonly IEnumerable<Email> emails;

		public UnreadEmails(ImapConnection connection, IEnumerable<Email> emails)
		{
			this.connection = connection;
			this.emails = emails;
		}

		public IEnumerator<Email> GetEnumerator()
		=> emails
			.Where(email => connection.Connection.GetMailFlag(email, "SEEN") == 1).GetEnumerator();


		IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
	}
}
