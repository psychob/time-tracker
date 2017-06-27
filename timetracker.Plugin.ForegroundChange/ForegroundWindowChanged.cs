using System;
using timetracker.BasePlugin;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.ForegroundChange
{
    internal class ForegroundWindowChanged : AbstractToken
    {
        private uint ThreadId;
        private uint ProcessID;

        public ForegroundWindowChanged(uint threadID, uint processId)
            : base(CurrentMessages.MessageHeader_ForegroundChange)
        {
            ThreadId = threadID;
            ProcessID = processId;
        }

        protected override byte[] ToBinary()
        {
            byte[] ret = new byte[8];
            byte[] buff;

            buff = BitConverter.GetBytes(ProcessID);
            buff.CopyTo(ret, 0);

            buff = BitConverter.GetBytes(ThreadId);
            buff.CopyTo(ret, 4);

            return ret;
        }
    }
}