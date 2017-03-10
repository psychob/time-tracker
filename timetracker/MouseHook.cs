using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static timetracker.WinAPI.User32;
using static timetracker.WinAPI.WinDef;
using static timetracker.WinAPI.WinUser;

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

		private SetWindowsHookProc mouseDelegate;
		private IntPtr hHook = IntPtr.Zero;

		public bool Init()
		{
			mouseDelegate = new SetWindowsHookProc(ProcessMouseHookEvent);
			hHook = SetWindowsHookEx(SetWindowsHookType.WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);

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

		private MouseButton EventToButton(WindowMessage mMsg, int edata)
		{
			switch (mMsg)
			{
				case WindowMessage.WM_LBUTTONUP:
				case WindowMessage.WM_LBUTTONDOWN:
					return MouseButton.Left;

				case WindowMessage.WM_RBUTTONUP:
				case WindowMessage.WM_RBUTTONDOWN:
					return MouseButton.Right;

				case WindowMessage.WM_MBUTTONDOWN:
				case WindowMessage.WM_MBUTTONUP:
					return MouseButton.Middle;

				case WindowMessage.WM_XBUTTONDOWN:
				case WindowMessage.WM_XBUTTONUP:
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
				var kInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
				var kMsg = (WindowMessage)wParam;

				switch (kMsg)
				{
					case WindowMessage.WM_LBUTTONDOWN:
					case WindowMessage.WM_RBUTTONDOWN:
					case WindowMessage.WM_MBUTTONDOWN:
					case WindowMessage.WM_XBUTTONDOWN:
						mouseClickEvent(EventToButton(kMsg, HIWORD(kInfo.mouseData)), true, kInfo.pt.X, kInfo.pt.Y);
						break;

					case WindowMessage.WM_LBUTTONUP:
					case WindowMessage.WM_RBUTTONUP:
					case WindowMessage.WM_MBUTTONUP:
					case WindowMessage.WM_XBUTTONUP:
						mouseClickEvent(EventToButton(kMsg, HIWORD(kInfo.mouseData)), false, kInfo.pt.X, kInfo.pt.Y);
						break;

					case WindowMessage.WM_MOUSEWHEEL:
						mouseWheelMoveEvent(MouseAxis.Vertical, HIWORD(kInfo.mouseData), kInfo.pt.X, kInfo.pt.Y);
						break;

					case WindowMessage.WM_MOUSEHWHEEL:
						mouseWheelMoveEvent(MouseAxis.Horizontal, HIWORD(kInfo.mouseData), kInfo.pt.X, kInfo.pt.Y);
						break;

					case WindowMessage.WM_MOUSEMOVE:
						mouseMoveEvent(kInfo.pt.X, kInfo.pt.Y);
						break;
				}
			}

			return CallNextHookEx(hHook, code, wParam, lParam);
		}
	}
}
