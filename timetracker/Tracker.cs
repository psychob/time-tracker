using System;
using System.Management;

namespace timetracker
{
 class Tracker : IDisposable
 {
  public delegate void post_event_callback( int pid, bool create );

  void empty_callback (int fuck, bool off)
  { }

  public Tracker( )
  {
   callback = empty_callback;
   begin_tracking();
  }

  public void Dispose()
  {
   finish_tracking();
  }

  ManagementEventWatcher mewCreatingProcess;
  ManagementEventWatcher mewDestroyingProcess;

  private void begin_tracking()
  {
   const string name_space    = @"\\.\root\CIMV2";
   const string query_create  = @"SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
   const string query_destroy = @"SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";

   mewCreatingProcess = new ManagementEventWatcher(name_space, query_create);
   mewCreatingProcess.EventArrived += mewCreatingProcess_EventArrived;

   mewDestroyingProcess = new ManagementEventWatcher(name_space, query_destroy);
   mewDestroyingProcess.EventArrived += mewDestroyingProcess_EventArrived;

   mewCreatingProcess.Start();
   mewDestroyingProcess.Start();
  }

  private void finish_tracking( )
  {
   mewCreatingProcess.Stop();
   mewDestroyingProcess.Stop();

   mewCreatingProcess.Dispose();
   mewDestroyingProcess.Dispose();
  }

  post_event_callback callback;

  void mewDestroyingProcess_EventArrived(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

   callback(pid, false);
  }

  void mewCreatingProcess_EventArrived(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

   callback(pid, true);
  }

  public void set_callback(post_event_callback action)
  {
   callback = action;
  }
 }
}
