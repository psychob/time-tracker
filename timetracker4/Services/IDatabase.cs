using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker4.Services
{
    interface IDatabase
    {
        void NewEvent<T>(T obj, DateTime? when = null);
    }
}
