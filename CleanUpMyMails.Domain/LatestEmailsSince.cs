using System.Collections;
using System.Collections.Generic;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	public class LatestEmailsSince : IEnumerable<Email>
	{
		private readonly IImapConnection connection;
		private readonly ImapDate dateTime;

		public LatestEmailsSince(IImapConnection connection, ImapDate dateTime)
		{
			this.connection = connection;
			this.dateTime = dateTime;
		}


		public IEnumerator<Email> GetEnumerator()
		{

			MessageSet set = connection.Connection.Search($"ENTSINCE {dateTime}", false);
			if(!set.LastMethodSuccess)
			{
				throw new LatestEmailsException("Error fetching email", connection.Connection.LastErrorText);
			}

			for(int i = 0; i < set.Count; i++)
			{
				yield return new LazyEmailFromId(connection, set.GetId(i)).Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
	}
}
