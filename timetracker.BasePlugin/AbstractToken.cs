using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.BasePlugin
{
    public abstract class AbstractToken : IToken
    {
        public abstract string GetInnerType();

        protected abstract byte[] ToBinary();

        public byte[] ToBinaryStream()
        {
            byte[] inner = this.ToBinary();

            byte[] ret = new byte[inner.Length + 3];
            byte[] type = GetInnerType().GetBytes();

            type.CopyTo(ret, 0);
            inner.CopyTo(ret, 3);

            return ret;
        }
    }
}
