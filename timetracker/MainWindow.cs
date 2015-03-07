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
 public partial class MainWindow : Form
 {
  ApplicationDatabase appDB;

  public MainWindow()
  {
   InitializeComponent();
  }

  private void MainWindow_Load(object sender, EventArgs e)
  {
   {
    Assembly ass = Assembly.GetExecutingAssembly();
    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ass.Location);
    this.Text = "Time Tracker v" + fvi.ProductVersion;
   }

   appDB = new ApplicationDatabase();

   appDB.StartApp();

   validate_time = (ulong)timer_validate.Interval;
   refresh_time = (ulong)timer_RefreshCurrentApplications.Interval;
  }

  private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
   appDB.CloseApp();
  }

  private void niMainApp_MouseDoubleClick(object sender, MouseEventArgs e)
  {
   if (this.Visible)
    this.Hide();
   else
   {
    this.Show();

    if (WindowState == FormWindowState.Minimized)
     WindowState = FormWindowState.Normal;

    this.Activate();
   }
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e)
  {
   this.Close();
  }

  private void timer_RefreshCurrentApplications_Tick(object sender, EventArgs e)
  {
   List<ApplicationDatabase.DatabaseCurrentView> list = appDB.PollActiveApps();

   list = list.OrderByDescending(o => o.currentTime).ToList();

   lvTrackApp.SuspendLayout();

   lvTrackApp.Items.Clear();

   foreach (ApplicationDatabase.DatabaseCurrentView it in list)
   {
    ListViewItem lvi = new ListViewItem(it.processid.ToString());
    lvi.SubItems.Add(it.name);
    lvi.SubItems.Add(Utils.calculateTime(it.currentTime));
    lvi.SubItems.Add(Utils.calculateTime(it.allTime));

    lvTrackApp.Items.Add(lvi);
   }

   lvTrackApp.ResumeLayout(true);
   refresh_time = (ulong)timer_RefreshCurrentApplications.Interval;
  }

  private void applicationDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
   window.AllApplicationDatabase aad = new window.AllApplicationDatabase();

   aad.listOfAllEntries = appDB.PollAllEntries();

   aad.ShowDialog(this);

   appDB.SynchronizeEntries(aad.listOfAllEntries);
  }

  private void trackedApplicationDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
   window.ShowTracked st = new window.ShowTracked();

   st.listOfTracked = appDB.PollTrackedApps();
   st.ShowDialog(this);
  }

  ulong refresh_time = 1000,
        validate_time = 1000 * 60 * 5;

  private void timer_validate_Tick(object sender, EventArgs e)
  {
   appDB.ValidateRunningProcess();
   validate_time = (ulong)timer_validate.Interval;
  }

  private void validateProcessToolStripMenuItem_Click(object sender, EventArgs e)
  {
   appDB.ValidateRunningProcess();
  }

  private void timer_updateTimers_Tick(object sender, EventArgs e)
  {
   tsslRefreshCount.Text = Utils.calculateTime(refresh_time);
   tsslValidateRefresh.Text = Utils.calculateTime(validate_time);

   refresh_time -= (ulong)timer_updateTimers.Interval;
   validate_time -= (ulong)timer_updateTimers.Interval;
  }
 }
}
