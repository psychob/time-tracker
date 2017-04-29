using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class BaseClass
	{
		public enum WatchType
		{
			Creation,
			Modification,
			Deletion
		}

		protected static List<T> FetchImpl<T>(string NS, string CN,
			Func<ManagementBaseObject, T> factory)
		{
			List<T> ret = new List<T>();

			using (ManagementObjectSearcher mos =
				new ManagementObjectSearcher(NS, "SELECT * FROM " + CN))
			{
				foreach (ManagementObject mo in mos.Get())
					ret.Add(factory(mo));
			}

			return ret;
		}

		protected static ManagementEventWatcher WatchImpl<T>(int Interval, string NS,
			string CN, WatchType Type, Action<T> onEvent,
			Func<ManagementBaseObject, T> factory)
		{
			var ret = new ManagementEventWatcher(NS, WatchQueryCreate(Interval, Type, CN));

			ret.EventArrived += (x, e) => onEvent(factory(e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject));

			ret.Start();

			return ret;
		}

		private static string WatchQueryCreate(int Interval, WatchType Type,
			string ClassName)
		{
			string from = "";
			switch (Type)
			{
				case WatchType.Creation:
					from = "__InstanceCreationEvent";
					break;

				case WatchType.Deletion:
					from = "__InstanceDeletionEvent";
					break;

				case WatchType.Modification:
					from = "__InstanceModificationEvent";
					break;
			}

			return string.Format("SELECT * FROM {0} WITHIN {1} WHERE TargetInstance ISA '{2}'",
				from, Interval, ClassName);
		}

		protected string GetValueString(ManagementBaseObject mbo, string name)
		{
			var logger = LogManager.GetLogger(this.GetType());

			try
			{
				return (string)mbo.Properties[name].Value;
			} catch (ManagementException me)
			{
				logger.ErrorFormat("Exception thrown while trying to access property: {0}", name);
				logger.Error(me);
			}

			return null;
		}

		protected string[] GetValueArrayString(ManagementBaseObject mbo, string name)
		{
			return null;
		}

		protected DateTime? GetValueDateTime(ManagementBaseObject mbo,
			string name)
		{
			var logger = LogManager.GetLogger(this.GetType());

			try
			{
				string val = (string)mbo.Properties[name].Value;

				if (val == null)
					return null;

				try
				{
					return ManagementDateTimeConverter.ToDateTime(val);
				} catch (Exception ex)
				{
					logger.ErrorFormat("Exception thrown while trying to convert date in property: {0}", name);
					logger.Error(ex);
					return null;
				}
			} catch (ManagementException me)
			{
				logger.ErrorFormat("Exception thrown while trying to access property: {0}", name);
				logger.Error(me);
				return null;
			}
		}

		protected T? GetValue<T>(ManagementBaseObject mbo, string name) where T:struct
		{
			var logger = LogManager.GetLogger(this.GetType());

			try
			{
				return (T?)mbo.Properties[name].Value;
			} catch (ManagementException me)
			{
				logger.ErrorFormat("Exception thrown while trying to access property: {0}", name);
				logger.Error(me);
			}

			return null;
		}

		protected U? GetValueEnum<T, U>(ManagementBaseObject mbo,
			string name, Func<T, U> conv) where U : struct
		{
			var logger = LogManager.GetLogger(this.GetType());

			try
			{
				if (mbo.Properties[name].Value != null)
				{
					T val = (T)mbo.Properties[name].Value;
					return conv(val);
				}
			} catch (ManagementException me)
			{
				logger.ErrorFormat("Exception thrown while trying to access property: {0}", name);
				logger.Error(me);
			}

			return null;
		}
	}
}
