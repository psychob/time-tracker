using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using static timetracker.WinAPI.User32;

namespace timetracker.Tracking.Foreground
{
	class ForegroundTracker: ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.ForegroundChange,
		};

		private IDatabase Db;
		private ILog Logger = LogManager.GetLogger(typeof(ForegroundTracker));

		private IntPtr hHook;
		private SetWinEventHookProc wpDel;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public string GetName()
		{
			return "Foreground";
		}

		public void SetUp()
		{
			wpDel = new SetWinEventHookProc(eventArrived);

			hHook = SetWinEventHook(SetWinEventHookType.EVENT_SYSTEM_FOREGROUND,
				SetWinEventHookType.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero,
				wpDel, 0, 0, SetWinEventHookFlags.WINEVENT_OUTOFCONTEXT);
		}

		public void TearDown()
		{
			UnhookWinEvent(hHook);
		}

		private void eventArrived(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
		{
			uint ProcessId;
			uint threadID = GetWindowThreadProcessId(hwnd, out ProcessId);

			Db.AppendMessage(new ForegroundEvent(ProcessId, threadID));
		}
	}
}
