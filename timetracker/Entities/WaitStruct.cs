using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Entities
{
	public struct WaitStruct
	{
		public int PID;
		public ulong StartTime;
		public int Count;
		public int ParentID;
	}
}
