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
    this.Text = "Time Tracker v" + fvi.ProductVersion.Trim('0', '.');
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
   tsslInvalidProcessQueue.Text = appDB.GetInvalidProcessQueueCount().ToString();

   refresh_time -= (ulong)timer_updateTimers.Interval;
   validate_time -= (ulong)timer_updateTimers.Interval;
  }

  private void sToolStripMenuItem_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 5;
   refresh_time = 1000 * 5;
   sToolStripMenuItem.Checked = true;
  }

  private void sToolStripMenuItem1_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 15;
   refresh_time = 1000 * 15;
   sToolStripMenuItem1.Checked = true;
  }

  private void sToolStripMenuItem2_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 30;
   refresh_time = 1000 * 30;
   sToolStripMenuItem2.Checked = true;
  }

  private void sToolStripMenuItem3_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 45;
   refresh_time = 1000 * 45;
   sToolStripMenuItem3.Checked = true;
  }

  private void mToolStripMenuItem_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 60 * 1;
   refresh_time = 1000 * 60 * 1;
   mToolStripMenuItem.Checked = true;
  }

  private void mToolStripMenuItem1_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 60 * 2;
   refresh_time = 1000 * 60 * 2;
   mToolStripMenuItem1.Checked = true;
  }

  private void mToolStripMenuItem2_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 60 * 5;
   refresh_time = 1000 * 60 * 5;
   mToolStripMenuItem2.Checked = true;
  }

  private void mToolStripMenuItem3_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 60 * 10;
   refresh_time = 1000 * 60 * 10;
   mToolStripMenuItem3.Checked = true;
  }

  private void mToolStripMenuItem4_Click(object sender, EventArgs e)
  {
   sToolStripMenuItem.Checked = false;
   sToolStripMenuItem1.Checked = false;
   sToolStripMenuItem2.Checked = false;
   sToolStripMenuItem3.Checked = false;
   mToolStripMenuItem.Checked = false;
   mToolStripMenuItem1.Checked = false;
   mToolStripMenuItem2.Checked = false;
   mToolStripMenuItem3.Checked = false;
   mToolStripMenuItem4.Checked = false;

   timer_RefreshCurrentApplications.Interval = 1000 * 60 * 15;
   refresh_time = 1000 * 60 * 15;
   mToolStripMenuItem3.Checked = true;
  }

  private void mToolStripMenuItem5_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 1;
   validate_time = 1000 * 60 * 1;

   mToolStripMenuItem5.Checked = true;
  }

  private void mToolStripMenuItem6_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 2;
   validate_time = 1000 * 60 * 2;

   mToolStripMenuItem6.Checked = true;
  }

  private void mToolStripMenuItem7_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 5;
   validate_time = 1000 * 60 * 5;

   mToolStripMenuItem7.Checked = true;
  }

  private void mToolStripMenuItem8_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 10;
   validate_time = 1000 * 60 * 10;

   mToolStripMenuItem8.Checked = true;
  }

  private void mToolStripMenuItem9_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 15;
   validate_time = 1000 * 60 * 15;

   mToolStripMenuItem9.Checked = true;
  }

  private void mToolStripMenuItem10_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 30;
   validate_time = 1000 * 60 * 30;

   mToolStripMenuItem10.Checked = true;
  }

  private void hToolStripMenuItem_Click(object sender, EventArgs e)
  {
   mToolStripMenuItem5.Checked = false;
   mToolStripMenuItem6.Checked = false;
   mToolStripMenuItem7.Checked = false;
   mToolStripMenuItem8.Checked = false;
   mToolStripMenuItem9.Checked = false;
   mToolStripMenuItem10.Checked = false;
   hToolStripMenuItem.Checked = false;

   timer_validate.Interval = 1000 * 60 * 60;
   validate_time = 1000 * 60 * 60;

   hToolStripMenuItem.Checked = true;
  }

  private void timer_InvalidProcessorRefresh_Tick(object sender, EventArgs e)
  {
   appDB.ProcessInvalidProcessQueue();
  }
 }
}
