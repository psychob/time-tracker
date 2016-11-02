using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static WinAPI.User32;
using static WinAPI.WinDef;
using static WinAPI.WinUser;

namespace timetracker
{
	class KeyboardHook
	{
		public delegate void KeyEventType(uint virtualKode, uint scanKode, bool up);

		internal KeyEventType keyEvent;

		private bool[] KeyState = new bool[255];
		private SetWindowsHookProc keyDelegate;
		private IntPtr hHook = IntPtr.Zero;

		public bool Init()
		{
			KeyState.Fill(false);

			keyDelegate = new SetWindowsHookProc(KeyHook);
			hHook = SetWindowsHookEx(SetWindowsHookType.WH_KEYBOARD_LL, keyDelegate,
				IntPtr.Zero, 0);

			return hHook != IntPtr.Zero;
		}

		public void DeInit()
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

				switch (kMsg)
				{
					case WindowMessage.WM_KEYDOWN:
					case WindowMessage.WM_SYSKEYDOWN:
						if (KeyState[kInfo.vkCode] == false)
						{
							keyEvent(kInfo.vkCode, kInfo.scanCode, true);
							KeyState[kInfo.vkCode] = true;
						}
						break;

					case WindowMessage.WM_KEYUP:
					case WindowMessage.WM_SYSKEYUP:
						if (KeyState[kInfo.vkCode] == true)
						{
							keyEvent(kInfo.vkCode, kInfo.scanCode, false);
							KeyState[kInfo.vkCode] = false;
						}
						break;
				}
			}

			return CallNextHookEx(hHook, code, wParam, lParam);
		}
	}
}
