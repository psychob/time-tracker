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
 public partial class Win_Tracked : Form
 {
  public List<application_tracked_detailed> tracked_apps;
  public Win_Tracked()
  {
   InitializeComponent();
  }

  private void Win_Tracked_Load(object sender, EventArgs e)
  {
   foreach (var it in tracked_apps)
   {
    ListViewItem lvi = new ListViewItem();

    lvi.SubItems.Add(it.name);
    lvi.SubItems.Add(Utils.calculateTime(it.time));

    listView1.Items.Add(lvi);
   }

   listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
   listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
  }
 }
}
