using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker.window
{
 public partial class ShowTracked : Form
 {
  public ShowTracked()
  {
   InitializeComponent();
  }

  public List<ApplicationDatabase.DatabaseTrackView> listOfTracked;

  private void ShowTracked_Load(object sender, EventArgs e)
  {
   listOfTracked = listOfTracked.OrderByDescending(o => o.allTime).ToList();

   foreach (ApplicationDatabase.DatabaseTrackView it in listOfTracked)
   {
    ListViewItem lvi = new ListViewItem(it.name);
    lvi.SubItems.Add(Utils.calculateTime(it.allTime));
    lvTrackedApps.Items.Add(lvi);
   }

   lvTrackedApps.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
   lvTrackedApps.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
  }
 }
}
