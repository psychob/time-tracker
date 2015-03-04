using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
 public partial class MainWindow : Form
 {
  public MainWindow()
  {
   InitializeComponent();
  }

  private void MainWindow_Load(object sender, EventArgs e)
  {
   LoadBaseData();
   LoadTrackData();
   AddAllRunningProcess();
   EnableHook();
  }

  private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
   DisableHook();
   FinishAllTrackedProcess();
   SaveTrackData();
   SaveBaseData();
  }

  private void timer_RefreshCurrentApplications_Tick(object sender, EventArgs e)
  {
   refreshTrackApplication();
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

  private void applicationDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
   AppDB_Window.ApplicationDatabase appDbWin = new AppDB_Window.ApplicationDatabase();

   appDbWin.listOfAllApplications = dbBaseApplications;

   appDbWin.ShowDialog(this);

   dbBaseApplications = appDbWin.listOfAllApplications;
  }

  private void trackedApplicationDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
   AppDBTrackedApp_Window.ApplicationTrackedDatabase atd = new AppDBTrackedApp_Window.ApplicationTrackedDatabase();

   List<AppTrack_Entry> ate = dbTrackApplication.OrderByDescending(o => o.allTime).ToList();

   foreach (AppTrack_Entry it in ate)
   {
    var dbInfo = checkIfAppExistInDBInternal(it.internalName);
    if ( !dbInfo.HasValue )
     continue;

    ListViewItem lvi = new ListViewItem(dbInfo.Value.nameOfApp);
    lvi.SubItems.Add(Utils.calculateTime(it.allTime));

    atd.lvAllAps.Items.Add(lvi);
   }

   atd.ShowDialog(this);
  }

  private void removeZombiesToolStripMenuItem_Click(object sender, EventArgs e)
  {
   removeZombieProcess();
  }
 }
}
