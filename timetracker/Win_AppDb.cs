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
 public partial class Win_AppDb : Form
 {
  public List<application_definition> all_aps;

  public Win_AppDb()
  {
   InitializeComponent();
  }

  private void Win_AppDb_Load(object sender, EventArgs e)
  {
   foreach ( var it in all_aps )
    listBox1.Items.Add(it.name);
  }

  private void button1_Click(object sender, EventArgs e)
  {
   Win_ItemDisplay wid = new Win_ItemDisplay();

   wid.ShowDialog();

   if ( wid.entry_valid )
   {
    application_definition app_def = new application_definition();
    app_def.name = wid.entry_name;
    app_def.guid = wid.entry_guid;
    app_def.ruleset = wid.entry_ruleset;

    all_aps.Add(app_def);
    listBox1.Items.Add(app_def.name);
   }
  }
 }
}
