using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.TrackSystem;

namespace timetracker
{
	class NamechangeHook
	{
		public delegate void NamechangeHookType(uint processID, string winTitle);

		private IntPtr hHook;
		private WinAPI.WinEventProc wpDel;

		internal NamechangeHookType namechangeEvent;

		public bool Init()
		{
			wpDel = new WinAPI.WinEventProc(eventArrived);

			hHook = WinAPI.SetWinEventHook(WinAPI.EVENT_OBJECT_NAMECHANGE,
				WinAPI.EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
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
			if (idObject == WinAPI.OBJID_WINDOW && idChild == WinAPI.CHILDID_SELF)
			{
				if (WinAPI.GetAncestor(hwnd, WinAPI.GetAncestorFlags.GetParent) != IntPtr.Zero)
					return;

				uint ProcessId;
				WinAPI.GetWindowThreadProcessId(hwnd, out ProcessId);

				StringBuilder sb = new StringBuilder(1024);
				int written = WinAPI.GetWindowText(hwnd, sb, 1024);

				if (written != 0)
					namechangeEvent(ProcessId, sb.ToString());
			}
		}
	}
}
