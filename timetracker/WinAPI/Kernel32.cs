using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI
{
	public static class Kernel32
	{
		/// <summary>
		/// Retrieves the number of milliseconds that have elapsed since the
		/// system was started, up to 49.7 days
		/// </summary>
		/// <returns>
		/// The return value is the number of milliseconds that have elapsed
		/// since the system was started.
		/// </returns>
		/// <remarks>
		/// The resolution of the GetTickCount function is limited to the
		/// resolution of the system timer, which is typically in the range of
		/// 10 milliseconds to 16 milliseconds. The resolution of the GetTickCount
		/// function is not affected by adjustments made by the
		/// GetSystemTimeAdjustment function.
		/// The elapsed time is stored as a DWORD value. Therefore, the time will
		/// wrap around to zero if the system is run continuously for 49.7 days.
		/// To avoid this problem, use the GetTickCount64 function. Otherwise,
		/// check for an overflow condition when comparing times.
		/// If you need a higher resolution timer, use a multimedia timer or a
		/// high-resolution timer.
		/// To obtain the time elapsed since the computer was started, retrieve
		/// the System Up Time counter in the performance data in the registry
		/// key HKEY_PERFORMANCE_DATA. The value returned is an 8-byte value.
		/// For more information, see Performance Counters.
		/// To obtain the time the system has spent in the working state since it
		/// was started, use the QueryUnbiasedInterruptTime function.
		/// </remarks>
		[DllImport("kernel32.dll", ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern uint GetTickCount();

		/// <summary>
		/// Retrieves the number of milliseconds that have elapsed since the
		/// system was started.
		/// </summary>
		/// <returns>
		/// The number of milliseconds
		/// </returns>
		/// <remarks>
		/// The resolution of the GetTickCount64 function is limited to the
		/// resolution of the system timer, which is typically in the range of
		/// 10 milliseconds to 16 milliseconds. The resolution of the
		/// GetTickCount64 function is not affected by adjustments made by
		/// the GetSystemTimeAdjustment function.
		/// If you need a higher resolution timer, use a multimedia timer or a
		/// high-resolution timer.
		/// To obtain the time the system has spent in the working state since
		/// it was started, use the QueryUnbiasedInterruptTime function.
		/// </remarks>
		[DllImport("kernel32.dll", ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern ulong GetTickCount64();
	}
}
