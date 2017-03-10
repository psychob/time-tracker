using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using timetracker.WinAPI.WMI;

namespace timetracker.Tracking.NetworkBandwidth
{
	class NetworkBandwidthTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.NetworkLoad
		};

		private IDatabase Db;
		private ManagementEventWatcher Tracker;
		private ILog Logger = LogManager.GetLogger(typeof(NetworkBandwidthTracker));

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "Network Bandwidth";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			Tracker = Win32_PerfRawData_Tcpip_NetworkInterface.Watch(1,
				BaseClass.WatchType.Modification, OnMod);
		}

		public void TearDown()
		{
			Tracker.Dispose();
		}

		struct NetData
		{
			public ulong Recived, Send;
		}

		Dictionary<string, NetData> Cache = new Dictionary<string, NetData>();

		private void OnMod(Win32_PerfRawData_Tcpip_NetworkInterface obj)
		{
			if (obj.Name.IsEmptyOrNull())
				return;

			NetData sdv;

			ulong Recivied = obj.BytesReceivedPerSec.GetValueOrDefault(0);
			ulong Send = obj.BytesSentPerSec.GetValueOrDefault(0);

			if (!Cache.TryGetValue(obj.Name, out sdv))
			{
				sdv.Recived = Recivied;
				sdv.Send = Send;

				Cache.Add(obj.Name, sdv);

				return;
			}

			if (Recivied == 0 && Send == 0)
			{
				sdv.Recived = 0;
				sdv.Send = 0;
			} else if (sdv.Recived == 0 && sdv.Send == 0)
			{
				sdv.Recived = Recivied;
				sdv.Send = Send;
			} else
			{
				var x = Recivied - sdv.Recived;
				var y = Send - sdv.Send;

				sdv.Recived = Recivied;
				sdv.Send = Send;

				Db.AppendMessage(new NetworkBandwidthEvent(obj.Name, x, y));
			}

			Cache[obj.Name] = sdv;
		}
	}
}
