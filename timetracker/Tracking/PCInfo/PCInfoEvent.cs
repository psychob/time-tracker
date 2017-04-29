using System;
using timetracker.Messages;

namespace timetracker.Tracking.PCInfo
{
	internal class PCInfoEvent : IMessage
	{
		private string MachineName;
		private string UserName;
		private string WindowsType;
		private string WindowsVersion;

		public PCInfoEvent(string machineName, string userName, string v, string versionString)
		{
			MachineName = machineName;
			UserName = userName;
			WindowsType = v;
			WindowsVersion = versionString;
		}

		public int AsByteStream(ref byte[] outputStream, int start, int length)
		{
			throw new NotImplementedException();
		}

		public string GetMessageType()
		{
			return Current.Messages.PCInfo;
		}
	}
}