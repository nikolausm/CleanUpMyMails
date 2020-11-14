using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class LatestEmails : IEnumerable<Email>
	{
		private readonly Imap connection;
		private readonly int count;

		public LatestEmails(IImapConnection connection, int count)
		{
			this.connection = connection.Connection;
			this.count = count;
		}



		public IEnumerator<Email> GetEnumerator()
		{
			int emailSequentialNumber = connection.NumMessages;


			int startIndex = (emailSequentialNumber - count) <= 0
				? emailSequentialNumber
				: emailSequentialNumber - count;

			for(int i = startIndex; i <= emailSequentialNumber; i++)
			{
				Email newestEmail = connection.FetchSingle(i, bUid: false);
				if(!connection.LastMethodSuccess)
				{
					throw new LatestEmailsException("Error fetching email", connection.LastErrorText);
				}

				yield return newestEmail;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
	}
}
