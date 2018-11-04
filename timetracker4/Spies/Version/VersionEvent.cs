using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Spies.Version
{
    internal class VersionEvent
    {
        public string Version { get; set; }

        public VersionEvent(string version)
        {
            Version = version;
        }
    }
}
