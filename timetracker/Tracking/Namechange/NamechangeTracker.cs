using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using static timetracker.WinAPI.User32;

namespace timetracker.Tracking.Namechange
{
	class NamechangeTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.Namechange,
		};

		private IDatabase Db;
		private ILog Logger = LogManager.GetLogger(typeof(NamechangeTracker));

		private IntPtr hHook;
		private SetWinEventHookProc wpDel;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "Namechange";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			wpDel = new SetWinEventHookProc(eventArrived);

			hHook = SetWinEventHook(SetWinEventHookType.EVENT_OBJECT_NAMECHANGE,
				SetWinEventHookType.EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
				wpDel, 0, 0, SetWinEventHookFlags.WINEVENT_OUTOFCONTEXT);
		}

		public void TearDown()
		{
			UnhookWinEvent(hHook);

			hHook = IntPtr.Zero;
		}

		private void eventArrived(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
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
					Db.AppendMessage(new NamechangeEvent(ProcessId, sb.ToString()));
			}
		}
	}
}
