using System;
using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.MouseTracker
{
    class MouseVerticalToken : MouseToken
    {
        private short Value;

        public MouseVerticalToken(short value, int x, int y)
            : base(CurrentMessages.MessageHeader_MouseVerticalWheel, x, y)
        {
            Value = value;
        }

        protected override byte[] ToBinary()
        {
            byte[] old = base.ToBinary();
            byte[] ret = new byte[old.Length + 4];
            byte[] buff;

            buff = BitConverter.GetBytes((int)Value);
            buff.CopyTo(ret, 0);

            old.CopyTo(ret, 4);

            return ret;
        }
    }
}