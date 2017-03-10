using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class Win32_PerfRawData : Win32_Perf
	{
		public Win32_PerfRawData(ManagementBaseObject mbo) : base(mbo)
		{
		}
	}
}
