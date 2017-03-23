using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using static timetracker.WinAPI.User32;
using static timetracker.WinAPI.WinUser;

namespace timetracker.Tracking.Keyboard
{
	class KeyboardTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.KeyboardPress,
			Current.Messages.KeyboardRelease
		};

		private IDatabase Db;
		private ManagementEventWatcher Tracker;
		private ILog Logger = LogManager.GetLogger(typeof(KeyboardTracker));

		private bool[] KeyState = new bool[256];
		private SetWindowsHookProc keyDelegate;
		private IntPtr hHook = IntPtr.Zero;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "Keyboard";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			Array.Clear(KeyState, 0, KeyState.Length);

			keyDelegate = new SetWindowsHookProc(KeyHook);
			hHook = SetWindowsHookEx(SetWindowsHookType.WH_KEYBOARD_LL, keyDelegate,
				IntPtr.Zero, 0);
		}

		public void TearDown()
		{
			if (hHook != IntPtr.Zero)
			{
				UnhookWindowsHookEx(hHook);
				hHook = IntPtr.Zero;
			}
		}

		private int KeyHook(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code >= 0)
			{
				var kInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
				var kMsg = (WindowMessage)wParam;
				KeyEvent evnt = null;

				switch (kMsg)
				{
					case WindowMessage.WM_KEYDOWN:
					case WindowMessage.WM_SYSKEYDOWN:
						if (KeyState[kInfo.vkCode] == false)
						{
							evnt = new KeyPressEvent(kInfo.vkCode, kInfo.scanCode);
							KeyState[kInfo.vkCode] = true;
						}
						break;

					case WindowMessage.WM_KEYUP:
					case WindowMessage.WM_SYSKEYUP:
						if (KeyState[kInfo.vkCode] == true)
						{
							evnt = new KeyReleaseEvent(kInfo.vkCode, kInfo.scanCode);
							KeyState[kInfo.vkCode] = false;
						}
						break;
				}

				if (evnt != null)
					Db.AppendMessage(evnt);
			}

			return CallNextHookEx(hHook, code, wParam, lParam);
		}
	}
}
