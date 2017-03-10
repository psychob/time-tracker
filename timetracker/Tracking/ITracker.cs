using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Tracking
{
	interface ITracker
	{
		string GetName();

		bool SetUp();

		void TearDown();

		void SetDatabase(IDatabase db);

		string[] GetExtensions();
	}
}
