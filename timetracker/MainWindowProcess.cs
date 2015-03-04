using System;
using System.Collections.Generic;
using System.Management;

namespace timetracker
{
 public partial class MainWindow
 {
  ManagementEventWatcher ewStartProcess = null,
                         ewCloseProcess = null;

  private void EnableHook( )
  {
   ewStartProcess = new ManagementEventWatcher();
   ewStartProcess.Scope = new ManagementScope(@"\\.\root\CIMV2");
   ewStartProcess.Query = new EventQuery(@"select * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_Process'");
   ewStartProcess.EventArrived += ewStartProcess_EventArrived;

   ewCloseProcess = new ManagementEventWatcher();
   ewCloseProcess.Scope = new ManagementScope(@"\\.\root\CIMV2");
   ewCloseProcess.Query = new EventQuery(@"select * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_Process'");
   ewCloseProcess.EventArrived += ewCloseProcess_EventArrived;

   ewStartProcess.Start();
   ewCloseProcess.Start();
  }

  private void DisableHook( )
  {
   ewStartProcess.Stop();
   ewCloseProcess.Stop();

   ewStartProcess.Dispose();
   ewCloseProcess.Dispose();

   ewStartProcess = ewCloseProcess = null;
  }

  void ewStartProcess_EventArrived(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   Console.Out.WriteLine("Start: " + mbo.Properties["Name"].Value.ToString());

   uint procid = (uint)(UInt32)mbo.Properties["ProcessId"].Value;
   string name, commandline;

   name = mbo.Properties["Name"].Value != null ? mbo.Properties["Name"].Value.ToString() : "";
   commandline = mbo.Properties["CommandLine"].Value != null ? mbo.Properties["CommandLine"].Value.ToString() : "";

   if (name == "")
    return;

   registerTrackApp(procid, name, commandline);
  }

  void ewCloseProcess_EventArrived(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   uint procid = (UInt32)mbo.Properties["ProcessId"].Value;
   Console.Out.WriteLine("End: " + mbo.Properties["Name"].Value.ToString());

   unregisterTrackApp(procid);
  }

  private void AddAllRunningProcess()
  {
   ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Process");

   foreach (ManagementObject it in mos.Get())
   {
    if (it["ProcessId"] != null && it["Name"] != null && it["CommandLine"] != null && (UInt32)(it["ProcessId"]) != 0 )
    {
     registerTrackApp((UInt32)it["ProcessId"],
                      it["Name"].ToString(),
                      it["CommandLine"].ToString());
    }
   }
  }
 }
}
