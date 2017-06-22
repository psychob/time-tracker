using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI
{
	public static class WinDef
	{
		/// <summary>
		/// Retrieves the low-order word from the specified value.
		/// </summary>
		/// <param name="value">
		/// The value to be converted.
		/// </param>
		/// <returns>
		/// The return value is the low-order word of the specified value.
		/// </returns>
		/// <remarks>
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms632659(v=vs.85).aspx
		/// </remarks>
		public static short LOWORD(int value)
		{
			return (short)(value & 0xFFFF);
		}

		/// <summary>
		/// Retrieves the low-order word from the specified value.
		/// </summary>
		/// <param name="value">
		/// The value to be converted.
		/// </param>
		/// <returns>
		/// The return value is the low-order word of the specified value.
		/// </returns>
		/// <remarks>
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms632659(v=vs.85).aspx
		/// </remarks>
		public static short LOWORD(uint value)
		{
			return (short)(value & 0xFFFF);
		}

		/// <summary>
		/// Retrieves the high-order word from the specified 32-bit value.
		/// </summary>
		/// <param name="value">
		/// The value to be converted.
		/// </param>
		/// <returns>
		/// The return value is the high-order word of the specified value.
		/// </returns>
		/// <remarks>
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms632657(v=vs.85).aspx
		/// </remarks>
		public static short HIWORD(int value)
		{
			return (short)((value >> 16) & 0xFFFF);
		}

		/// <summary>
		/// Retrieves the high-order word from the specified 32-bit value.
		/// </summary>
		/// <param name="value">
		/// The value to be converted.
		/// </param>
		/// <returns>
		/// The return value is the high-order word of the specified value.
		/// </returns>
		/// <remarks>
		/// https://msdn.microsoft.com/en-us/library/windows/desktop/ms632657(v=vs.85).aspx
		/// </remarks>
		public static short HIWORD(uint value)
		{
			return (short)((value >> 16) & 0xFFFF);
		}
	}
}
