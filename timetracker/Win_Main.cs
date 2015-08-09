using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
 public partial class Win_Main : Form
 {
  private NotifyIcon niMainNotifyIcon;
  private Tracker global_tracker;
  private Tracked_Apps tracked_apps;
  private string current_version;

  public Win_Main()
  {
   InitializeComponent();

   {
    Assembly ass = Assembly.GetExecutingAssembly();
    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ass.Location);
    current_version = fvi.ProductVersion.Trim('.', '0');
   }

   niMainNotifyIcon = new NotifyIcon();
   niMainNotifyIcon.Visible = true;
   niMainNotifyIcon.Icon = this.Icon;
   niMainNotifyIcon.Text = "Time Tracker v" + current_version;
   niMainNotifyIcon.DoubleClick += niMainNotifyIcon_DoubleClick;

   tracked_apps = new Tracked_Apps();
   tracked_apps.load_configs("app_def.xml", "app_track.xml");
   tracked_apps.update_from_old_version();
   global_tracker = new Tracker();
   tracked_apps.set_tracker(global_tracker);
  }

  void niMainNotifyIcon_DoubleClick(object sender, EventArgs e)
  {
   if ( this.Visible )
   {
    allow_visible = false;
    Hide();
   } else
   {
    allow_visible = true;
    Show();
    WindowState = FormWindowState.Normal;
    Activate();
   }
  }

  private bool allow_visible = false;
  private bool allow_close = false;

  protected override void SetVisibleCore(bool value)
  {
   if (!allow_visible)
   {
    value = false;
    if (!this.IsHandleCreated) CreateHandle();
   }

   base.SetVisibleCore(value);
  }

  protected override void OnFormClosing(FormClosingEventArgs e)
  {
   if (!allow_close)
   {
    this.Hide();
    e.Cancel = true;
   } else
   {
    global_tracker.Dispose();
    tracked_apps.Dispose();
    niMainNotifyIcon.Dispose();
   }

   base.OnFormClosing(e);
  }

  protected override void WndProc( ref System.Windows.Forms.Message msg )
  {
   switch ( msg.Msg )
   {
    case WinAPI.WM_QUERYENDSESSION:
     msg.Result = (IntPtr)1;
     break;

    case WinAPI.WM_ENDSESSION:
     allow_close = true;
     Close();
     msg.Result = (IntPtr)1;
     break;

    default:
     base.WndProc(ref msg);
     break;
   }
  }

  private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
  {
   allow_close = true;
   Close();
  }

  private void trackedToolStripMenuItem_Click(object sender, EventArgs e)
  {
   Win_Tracked wt = new Win_Tracked();

   wt.tracked_apps = tracked_apps.get_tracked_apps();

   wt.ShowDialog();
  }

  private void showToolStripMenuItem_Click(object sender, EventArgs e)
  {
   Win_AppDb wad = new Win_AppDb();

   wad.all_aps = tracked_apps.get_defined_apps();

   wad.ShowDialog();

   tracked_apps.set_defined_apps(wad.all_aps);
   global_tracker.report_all();
  }

  private void timer_fetch_current_tasks_Tick(object sender, EventArgs e)
  {
   fetch_current_tasks();
  }

  private void fetch_current_tasks()
  {
   List<application_current_tracked_detailed> act = tracked_apps.get_current_apps();

   act = act.OrderByDescending(o => WinAPI.GetTickCount64() - o.start_time).ToList();

   lvTrackedApps.Items.Clear();

   foreach (var it in act)
   {
    ListViewItem lvi = new ListViewItem(it.pid.ToString());

    lvi.SubItems.Add(it.name);
    lvi.SubItems.Add(Utils.calculateTime(WinAPI.GetTickCount64() - it.start_time));
    lvi.SubItems.Add(Utils.calculateTime(it.all_time + (WinAPI.GetTickCount64() - it.start_time)));

    lvTrackedApps.Items.Add(lvi);
   }
  }

  private void Win_Main_Load(object sender, EventArgs e)
  {
   timer_fetch_current_tasks.Enabled = true;
   fetch_current_tasks();
   Text = "Time Tracker v" + current_version;
  }

  private void Win_Main_Resize(object sender, EventArgs e)
  {
   if ( this.WindowState == FormWindowState.Minimized )
   {
    WindowState = FormWindowState.Normal;
    allow_visible = false;
    Hide();
   }
  }
 }
}
