using System;
namespace CleanUpMyMails.Domain
{
	public class ImapDate : Lazy<string>
	{
		public ImapDate(DateTime dateTime)
		: base(() => $"{dateTime:DD}-{dateTime:MMM}-{dateTime:YYY}")
		{
		}

		public override string ToString()
		=> Value;
	}
}

