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

		class NetworkBandwidthEvent : TokenValue
		{
			public byte Type;
			public ulong Recivied, Send;
			public string Name;

			public NetworkBandwidthEvent(string n, ulong r, ulong s)
			{
				Type = MessageHeader_NetworkBandwidth;

				Recivied = r;
				Send = s;
				Name = n;
			}

			public int AsByteStream(ref byte[] str, int start, int length)
			{
				int Written = 0;
				byte[] buff;

				str[start + Written++] = Type;

				buff = BitConverter.GetBytes(Recivied);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = BitConverter.GetBytes(Send);
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				buff = Name.GetBytesEncoded();
				buff.CopyTo(str, start + Written);
				Written += buff.Length;

				return Written;
			}
		}

		struct InternetData
		{
			public DateTime Time;
			public ulong Data;

			public InternetData(DateTime d, ulong u)
			{
				Time = d;
				Data = u;
			}
		}

		RingBuffer<InternetData> ReciverSpeedData = new RingBuffer<InternetData>(4);
		RingBuffer<InternetData> SentSpeedData = new RingBuffer<InternetData>(4);

		internal ulong ReciverSpeed
		{
			get
			{
				if (ReciverSpeedData.Count <= 2)
					return 0;

				InternetData id;
				ReciverSpeedData.Bottom(out id);

				DateTime low = id.Time;
				ulong sum = 0;

				foreach (var it in ReciverSpeedData)
					sum += it.Data;

				return (ulong)(sum / (DateTime.Now - low).TotalSeconds);
			}
		}

		internal ulong ReciverData
		{
			get; private set;
		}

		internal ulong SentSpeed
		{
			get
			{
				if (SentSpeedData.Count <= 2)
					return 0;

				InternetData id;
				SentSpeedData.Bottom(out id);

				DateTime low = id.Time;
				ulong sum = 0;

				foreach (var it in SentSpeedData)
					sum += it.Data;

				return (ulong)(sum / (DateTime.Now - low).TotalSeconds);
			}
		}

		internal ulong SentData
		{
			get; private set;
		}

		struct SendedData
		{
			public ulong Recived;
			public ulong Send;
		}

		Dictionary<string, SendedData> sdata = new Dictionary<string, SendedData>();

		void NetworkBandwitch(string Name, ulong Recivied, ulong Send)
		{
			SendedData sdv = new SendedData();

			if (!sdata.TryGetValue(Name, out sdv))
			{
				sdv.Recived = Recivied;
				sdv.Send = Send;

				sdata.Add(Name, sdv);
			}

			var d = DateTime.Now;

			sdv = sdata[Name];

			if (Recivied == 0 && Send == 0)
			{
				sdv.Recived = 0;
				sdv.Send = 0;

				ReciverSpeedData.Add(new InternetData(d, 0));
				SentSpeedData.Add(new InternetData(d, 0));
			} else if (sdv.Recived == 0 && sdv.Send == 0)
			{
				sdv.Recived = Recivied;
				sdv.Send = Send;
			} else
			{
				var x = Recivied - sdv.Recived;
				var y = Send - sdv.Send;

				AppendBinary(new NetworkBandwidthEvent(Name, x, y), d);

				ReciverSpeedData.Add(new InternetData(d, x));
				SentSpeedData.Add(new InternetData(d, y));

				ReciverData += x;
				SentData += y;

				sdv.Recived = Recivied;
				sdv.Send = Send;
			}

			sdata[Name] = sdv;
		}
	}
}
