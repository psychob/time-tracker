using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Services
{
    internal class TrackerManager: ITrackerManager, IDisposable
    {
        public bool SetMetric(string name, object value)
        {
            throw new NotImplementedException();
        }

        public bool SetMetric(string id, string name, object value)
        {
            throw new NotImplementedException();
        }

        public bool SetEntry(string id, TrackerEntry entry)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntry(string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
