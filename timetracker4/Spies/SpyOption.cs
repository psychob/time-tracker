using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Spies
{
    internal enum SpyType
    {
        Integer, String, Boolean,
    }

    internal enum SpyAttachTo
    {
        Application, Spy,
    }

    internal class SpyOption
    {
        public string Name;
        public object DefaultValue;
        public SpyType Type;
        public SpyAttachTo AttachTo;
    }
}
