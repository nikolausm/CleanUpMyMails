using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class LatestEmails : IEnumerable<Email>
	{
		private readonly Imap connection;
		private readonly int count;
		private readonly int startIndex;

		public LatestEmails(IImapConnection connection, int count, int startIndex = -1)
		{
			this.connection = connection.Connection;
			this.count = count;
			this.startIndex = startIndex;
		}



		public IEnumerator<Email> GetEnumerator()
		{
			int emailSequentialNumber = startIndex == -1 ? connection.NumMessages : startIndex;


			int currentStartIndex = (emailSequentialNumber - count) <= 0
				? 1
				: emailSequentialNumber - count + 1;


			var set = new MessageSet();
			set.HasUids = false;


			set.FromCompactString($"{currentStartIndex}:{connection.NumMessages}");
			if(!set.LastMethodSuccess)
			{
				throw new LatestEmailsException("Error fetching email", connection.LastErrorText);
			}

			EmailBundle newestEmails = connection.FetchBundle(set);
			if(!newestEmails.LastMethodSuccess)
			{
				throw new LatestEmailsException("Error fetching email", connection.LastErrorText);
			}

			for(int i = 0; i < newestEmails.MessageCount; i++)
			{
				yield return newestEmails.GetEmail(i);
			}


		}

		IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
	}
}
