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

   appDB = new ApplicationDatabase(this);

   appDB.StartApp();
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
  }

  private void applicationDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
   window.AllApplicationDatabase aad = new window.AllApplicationDatabase();

   aad.listOfAllEntries = appDB.PollAllEntries();

   aad.ShowDialog(this);

   appDB.SynchronizeEntries(aad.listOfAllEntries);
  }
 }
}
