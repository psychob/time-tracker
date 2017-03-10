using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class Win32_PerfFormattedData : Win32_Perf
	{
		public Win32_PerfFormattedData(ManagementBaseObject mbo) : base(mbo)
		{
		}
	}
}
