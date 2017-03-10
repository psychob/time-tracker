﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using timetracker.Messages;
using timetracker.WinAPI.WMI;

namespace timetracker.Tracking.SystemMemory
{
	class MemoryTracker : ITracker
	{
		private readonly string[] Ext = new string[]
		{
			Current.Messages.MemoryInfo
		};

		private IDatabase Db;
		private ManagementEventWatcher Tracker;

		public string[] GetExtensions()
		{
			return Ext;
		}

		public string GetName()
		{
			return "SystemMemory";
		}

		public void SetDatabase(IDatabase db)
		{
			Db = db;
		}

		public bool SetUp()
		{
			Tracker = Win32_OperatingSystem.Watch(30,
				BaseClass.WatchType.Modification, ModEvent);

			return true;
		}

		public void TearDown()
		{
			Tracker.Dispose();
		}

		private void ModEvent(Win32_OperatingSystem obj)
		{
			Db.AppendMessage(new MemoryEvent(obj.FreePhysicalMemory.GetValueOrDefault(0),
				obj.FreeVirtualMemory.GetValueOrDefault(0),
				obj.TotalVisibleMemorySize.GetValueOrDefault(0),
				obj.TotalVirtualMemorySize.GetValueOrDefault(0)));
		}
	}
}
