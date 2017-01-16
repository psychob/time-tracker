using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.Messages.v3_16.Constants;

namespace timetracker
{
	public partial class TrackSystem
	{
		class ProcessLoadToken : TokenValue
		{
			public byte Type;
			public string Name;
			public int Work;
			public int Kernel;
			public int Idle;

			public ProcessLoadToken(string name, ulong WorkProc, ulong KernelPorc,
				ulong IdleProc)
			{
				Type = MessageHeader_Processor;
				Name = name;
				Work = (int)WorkProc;
				Kernel = (int)KernelPorc;
				Idle = (int)IdleProc;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				// work
				buff = BitConverter.GetBytes(Work);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// kernel
				buff = BitConverter.GetBytes(Kernel);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				// idle
				buff = BitConverter.GetBytes(Idle);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = Name.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		void ProcessorLoad(string Name, ulong I, ulong K, ulong W)
		{
			AppendBinary(new ProcessLoadToken(Name, W, K, I));
		}
	}
}
