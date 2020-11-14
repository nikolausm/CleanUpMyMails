using System;
using System.Runtime.Serialization;

namespace CleanUpMyMails.Domain
{
	[Serializable]
	internal class ImapConnectionException : Exception
	{
		public ImapConnectionException()
		{
		}

		public ImapConnectionException(string message) : base(message)
		{
		}

		public ImapConnectionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ImapConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}