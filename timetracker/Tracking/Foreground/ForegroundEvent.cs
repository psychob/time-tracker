using System;
using timetracker.Messages;

namespace timetracker.Tracking.Foreground
{
	internal class ForegroundEvent : IMessage
	{
		private uint ProcessId;
		private uint ThreadID;

		public ForegroundEvent(uint processId, uint threadID)
		{
			ProcessId = processId;
			ThreadID = threadID;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return Current.Messages.ForegroundChange;
		}
	}
}