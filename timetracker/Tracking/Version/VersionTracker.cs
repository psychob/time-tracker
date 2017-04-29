using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.Version
{
	class VersionTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.Version,
		};

		private IDatabase Db;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "Time tracker version";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			string CurrentVersion = "";
			{
				var ass = Assembly.GetExecutingAssembly();
				var fvi = FileVersionInfo.GetVersionInfo(ass.Location);
				var str = fvi.ProductVersion.Split('.');
				int it = str.Length - 1;

				while (it >= 0 && str[it] == "0")
					it--;

				for (int kt = 0; kt <= it; ++kt)
					CurrentVersion += str[kt] + '.';

				CurrentVersion = CurrentVersion.Trim('.');
			}

			Db.AppendMessage(new VersionEvent(CurrentVersion));
		}

		public void TearDown()
		{
		}
	}
}
