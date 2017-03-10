using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using timetracker.WinAPI.WMI;

namespace timetracker.Tracking.ProcessorLoad
{
	class ProcessorLoadTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.ProcessorLoad
		};

		private IDatabase Db;
		private ManagementEventWatcher Tracker;
		private ILog Logger = LogManager.GetLogger(typeof(ProcessorLoadTracker));

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "Processor load";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public void SetUp()
		{
			Tracker = Win32_PerfFormattedData_PerfOS_Processor.Watch(5,
				BaseClass.WatchType.Modification, onMod);
		}

		public void TearDown()
		{
			Tracker.Dispose();
		}

		private void onMod(Win32_PerfFormattedData_PerfOS_Processor obj)
		{
			Logger.Debug(obj);

			Db.AppendMessage(new ProcessorLoadEvent(obj.Name,
				(int)obj.PercentIdleTime.GetValueOrDefault(0),
				(int)obj.PercentPrivilegedTime.GetValueOrDefault(0),
				(int)obj.PercentProcessorTime.GetValueOrDefault(0)));

			Db.SetProperty("Processor.Idle", obj.PercentIdleTime.GetValueOrDefault(0));
			Db.SetProperty("Processor.Kernel", obj.PercentPrivilegedTime.GetValueOrDefault(0));
			Db.SetProperty("Processor.Work", obj.PercentProcessorTime.GetValueOrDefault(0));
		}
	}
}
