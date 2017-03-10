using System;
using timetracker.Messages;

namespace timetracker.Tracking.NetworkBandwidth
{
	internal class NetworkBandwidthEvent : IMessage
	{
		private string Name;
		private ulong Recived;
		private ulong Sent;

		public NetworkBandwidthEvent(string name, ulong x, ulong y)
		{
			Name = name;
			Recived = x;
			Sent = y;
		}

		public string GetMessageType()
		{
			return Current.Messages.NetworkLoad;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}
	}
}