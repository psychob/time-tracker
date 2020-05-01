using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.WMI
{
    public abstract class BaseClass
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

        protected string GetValueString(ManagementBaseObject mbo, string name,
            Action<bool> wasQueried)
        {
            try
            {
                string val = (string)mbo.Properties[name].Value;
                wasQueried(true);
                return val;
            }
            catch (ManagementException)
            {
                wasQueried(false);
                return null;
            }
        }

        protected UInt32? GetValueUInt32(ManagementBaseObject mbo, string name,
            Action<bool> wasQueried)
        {
            try
            {
                object oVal = mbo.Properties[name].Value;

                wasQueried(true);

                if (oVal == null)
                    return null;

                UInt32 val = (UInt32)oVal;
                wasQueried(true);
                return val;
            }
            catch (ManagementException)
            {
                wasQueried(false);
                return null;
            }
        }

        protected UInt64? GetValueUInt64(ManagementBaseObject mbo, string name,
            Action<bool> wasQueried)
        {
            try
            {
                object oVal = mbo.Properties[name].Value;

                wasQueried(true);

                if (oVal == null)
                    return null;

                UInt64 val = (UInt64)oVal;
                wasQueried(true);
                return val;
            }
            catch (ManagementException)
            {
                wasQueried(false);
                return null;
            }
        }

        protected DateTime? GetValueDateTime(ManagementBaseObject mbo,
            string name, Action<bool> wasQueried)
        {
            try
            {
                string val = (string)mbo.Properties[name].Value;
                wasQueried(true);

                if (val == null)
                    return null;

                try
                {
                    return ManagementDateTimeConverter.ToDateTime(val);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            catch (ManagementException)
            {
                wasQueried(false);
                return null;
            }
        }

        protected U? GetValueEnum<T, U>(ManagementBaseObject mbo,
            string name, Action<bool> wasQueried, Func<T, U> conv) where U : struct
        {
            try
            {
                if (mbo.Properties[name].Value == null)
                {
                    wasQueried(true);
                    return null;
                }
                else
                {
                    T val = (T)mbo.Properties[name].Value;
                    wasQueried(true);
                    return conv(val);
                }
            }
            catch (ManagementException)
            {
                wasQueried(false);
                return null;
            }
        }
    }
}
