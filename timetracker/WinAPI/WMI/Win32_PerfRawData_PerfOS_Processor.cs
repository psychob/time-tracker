using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	class Win32_PerfRawData_PerfOS_Processor : Win32_PerfRawData
	{
		public UInt64? C1TransitionsPerSec { get; private set; }

		public UInt64? C2TransitionsPerSec { get; private set; }

		public UInt64? C3TransitionsPerSec { get; private set; }

		public UInt32? DPCRate { get; private set; }

		public UInt32? DPCsQueuedPerSec { get; private set; }

		public UInt32? InterruptsPerSec { get; private set; }

		public UInt64? PercentC1Time { get; private set; }

		public UInt64? PercentC2Time { get; private set; }

		public UInt64? PercentC3Time { get; private set; }

		public UInt64? PercentDPCTime { get; private set; }

		public UInt64? PercentIdleTime { get; private set; }

		public UInt64? PercentInterruptTime { get; private set; }

		public UInt64? PercentPrivilegedTime { get; private set; }

		public UInt64? PercentProcessorTime { get; private set; }

		public UInt64? PercentUserTime { get; private set; }

		public const string ClassName = "Win32_PerfRawData_PerfOS_Processor";

		public const string Namespace = @"\\.\root\CIMV2";

		public Win32_PerfRawData_PerfOS_Processor(ManagementBaseObject mbo) : base(mbo)
		{
			C1TransitionsPerSec = GetValue<UInt64>(mbo, nameof(C1TransitionsPerSec));
			C2TransitionsPerSec = GetValue<UInt64>(mbo, nameof(C2TransitionsPerSec));
			C3TransitionsPerSec = GetValue<UInt64>(mbo, nameof(C3TransitionsPerSec));
			DPCRate = GetValue<UInt32>(mbo, nameof(DPCRate));
			DPCsQueuedPerSec = GetValue<UInt32>(mbo, nameof(DPCsQueuedPerSec));
			InterruptsPerSec = GetValue<UInt32>(mbo, nameof(InterruptsPerSec));
			PercentC1Time = GetValue<UInt64>(mbo, nameof(PercentC1Time));
			PercentC2Time = GetValue<UInt64>(mbo, nameof(PercentC2Time));
			PercentC3Time = GetValue<UInt64>(mbo, nameof(PercentC3Time));
			PercentDPCTime = GetValue<UInt64>(mbo, nameof(PercentDPCTime));
			PercentIdleTime = GetValue<UInt64>(mbo, nameof(PercentIdleTime));
			PercentInterruptTime = GetValue<UInt64>(mbo, nameof(PercentInterruptTime));
			PercentPrivilegedTime = GetValue<UInt64>(mbo, nameof(PercentPrivilegedTime));
			PercentProcessorTime = GetValue<UInt64>(mbo, nameof(PercentProcessorTime));
			PercentUserTime = GetValue<UInt64>(mbo, nameof(PercentUserTime));
		}

		public static List<Win32_PerfRawData_PerfOS_Processor> Fetch()
		{
			return FetchImpl(Namespace, ClassName,
				p => new Win32_PerfRawData_PerfOS_Processor(p));
		}

		public static ManagementEventWatcher Watch(int Interval,
			WatchType Type, Action<Win32_PerfRawData_PerfOS_Processor> onEvent)
		{
			return WatchImpl(Interval, Namespace, ClassName, Type, onEvent,
				p => new Win32_PerfRawData_PerfOS_Processor(p));
		}
	}
}
