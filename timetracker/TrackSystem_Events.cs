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
		class BeginEventType : TokenValue
		{
			public byte Type;
			public int PID, ParentId;
			public string AppTag;
			public string RuleSet;

			public BeginEventType(int pid, string appTag, string ruleSet, int parentId)
			{
				Type = MessageHeader_Begin;
				PID = pid;
				AppTag = appTag;
				RuleSet = ruleSet;
				ParentId = parentId;
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

				buff = BitConverter.GetBytes(ParentId);
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
				Type = MessageHeader_End;
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

		class VersionEventType : TokenValue
		{
			public byte Type;
			public string Version;

			public VersionEventType(string ver)
			{
				Type = MessageHeader_Version;
				Version = ver;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = Version.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

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
				Type = MessageHeader_AddDefinition;

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
				Type = MessageHeader_RemoveDefinition;

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

		internal ulong ReciverSpeed
		{
			get
			{
				return 0;
			}
		}

		internal ulong ReciverData
		{
			get
			{
				return 0;
			}
		}

		internal ulong SentSpeed
		{
			get
			{
				return 0;
			}
		}

		internal ulong SentData
		{
			get
			{
				return 0;
			}
		}

		class PCInfoEventType : TokenValue
		{
			public byte Type;
			public string PcName, UserName, OsVersion;

			public PCInfoEventType()
			{
				Type = MessageHeader_PCName;
				PcName = Environment.MachineName;
				UserName = Environment.UserName;
				OsVersion = Environment.OSVersion.Platform.ToString() + " " +Environment.OSVersion.VersionString;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = PcName.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = UserName.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = OsVersion.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}
	}
}
