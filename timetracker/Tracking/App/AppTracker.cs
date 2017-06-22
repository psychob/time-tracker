using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using timetracker.WinAPI.WMI;

namespace timetracker.Tracking.App
{
	class AppTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.AppBegin,
			Current.Messages.AppEnd,
			Current.Messages.UnknownAppBegin,
			Current.Messages.UnknownAppEnd,
		};

		private IDatabase Db;
		private ILog Logger = LogManager.GetLogger(typeof(AppTracker));

		private ManagementEventWatcher OnStart, OnEnd;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "App Tracking";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			OnStart = Win32_Process.Watch(5, BaseClass.WatchType.Creation, CreateProcess);
			OnEnd = Win32_Process.Watch(5, BaseClass.WatchType.Deletion, DestroyProcess);
		}

		private void CreateProcess(Win32_Process obj)
		{
			throw new NotImplementedException();
		}

		private void DestroyProcess(Win32_Process obj)
		{
			throw new NotImplementedException();
		}

		public void TearDown()
		{
			OnStart.Stop();
			OnEnd.Stop();

			OnStart.Dispose();
			OnEnd.Dispose();
		}
	}
}
