using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using timetracker.BasePlugin;
using System.Management;
using timetracker.BasePlugin.Messages;

namespace timetracker.Plugin.Memory
{
    public class MemorySpy : ISpy
    {
        const string NameSpace = @"\\.\root\CIMV2";
        const string OperatingSystem = @"SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_OperatingSystem'";

        IValueStorage Values;
        IBinaryStream Stream;
        ILog Logger;
        IConfigurationStorage Config;
        private ManagementEventWatcher EventOS;

        public void Deinitialize()
        {
        }

        public void Initialize()
        {
        }

        public string[] RegisterTokens()
        {
            return new string[]
            {
                CurrentMessages.MessageHeader_Memory,
            };
        }

        public void SetObjects(IValueStorage storage, 
            IBinaryStream stream, ILog logger, 
            IConfigurationStorage conf)
        {
            Values = storage;
            Stream = stream;
            Logger = logger;
            Config = conf;
        }

        public void Start()
        {
            EventOS = new ManagementEventWatcher(NameSpace, OperatingSystem);
            EventOS.EventArrived += OnOSEvent;
            EventOS.Start();
        }

        public void Stop()
        {
            EventOS.Stop();
            EventOS.Dispose();
        }

        private void OnOSEvent(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

            ulong FreePhysicialMemory = (ulong)(UInt64)mbo.Properties["FreePhysicalMemory"].Value;
            ulong TotalVisibleMemorySize = (ulong)(UInt64)mbo.Properties["TotalVisibleMemorySize"].Value;
            ulong FreeVirtualMemory = (ulong)(UInt64)mbo.Properties["FreeVirtualMemory"].Value;
            ulong TotalVirtualMemorySize = (ulong)(UInt64)mbo.Properties["TotalVirtualMemorySize"].Value;

            Stream.Send(new MemoryToken(FreePhysicialMemory, TotalVisibleMemorySize, FreeVirtualMemory, TotalVirtualMemorySize));
        }
    }
}
