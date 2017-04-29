using System;
using timetracker.Messages;

namespace timetracker.Tracking.Namechange
{
	internal class NamechangeEvent : IMessage
	{
		private uint ProcessId;
		private string WindowTitle;

		public NamechangeEvent(uint processId, string v)
		{
			ProcessId = processId;
			WindowTitle = v;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return Current.Messages.Namechange;
		}
	}
}