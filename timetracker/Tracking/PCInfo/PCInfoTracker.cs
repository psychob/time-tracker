using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;

namespace timetracker.Tracking.PCInfo
{
	class PCInfoTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.PCInfo,
		};

		private IDatabase Db;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "PCInfo";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			Db.AppendMessage(new PCInfoEvent(Environment.MachineName, Environment.UserName, Environment.OSVersion.Platform.ToString(), Environment.OSVersion.VersionString));
		}

		public void TearDown()
		{
		}
	}
}
