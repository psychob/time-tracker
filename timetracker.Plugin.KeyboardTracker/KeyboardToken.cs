using System;
using timetracker.BasePlugin;

namespace timetracker.Plugin.KeyboardTracker
{
    public abstract class KeyboardToken : AbstractToken
    {
        protected uint VirtualKey;
        protected uint ScanCode;

        public KeyboardToken(uint virtualKey, uint scanCode)
        {
            VirtualKey = virtualKey;
            ScanCode = scanCode;
        }

        public abstract override string GetInnerType();

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
