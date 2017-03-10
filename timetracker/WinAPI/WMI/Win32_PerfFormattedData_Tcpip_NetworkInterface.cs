using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	class Win32_PerfFormattedData_Tcpip_NetworkInterface : Win32_PerfFormattedData
	{
		public UInt32? BytesReceivedPerSec { get; private set; }

		public UInt32? BytesSentPerSec { get; private set; }

		public UInt64? BytesTotalPerSec { get; private set; }

		public UInt32? CurrentBandwidth { get; private set; }

		public UInt32? OutputQueueLength { get; private set; }

		public UInt32? PacketsOutboundDiscarded { get; private set; }

		public UInt32? PacketsOutboundErrors { get; private set; }

		public UInt32? PacketsPerSec { get; private set; }

		public UInt32? PacketsReceivedDiscarded { get; private set; }

		public UInt32? PacketsReceivedErrors { get; private set; }

		public UInt32? PacketsReceivedNonUnicastPerSec { get; private set; }

		public UInt32? PacketsReceivedPerSec { get; private set; }

		public UInt32? PacketsReceivedUnicastPerSec { get; private set; }

		public UInt32? PacketsReceivedUnknown { get; private set; }

		public UInt32? PacketsSentNonUnicastPerSec { get; private set; }

		public UInt32? PacketsSentPerSec { get; private set; }

		public UInt32? PacketsSentUnicastPerSec { get; private set; }

		public const string ClassName = "Win32_PerfFormattedData_Tcpip_NetworkInterface";

		public const string Namespace = @"\\.\root\CIMV2";

		public Win32_PerfFormattedData_Tcpip_NetworkInterface(ManagementBaseObject mbo) : base(mbo)
		{
			BytesReceivedPerSec = GetValue<UInt32>(mbo, nameof(BytesReceivedPerSec));
			BytesSentPerSec = GetValue<UInt32>(mbo, nameof(BytesSentPerSec));
			BytesTotalPerSec = GetValue<UInt64>(mbo, nameof(BytesTotalPerSec));
			CurrentBandwidth = GetValue<UInt32>(mbo, nameof(CurrentBandwidth));
			OutputQueueLength = GetValue<UInt32>(mbo, nameof(OutputQueueLength));
			PacketsOutboundDiscarded = GetValue<UInt32>(mbo, nameof(PacketsOutboundDiscarded));
			PacketsOutboundErrors = GetValue<UInt32>(mbo, nameof(PacketsOutboundErrors));
			PacketsPerSec = GetValue<UInt32>(mbo, nameof(PacketsPerSec));
			PacketsReceivedDiscarded = GetValue<UInt32>(mbo, nameof(PacketsReceivedDiscarded));
			PacketsReceivedErrors = GetValue<UInt32>(mbo, nameof(PacketsReceivedErrors));
			PacketsReceivedNonUnicastPerSec = GetValue<UInt32>(mbo, nameof(PacketsReceivedNonUnicastPerSec));
			PacketsReceivedPerSec = GetValue<UInt32>(mbo, nameof(PacketsReceivedPerSec));
			PacketsReceivedUnicastPerSec = GetValue<UInt32>(mbo, nameof(PacketsReceivedUnicastPerSec));
			PacketsReceivedUnknown = GetValue<UInt32>(mbo, nameof(PacketsReceivedUnknown));
			PacketsSentNonUnicastPerSec = GetValue<UInt32>(mbo, nameof(PacketsSentNonUnicastPerSec));
			PacketsSentPerSec = GetValue<UInt32>(mbo, nameof(PacketsSentPerSec));
			PacketsSentUnicastPerSec = GetValue<UInt32>(mbo, nameof(PacketsSentUnicastPerSec));
		}

		public static List<Win32_PerfFormattedData_Tcpip_NetworkInterface> Fetch()
		{
			return FetchImpl(Namespace, ClassName,
				p => new Win32_PerfFormattedData_Tcpip_NetworkInterface(p));
		}

		public static ManagementEventWatcher Watch(int Interval,
			WatchType Type, Action<Win32_PerfFormattedData_Tcpip_NetworkInterface> onEvent)
		{
			return WatchImpl(Interval, Namespace, ClassName,
				Type, onEvent, p => new Win32_PerfFormattedData_Tcpip_NetworkInterface(p));
		}
	}
}
