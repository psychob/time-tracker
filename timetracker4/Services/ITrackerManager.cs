using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Services
{
    internal interface ITrackerManager
    {
        bool SetMetric(string name, object value);
        bool SetMetric(string id, string name, object value);

        bool SetEntry(string id, TrackerEntry entry);
        bool RemoveEntry(string id);
    }
}
