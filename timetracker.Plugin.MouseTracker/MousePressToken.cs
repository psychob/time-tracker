using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.MouseTracker
{
    class MousePressToken : MouseToken
    {
        protected MouseButton Button;

        public MousePressToken(MouseButton btn, int x, int y)
            : base(CurrentMessages.MessageHeader_MousePressClick, x, y)
        {
            Button = btn;
        }

        protected override byte[] ToBinary()
        {
            byte[] old = base.ToBinary();
            byte[] ret = new byte[old.Length + 4];
            byte[] buff;

            buff = BitConverter.GetBytes((int)Button);
            buff.CopyTo(ret, 0);

            old.CopyTo(ret, 4);

            return ret;
        }
    }
}
