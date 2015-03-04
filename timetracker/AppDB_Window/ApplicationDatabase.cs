using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker.AppDB_Window
{
 public partial class ApplicationDatabase : Form
 {
  public ApplicationDatabase()
  {
   InitializeComponent();
  }

  public List<AppDB_Entry> listOfAllApplications { get; set; }

  private void button1_Click(object sender, EventArgs e)
  {
   AppDBNewApp_Window.ApplicationDatabase_AddNewApp ana = new AppDBNewApp_Window.ApplicationDatabase_AddNewApp();

   ana.ShowDialog(this);

   if ( ana.entry_isValid )
   {
    AppDB_Entry entry = new AppDB_Entry();
    entry.nameOfApp = ana.entry_name;
    entry.internalNameOfApp = ana.entry_internalName;
    entry.rules = ana.entry_rules;

    listOfAllApplications.Add(entry);

    lbAllApps.Items.Add(entry.nameOfApp);
   }
  }

  private void ApplicationDatabase_Load(object sender, EventArgs e)
  {
   foreach (AppDB_Entry it in listOfAllApplications)
    lbAllApps.Items.Add(it.nameOfApp);
  }
 }
}
