using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.TrackSystem;

namespace timetracker
{
	class ForegroundHook
	{
		public delegate void ForegroundChangedType(uint threadId, uint processID);

		private IntPtr hHook;
		private WinAPI.WinEventProc wpDel;

		internal ForegroundChangedType foregroundChanged;

		public bool Init()
		{
			wpDel = new WinAPI.WinEventProc(eventArrived);

			hHook = WinAPI.SetWinEventHook(WinAPI.EVENT_SYSTEM_FOREGROUND,
				WinAPI.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero,
				wpDel, 0, 0, WinAPI.WINEVENT_OUTOFCONTEXT);

			return hHook == IntPtr.Zero;
		}

		public void DeInit()
		{
			WinAPI.UnhookWinEvent(hHook);

			hHook = IntPtr.Zero;
		}

		private void eventArrived(IntPtr hWinEventHook,
				uint eventType, IntPtr hwnd, int idObject, int idChild,
				uint dwEventThread, uint dwmsEventTime)
		{
			uint ProcessId;
			uint threadID = WinAPI.GetWindowThreadProcessId(hwnd, out ProcessId);

			foregroundChanged(threadID, ProcessId);
		}
	}
}
