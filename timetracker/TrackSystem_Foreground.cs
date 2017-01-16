using System;

using static timetracker.Messages.v3_16.Constants;

namespace timetracker
{
	public partial class TrackSystem
	{
		class ForegroundToken : TokenValue
		{
			public byte Type;
			public uint PID;

			public ForegroundToken(uint pid)
			{
				Type = MessageHeader_ForegroundChange;
				PID = pid;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				// vk
				buff = BitConverter.GetBytes(PID);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		uint LastForegroundProcessID = 0;

		private void ForegroundEvent(uint ThreadId, uint ProcessID)
		{
			if (LastForegroundProcessID == ProcessID)
				return;

			AppendBinary(new ForegroundToken(ProcessID));

			LastForegroundProcessID = ProcessID;
		}
	}
}
