using System;
using System.Text;

using static timetracker.WinAPI.User32;

namespace timetracker
{
	class NamechangeHook
	{
		public delegate void NamechangeHookType(uint processID, string winTitle);

		private IntPtr hHook;
		private SetWinEventHookProc wpDel;

		internal NamechangeHookType namechangeEvent;

		public bool Init()
		{
			wpDel = new SetWinEventHookProc(eventArrived);

			hHook = SetWinEventHook(SetWinEventHookType.EVENT_OBJECT_NAMECHANGE,
				SetWinEventHookType.EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
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
			if (idObject == OBJID_WINDOW && idChild == CHILDID_SELF)
			{
				if (GetAncestor(hwnd, GetAncestorFlags.GetParent) != IntPtr.Zero)
					return;

				uint ProcessId;
				GetWindowThreadProcessId(hwnd, out ProcessId);

				StringBuilder sb = new StringBuilder(1024);
				int written = GetWindowText(hwnd, sb, 1024);

				if (written != 0)
					namechangeEvent(ProcessId, sb.ToString());
			}
		}
	}
}
