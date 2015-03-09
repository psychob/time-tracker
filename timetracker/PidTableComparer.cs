using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker
{
 class PidTableComparer : IEqualityComparer<ApplicationDatabase.PidTable>
 {
  public bool Equals(ApplicationDatabase.PidTable x, ApplicationDatabase.PidTable y)
  {
   return x.pid == y.pid;
  }

  public int GetHashCode(ApplicationDatabase.PidTable obj)
  {
   return obj.pid.GetHashCode();
  }
 }
}
