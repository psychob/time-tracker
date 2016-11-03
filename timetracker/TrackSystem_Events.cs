using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker
{
	public partial class TrackSystem
	{
		class BeginEventType : TokenValue
		{
			public byte Type;
			public int PID;
			public string AppTag;
			public string RuleSet;

			public BeginEventType(int pid, string appTag, string ruleSet)
			{
				Type = MessageHeader_Begin;
				PID = pid;
				AppTag = appTag;
				RuleSet = ruleSet;
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

				buff = AppTag.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = RuleSet.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class EndEventType : TokenValue
		{
			public byte Type;
			public int PID;
			public string AppTag;

			public EndEventType(int pid, string appTag)
			{
				Type = MessageHeader_Begin;
				PID = pid;
				AppTag = appTag;
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

				buff = AppTag.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class PingEventType : TokenValue
		{
			public byte Type;

			public PingEventType()
			{
				Type = MessageHeader_KeppAlive;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;

				str[start + Written++] = Type;

				return Written;
			}
		}

		class ResolutionChange : TokenValue
		{
			public byte Type;
			public int Width, Height;

			public ResolutionChange(int width, int height)
			{
				Type = MessageHeader_ResolutionChange;

				Width = width;
				Height = height;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = BitConverter.GetBytes(Width);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = BitConverter.GetBytes(Height);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class AddNewDefinitionType : TokenValue
		{
			public byte Type;
			public string AppTag, Name;

			public AddNewDefinitionType(string appTag, string name)
			{
				Type = MessageHeader_ResolutionChange;

				AppTag = appTag;
				Name = name;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = AppTag.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = Name.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class RemoveDefinition : TokenValue
		{
			public byte Type;
			public string AppTag;

			public RemoveDefinition(string appTag)
			{
				Type = MessageHeader_ResolutionChange;

				AppTag = appTag;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = AppTag.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		class NetworkAdapterDefinition : TokenValue
		{
			public byte Type;
			public string Name;
			public Guid Guid;

			public NetworkAdapterDefinition(string name, Guid guid)
			{
				Type = MessageHeader_NetworkAdapter;
				Name = name;
				Guid = guid;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = Name.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = Guid.ToString().GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

	}
}
