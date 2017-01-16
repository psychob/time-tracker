using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.Messages.v3_16.Constants;

namespace timetracker
{
	public partial class TrackSystem
	{
		class NamechangeToken : TokenValue
		{
			public byte Type;
			public uint PID;
			public string Name;

			public NamechangeToken(uint pid, string name)
			{
				Type = MessageHeader_Namechange;
				PID = pid;
				Name = name;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				// work
				buff = BitConverter.GetBytes(PID);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = Name.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		uint LastNameChangePID = 0;
		string LastNameChangeTitle = "";

		private void NamechangeEvent(uint PID, string winTitle)
		{
			DateTime x = DateTime.Now;

			if (PID == LastNameChangePID && winTitle == LastNameChangeTitle)
				return;

			LastNameChangePID = PID;
			LastNameChangeTitle = winTitle;

			AppendBinary(new NamechangeToken(PID, winTitle), x);
		}
	}
}
