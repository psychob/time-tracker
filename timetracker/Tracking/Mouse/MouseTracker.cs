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
using static timetracker.WinAPI.WinDef;
using static timetracker.WinAPI.WinUser;

namespace timetracker.Tracking.Mouse
{
	class MouseTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.MousePressLeft,
			Current.Messages.MousePressMiddle,
			Current.Messages.MousePressRight,
			Current.Messages.MousePressXn,
			Current.Messages.MouseReleaseLeft,
			Current.Messages.MouseReleaseMiddle,
			Current.Messages.MouseReleaseRight,
			Current.Messages.MouseReleaseXn,
			Current.Messages.MouseWheelHorizontal,
			Current.Messages.MouseWheelVertical,
			Current.Messages.MouseMove,
		};

		private IDatabase Db;
		private ILog Logger = LogManager.GetLogger(typeof(MouseTracker));

		private SetWindowsHookProc mouseDelegate;
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
			mouseDelegate = new SetWindowsHookProc(ProcessMouseHookEvent);
			hHook = SetWindowsHookEx(SetWindowsHookType.WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);
		}

		public void TearDown()
		{
			if (hHook != IntPtr.Zero)
			{
				UnhookWindowsHookEx(hHook);
				hHook = IntPtr.Zero;
			}
		}

		private int ProcessMouseHookEvent(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				var kInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
				var kMsg = (WindowMessage)wParam;

				string type = string.Empty;
				int x = kInfo.pt.X, y = kInfo.pt.Y;
				int value = 0;
				int mouseBtn = 0;

				switch (kMsg)
				{
					case WindowMessage.WM_LBUTTONDOWN:
						type = Current.Messages.MousePressLeft;
						break;

					case WindowMessage.WM_RBUTTONDOWN:
						type = Current.Messages.MousePressRight;
						break;

					case WindowMessage.WM_MBUTTONDOWN:
						type = Current.Messages.MousePressMiddle;
						break;

					case WindowMessage.WM_XBUTTONDOWN:
						type = Current.Messages.MousePressXn;
						mouseBtn = HIWORD(kInfo.mouseData);
						break;

					case WindowMessage.WM_LBUTTONUP:
						type = Current.Messages.MouseReleaseLeft;
						break;

					case WindowMessage.WM_RBUTTONUP:
						type = Current.Messages.MouseReleaseRight;
						break;

					case WindowMessage.WM_MBUTTONUP:
						type = Current.Messages.MouseReleaseMiddle;
						break;

					case WindowMessage.WM_XBUTTONUP:
						type = Current.Messages.MouseReleaseXn;
						mouseBtn = HIWORD(kInfo.mouseData);
						break;

					case WindowMessage.WM_MOUSEWHEEL:
						type = Current.Messages.MouseWheelVertical;
						value = HIWORD(kInfo.mouseData);
						break;

					case WindowMessage.WM_MOUSEHWHEEL:
						type = Current.Messages.MouseWheelHorizontal;
						value = HIWORD(kInfo.mouseData);
						break;

					case WindowMessage.WM_MOUSEMOVE:
						type = Current.Messages.MouseMove;
						break;
				}

				if (type == Current.Messages.MouseWheelHorizontal ||
					type == Current.Messages.MouseWheelVertical)
				{
					Db.AppendMessage(new MouseWheelEvent(type, x, y, value, mouseBtn));
				} else if (type != string.Empty)
				{
					Db.AppendMessage(new MouseEvent(type, x, y, mouseBtn));
				}
			}

			return CallNextHookEx(hHook, nCode, wParam, lParam);
		}
	}
}
