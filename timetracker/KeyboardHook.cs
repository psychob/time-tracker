using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static timetracker.TrackSystem;

namespace timetracker
{
	class KeyboardHook
	{
		public delegate void KeyEventType(uint virtualKode, uint scanKode, bool up);

		internal KeyEventType keyEvent;

		private bool[] KeyState = new bool[255];
		private WinAPI.HookProc keyDelegate;
		private int hHook = 0;

		public bool Init()
		{
			KeyState.Fill(false);

			keyDelegate = new WinAPI.HookProc(KeyHook);
			hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_KEYBOARD_LL, keyDelegate, IntPtr.Zero, 0);

			return hHook != 0;
		}

		public void DeInit()
		{
			if (hHook != 0)
			{
				WinAPI.UnhookWindowsHookEx(hHook);
				hHook = 0;
			}
		}

		private int KeyHook(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code >= 0)
			{
				var kInfo = (WinAPI.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(WinAPI.KBDLLHOOKSTRUCT));
				var kMsg = (int)wParam;

				switch (kMsg)
				{
					case WinAPI.WM_KEYDOWN:
					case WinAPI.WM_SYSKEYDOWN:
						if (KeyState[kInfo.vkCode] == false)
						{
							keyEvent(kInfo.vkCode, kInfo.scanCode, true);
							KeyState[kInfo.vkCode] = true;
						}
						break;

					case WinAPI.WM_KEYUP:
					case WinAPI.WM_SYSKEYUP:
						if (KeyState[kInfo.vkCode] == true)
						{
							keyEvent(kInfo.vkCode, kInfo.scanCode, false);
							KeyState[kInfo.vkCode] = false;
						}
						break;
				}
			}

			return WinAPI.CallNextHookEx(hHook, code, wParam, lParam);
		}
	}
}
