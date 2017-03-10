using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class Win32_Perf : CIM_StatisticalInformation
	{
		public UInt64? Frequency_Object { get; private set; }

		public UInt64? Frequency_PerfTime { get; private set; }

		public UInt64? Frequency_Sys100NS { get; private set; }

		public UInt64? Timestamp_Object { get; private set; }

		public UInt64? Timestamp_PerfTime { get; private set; }

		public UInt64? Timestamp_Sys100NS { get; private set; }

		public Win32_Perf(ManagementBaseObject mbo) : base(mbo)
		{
			Frequency_Object = GetValue<UInt64>(mbo, nameof(Frequency_Object));
			Frequency_PerfTime = GetValue<UInt64>(mbo, nameof(Frequency_PerfTime));
			Frequency_Sys100NS = GetValue<UInt64>(mbo, nameof(Frequency_Sys100NS));
			Timestamp_Object = GetValue<UInt64>(mbo, nameof(Timestamp_Object));
			Timestamp_PerfTime = GetValue<UInt64>(mbo, nameof(Timestamp_PerfTime));
			Timestamp_Sys100NS = GetValue<UInt64>(mbo, nameof(Timestamp_Sys100NS));
		}
	}
}
