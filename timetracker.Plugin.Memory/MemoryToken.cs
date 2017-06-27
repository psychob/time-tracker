using System;
using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.Memory
{
    internal class MemoryToken : AbstractToken
    {
        private ulong PFree, PTotal;
        private ulong VFree, VTotal;
        private ulong AFree, ATotal;

        public MemoryToken(ulong freePhysicialMemory, ulong totalVisibleMemorySize, ulong freeVirtualMemory, ulong totalVirtualMemorySize)
            : base(CurrentMessages.MessageHeader_Memory)
        {
            PFree = freePhysicialMemory;
            PTotal = totalVisibleMemorySize;
            VFree = freeVirtualMemory;
            VTotal = totalVirtualMemorySize;

            AFree = PFree + VFree;
            ATotal = PTotal + VTotal;
        }

        protected override byte[] ToBinary()
        {
            byte[] ret = new byte[6 * 8];
            byte[] buff;

            buff = BitConverter.GetBytes(PFree);
            buff.CopyTo(ret, 8 * 0);
            buff = BitConverter.GetBytes(PTotal);
            buff.CopyTo(ret, 8 * 1);
            buff = BitConverter.GetBytes(VFree);
            buff.CopyTo(ret, 8 * 2);
            buff = BitConverter.GetBytes(VTotal);
            buff.CopyTo(ret, 8 * 3);
            buff = BitConverter.GetBytes(AFree);
            buff.CopyTo(ret, 8 * 4);
            buff = BitConverter.GetBytes(ATotal);
            buff.CopyTo(ret, 8 * 5);

            return ret;
        }
    }
}