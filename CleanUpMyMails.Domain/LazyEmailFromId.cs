using System;
using Chilkat;

namespace CleanUpMyMails.Domain
{
	internal class LazyEmailFromId : Lazy<Email>
	{

		public LazyEmailFromId(IImapConnection connection, int id)
			: base(() => FromId(connection, id))
		{ }

		private static Email FromId(IImapConnection connection, int id)
		{
			return connection.Connection.FetchSingle(id, false);
		}
	}
}