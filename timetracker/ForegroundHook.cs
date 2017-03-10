using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.User32;
using static timetracker.WinDef;
using static timetracker.WinUser;

namespace timetracker
{
	class ForegroundHook
	{
		public delegate void ForegroundChangedType(uint threadId, uint processID);

		private IntPtr hHook;
		private SetWinEventHookProc wpDel;

		internal ForegroundChangedType foregroundChanged;

		public bool Init()
		{
			wpDel = new SetWinEventHookProc(eventArrived);

			hHook = SetWinEventHook(SetWinEventHookType.EVENT_SYSTEM_FOREGROUND,
				SetWinEventHookType.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero,
				wpDel, 0, 0, SetWinEventHookFlags.WINEVENT_OUTOFCONTEXT);

			return hHook == IntPtr.Zero;
		}

		public void DeInit()
		{
			UnhookWinEvent(hHook);

			hHook = IntPtr.Zero;
		}

		private void eventArrived(IntPtr hWinEventHook,
				uint eventType, IntPtr hwnd, int idObject, int idChild,
				uint dwEventThread, uint dwmsEventTime)
		{
			uint ProcessId;
			uint threadID = GetWindowThreadProcessId(hwnd, out ProcessId);

			foregroundChanged(threadID, ProcessId);
		}
	}
}
