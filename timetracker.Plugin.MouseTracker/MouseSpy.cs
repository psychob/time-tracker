using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using timetracker.BasePlugin;
using timetracker.WinAPI;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace timetracker.Plugin.MouseTracker
{
    public class MouseSpy : ISpy
    {
        IValueStorage Values;
        IBinaryStream Stream;
        ILog Logger;
        IConfigurationStorage Config;
        IntPtr mouseHook = IntPtr.Zero;
        User32.SetWindowsHookProc mouseDelegate = null;

        public void Initialize()
        {
            Logger.Debug("Initializing MouseSpy");
        }

        public void Deinitialize()
        {
            Logger.Debug("Deinitializing MouseSpy");
        }

        public string[] RegisterTokens()
        {
            throw new NotImplementedException();
        }

        public void SetObjects(IValueStorage storage, 
            IBinaryStream stream, ILog logger, 
            IConfigurationStorage conf)
        {
            Values = storage;
            Stream = stream;
            Logger = logger;
            Config = conf;
        }

        public void Start()
        {
            Logger.Debug("Starting MouseSpy");
            Logger.Debug("Initializing delegate");

            mouseDelegate = new User32.SetWindowsHookProc(MouseEventReciever);

            Logger.Debug("Initializing Hook");
            mouseHook = User32.SetWindowsHookEx(User32.SetWindowsHookType.WH_MOUSE_LL,
                mouseDelegate, IntPtr.Zero, 0);

            if (mouseHook == IntPtr.Zero)
            {
                int lastError = Marshal.GetLastWin32Error();

                Logger.ErrorFormat("Hook initialization failed! ErrorCode = {0}", lastError);
                throw new Win32Exception(lastError, "Can't register mouse hook");
            }
        }

        public void Stop()
        {
            Logger.Debug("Stopping MouseSpy");

            if (mouseHook != IntPtr.Zero)
            {
                Logger.Debug("Unregistering hook");
                if (!User32.UnhookWindowsHookEx(mouseHook))
                {
                    int lastError = Marshal.GetLastWin32Error();

                    Logger.ErrorFormat("UnHooking failed! ErrorCode = {0}", lastError);
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Can't unregister mouse hook");
                }

                mouseHook = IntPtr.Zero;
                mouseDelegate = null;
            }
            else
            {
                Logger.Error("KeyboardSpy wasn't started!");
            }
        }

        private MouseButton EventToButton(WinUser.WindowMessage mMsg, short edata)
        {
            switch (mMsg)
            {
                case WinUser.WindowMessage.WM_LBUTTONUP:
                case WinUser.WindowMessage.WM_LBUTTONDOWN:
                    return MouseButton.Left;

                case WinUser.WindowMessage.WM_RBUTTONUP:
                case WinUser.WindowMessage.WM_RBUTTONDOWN:
                    return MouseButton.Right;

                case WinUser.WindowMessage.WM_MBUTTONDOWN:
                case WinUser.WindowMessage.WM_MBUTTONUP:
                    return MouseButton.Middle;

                case WinUser.WindowMessage.WM_XBUTTONDOWN:
                case WinUser.WindowMessage.WM_XBUTTONUP:
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

        private int MouseEventReciever(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var kInfo = (User32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32.MSLLHOOKSTRUCT));
                var kMsg = (WinUser.WindowMessage)wParam;

                switch (kMsg)
                {
                    case WinUser.WindowMessage.WM_LBUTTONDOWN:
                    case WinUser.WindowMessage.WM_RBUTTONDOWN:
                    case WinUser.WindowMessage.WM_MBUTTONDOWN:
                    case WinUser.WindowMessage.WM_XBUTTONDOWN:
                        Stream.Send(new MousePressToken(EventToButton(kMsg, WinDef.HIWORD(kInfo.mouseData)),
                            kInfo.pt.X, kInfo.pt.Y));
                        break;

                    case WinUser.WindowMessage.WM_LBUTTONUP:
                    case WinUser.WindowMessage.WM_RBUTTONUP:
                    case WinUser.WindowMessage.WM_MBUTTONUP:
                    case WinUser.WindowMessage.WM_XBUTTONUP:
                        Stream.Send(new MouseUnpressToken(EventToButton(kMsg, WinDef.HIWORD(kInfo.mouseData)),
                            kInfo.pt.X, kInfo.pt.Y));
                        break;

                    case WinUser.WindowMessage.WM_MOUSEWHEEL:
                        Stream.Send(new MouseVerticalToken(WinDef.HIWORD(kInfo.mouseData),
                            kInfo.pt.X, kInfo.pt.Y));
                        break;

                    case WinUser.WindowMessage.WM_MOUSEHWHEEL:
                        Stream.Send(new MouseHorizontalToken(WinDef.HIWORD(kInfo.mouseData),
                            kInfo.pt.X, kInfo.pt.Y));
                        break;

                    case WinUser.WindowMessage.WM_MOUSEMOVE:
                        Stream.Send(new MouseMoveToken(kInfo.pt.X, kInfo.pt.Y));
                        break;
                }
            }

            return User32.CallNextHookEx(mouseHook, nCode, wParam, lParam);
        }
    }
}
