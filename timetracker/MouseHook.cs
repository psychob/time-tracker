using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static timetracker.WinAPI.User32;
using static timetracker.WinAPI.WinDef;
using static timetracker.WinAPI.WinUser;

namespace timetracker
{
	public class MouseHook
	{
		public enum MouseButton : int
		{
			Right = 'R',
			Left = 'L',
			Middle = 'M',
			X1 = 'X',
			X2 = 'Y',
			X3 = 'Z',
			X4 = 'A',
			X5 = 'B',
		}

		public enum MouseAxis : int
		{
			Vertical, Horizontal
		}
	}
}
