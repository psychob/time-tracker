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

		public delegate void MouseClickEventType(MouseButton btn, bool pressed, int x, int y);
		public delegate void MouseWheelMoveEventType(MouseAxis axis, int value, int x, int y);
		public delegate void MouseMoveEventType(int x, int y);

		public MouseClickEventType mouseClickEvent;
		public MouseWheelMoveEventType mouseWheelMoveEvent;
		public MouseMoveEventType mouseMoveEvent;

		private WinAPI.HookProc mouseDelegate;
		private int hHook = 0;

		public bool Init()
		{
			mouseDelegate = new WinAPI.HookProc(ProcessMouseHookEvent);
			hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);

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

		private MouseButton EventToButton(int mMsg, int edata)
		{
			switch (mMsg)
			{
				case WinAPI.WM_LBUTTONUP:
				case WinAPI.WM_LBUTTONDOWN:
					return MouseButton.Left;

				case WinAPI.WM_RBUTTONUP:
				case WinAPI.WM_RBUTTONDOWN:
					return MouseButton.Right;

				case WinAPI.WM_MBUTTONDOWN:
				case WinAPI.WM_MBUTTONUP:
					return MouseButton.Middle;

				case WinAPI.WM_XBUTTONDOWN:
				case WinAPI.WM_XBUTTONUP:
					switch (edata)
					{
						case 1:
							return MouseButton.X1;

						case 2:
							return MouseButton.X2;

						case 3:
							return MouseButton.X3;

						case 4:
							return MouseButton.X4;

						default:
							return MouseButton.X5;
					}

				default:
					return MouseButton.X5;
			}
		}

		private int ProcessMouseHookEvent(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code >= 0)
			{
				var kInfo = (WinAPI.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(WinAPI.MSLLHOOKSTRUCT));
				var kMsg = (int)wParam;

				switch (kMsg)
				{
					case WinAPI.WM_LBUTTONDOWN:
					case WinAPI.WM_RBUTTONDOWN:
					case WinAPI.WM_MBUTTONDOWN:
					case WinAPI.WM_XBUTTONDOWN:
						mouseClickEvent(EventToButton(kMsg, WinAPI.HIWORD(kInfo.mouseData)), true, kInfo.pt.X, kInfo.pt.Y);
						break;

					case WinAPI.WM_LBUTTONUP:
					case WinAPI.WM_RBUTTONUP:
					case WinAPI.WM_MBUTTONUP:
					case WinAPI.WM_XBUTTONUP:
						mouseClickEvent(EventToButton(kMsg, WinAPI.HIWORD(kInfo.mouseData)), false, kInfo.pt.X, kInfo.pt.Y);
						break;

					case WinAPI.WM_MOUSEWHEEL:
						mouseWheelMoveEvent(MouseAxis.Vertical, WinAPI.HIWORD(kInfo.mouseData), kInfo.pt.X, kInfo.pt.Y);
						break;

					case WinAPI.WM_MOUSEHWHEEL:
						mouseWheelMoveEvent(MouseAxis.Horizontal, WinAPI.HIWORD(kInfo.mouseData), kInfo.pt.X, kInfo.pt.Y);
						break;

					case WinAPI.WM_MOUSEMOVE:
						mouseMoveEvent(kInfo.pt.X, kInfo.pt.Y);
						break;

					//default:
					//	Debug.WriteLine("{0}: ({1}, {2}) ({3}, {4}) {5} {6}",
					//		kMsg, kInfo.pt.X, kInfo.pt.Y, WinAPI.HIWORD(kInfo.mouseData),
					//		WinAPI.LOWORD(kInfo.mouseData),	kInfo.flags, kInfo.dwExtraInfo);
					//	break;
				}
			}

			return WinAPI.CallNextHookEx(hHook, code, wParam, lParam);
		}
	}
}
