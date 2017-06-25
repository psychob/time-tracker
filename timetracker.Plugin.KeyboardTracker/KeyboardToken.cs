using System;
using timetracker.BasePlugin;

namespace timetracker.Plugin.KeyboardTracker
{
    public abstract class KeyboardToken : AbstractToken
    {
        protected uint VirtualKey;
        protected uint ScanCode;

        public KeyboardToken(string type, uint virtualKey, uint scanCode)
            : base(type)
        {
            VirtualKey = virtualKey;
            ScanCode = scanCode;
        }

        protected override byte[] ToBinary()
        {
            byte[] ret = new byte[8];
            byte[] buff;

            buff = BitConverter.GetBytes(VirtualKey);
            buff.CopyTo(ret, 0);

            buff = BitConverter.GetBytes(ScanCode);
            buff.CopyTo(ret, 4);

            return ret;
        }
    }
}
