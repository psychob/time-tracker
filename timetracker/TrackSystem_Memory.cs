using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static timetracker.Messages.v3_16.Constants;

namespace timetracker
{
    public partial class TrackSystem
    {
        class MemoryToke : TokenValue
        {
            public byte Type;

            public ulong PFree, PTotal;
            public ulong VFree, VTotal;
            public ulong AFree, ATotal;

            public MemoryToke(ulong pfree, ulong ptotal, ulong vfree, ulong vtotal)
            {
                Type = MessageHeader_Memory;

                PFree = pfree;
                PTotal = ptotal;

                VFree = vfree;
                VTotal = vtotal;

                AFree = pfree + vfree;
                ATotal = ptotal + vtotal;
            }

            public int AsByteStream(ref byte[] str, int start, int length)
            {
                int Written = 0;
                byte[] buff;

                str[start + Written++] = Type;

                buff = BitConverter.GetBytes(PFree);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                buff = BitConverter.GetBytes(PTotal);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                buff = BitConverter.GetBytes(VFree);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                buff = BitConverter.GetBytes(VTotal);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                buff = BitConverter.GetBytes(AFree);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                buff = BitConverter.GetBytes(ATotal);
                buff.CopyTo(str, start + Written);
                Written += buff.Length;

                return Written;
            }
        }

        private void MemoryEvent(ulong free, ulong all, ulong vfree, ulong vall)
        {
            AppendBinary(new MemoryToke(free, all, vfree, vall));
        }
    }
}
