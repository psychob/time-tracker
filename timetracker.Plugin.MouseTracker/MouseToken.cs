using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.BasePlugin;

namespace timetracker.Plugin.MouseTracker
{
    public abstract class MouseToken : AbstractToken
    {
        protected int X;
        protected int Y;

        public MouseToken(string type, int x, int y)
            : base(type)
        {
            X = x;
            Y = y;
        }

        protected override byte[] ToBinary()
        {
            byte[] ret = new byte[8];
            byte[] buff;

            buff = BitConverter.GetBytes(X);
            buff.CopyTo(ret, 0);

            buff = BitConverter.GetBytes(Y);
            buff.CopyTo(ret, 4);

            return ret;
        }
    }
}
