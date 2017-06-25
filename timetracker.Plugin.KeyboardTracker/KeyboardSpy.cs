using System;
using log4net;
using timetracker.BasePlugin;
using timetracker.WinAPI;
using System.ComponentModel;
using System.Runtime.InteropServices;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.KeyboardTracker
{
	public class KeyboardSpy : ISpy
	{
		IValueStorage Values;
		IBinaryStream Stream;
		ILog Logger;
		IConfigurationStorage Config;
        bool[] KeyState = new bool[255];
        IntPtr keyHook = IntPtr.Zero;
        User32.SetWindowsHookProc keyDelegate = null;

        public void Initialize()
		{
            Logger.Debug("Initializing KeyboardSpy");

            KeyState.Fill(false);
        }

        public void Deinitialize()
        {
            Logger.Debug("Deinitializing KeyboardSpy");
        }

        public void Start()
        {
            Logger.Debug("Starting KeyboardSpy");
            Logger.Debug("Initializing delegate");

            keyDelegate = new User32.SetWindowsHookProc(KeyReciver);
            
            Logger.Debug("Initializing Hook");
            keyHook = User32.SetWindowsHookEx(User32.SetWindowsHookType.WH_KEYBOARD_LL,
                keyDelegate, IntPtr.Zero, 0);

            if (keyHook == IntPtr.Zero)
            {
                int lastError = Marshal.GetLastWin32Error();

                Logger.ErrorFormat("Hook initialization failed! ErrorCode = {0}", lastError);
                throw new Win32Exception(lastError, "Can't register keyboard hook");
            }
        }

        public void Stop()
        {
            Logger.Debug("Stopping KeyboardSpy");

            if (keyHook != IntPtr.Zero)
            {
                Logger.Debug("Unregistering hook");
                if (!User32.UnhookWindowsHookEx(keyHook))
                {
                    int lastError = Marshal.GetLastWin32Error();

                    Logger.ErrorFormat("UnHooking failed! ErrorCode = {0}", lastError);
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Can't unregister keyboard hook");
                }

                keyHook = IntPtr.Zero;
                keyDelegate = null;
            } else
            {
                Logger.Error("KeyboardSpy wasn't started!");
            }
        }

        public string[] RegisterTokens()
		{
            return new string[]
            {
                CurrentMessages.MessageHeader_KeyPressed,
                CurrentMessages.MessageHeader_KeyUnpressed,
            };
		}

		public void SetObjects(IValueStorage storage, IBinaryStream stream,
			ILog logger, IConfigurationStorage conf)
		{
			Values = storage;
			Stream = stream;
			Logger = logger;
			Config = conf;
		}

        private int KeyReciver(int nCode, IntPtr wParam, IntPtr lParam)
        {
            Logger.DebugFormat("KeyRecived: nCode = {0} wParam = {1} lParam = {2}",
                nCode, wParam, lParam);

            if (nCode >= 0)
            {
                var kInfo = (User32.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32.KBDLLHOOKSTRUCT));
                var kMsg = (WinUser.WindowMessage)wParam;

                switch (kMsg)
                {
                    case WinUser.WindowMessage.WM_KEYDOWN:
                    case WinUser.WindowMessage.WM_SYSKEYDOWN:
                        if (KeyState[kInfo.vkCode] == false)
                        {
                            Logger.DebugFormat("KeyPressed v={0} s={1}",
                                kInfo.vkCode, kInfo.scanCode);

                            Stream.Send(new KeyPressedToken(kInfo.vkCode, kInfo.scanCode));
                            KeyState[kInfo.vkCode] = true;
                        }
                        break;

                    case WinUser.WindowMessage.WM_KEYUP:
                    case WinUser.WindowMessage.WM_SYSKEYUP:
                        if (KeyState[kInfo.vkCode] == true)
                        {
                            Logger.DebugFormat("KeyUnpressed v={0} s={1}",
                                kInfo.vkCode, kInfo.scanCode);

                            Stream.Send(new KeyUnpressedToken(kInfo.vkCode, kInfo.scanCode));
                            KeyState[kInfo.vkCode] = false;
                        }
                        break;
                }
            }

            return User32.CallNextHookEx(keyHook, nCode, wParam, lParam);
        }
	}
}
