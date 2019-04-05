using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Spies
{
    internal interface ISpy
    {
        string GetName();
        void Start();
        void Stop();

        List<SpyOption> GetOptions();
    }
}
