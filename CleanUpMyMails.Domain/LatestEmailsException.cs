using System;
using System.Runtime.Serialization;

namespace CleanUpMyMails.Domain
{
	[Serializable]
	internal class LatestEmailsException : Exception
	{
		private string v;
		private string lastErrorText;

		public LatestEmailsException()
		{
		}

		public LatestEmailsException(string message) : base(message)
		{
		}

		public LatestEmailsException(string v, string lastErrorText)
		{
			this.v = v;
			this.lastErrorText = lastErrorText;
		}

		public LatestEmailsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected LatestEmailsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}