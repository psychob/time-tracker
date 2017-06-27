using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;
using timetracker.WinAPI;

namespace timetracker.Plugin.ForegroundChange
{
    public class ForegroundChangeSpy : ISpy
    {
        IValueStorage Values;
        IBinaryStream Stream;
        ILog Logger;
        IConfigurationStorage Config;
        IntPtr hHook;
        User32.SetWinEventHookProc foreDelegate;

        public void Deinitialize()
        {
        }

        public void Initialize()
        {
        }

        public string[] RegisterTokens()
        {
            return new string[]
            {
                CurrentMessages.MessageHeader_ForegroundChange,
            };
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
            foreDelegate = new User32.SetWinEventHookProc(ForegroundChange);

            hHook = User32.SetWinEventHook(User32.SetWinEventHookType.EVENT_SYSTEM_FOREGROUND,
                User32.SetWinEventHookType.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero,
                foreDelegate, 0, 0, User32.SetWinEventHookFlags.WINEVENT_OUTOFCONTEXT);
        }

        public void Stop()
        {
            User32.UnhookWinEvent(hHook);

            hHook = IntPtr.Zero;
        }

        private void ForegroundChange(IntPtr hWinEventHook,
            uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            uint threadID = User32.GetWindowThreadProcessId(hwnd,
                out uint ProcessId);

            Stream.Send(new ForegroundWindowChanged(threadID, ProcessId));
        }
    }
}
