using System;
using timetracker.Messages;

namespace timetracker.Tracking.Version
{
	internal class VersionEvent : IMessage
	{
		private string CurrentVersion;

		public VersionEvent(string currentVersion)
		{
			CurrentVersion = currentVersion;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return Current.Messages.Version;
		}
	}
}