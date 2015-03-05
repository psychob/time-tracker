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
 public partial class AllApplicationDatabase : Form
 {
  public AllApplicationDatabase()
  {
   InitializeComponent();
  }

  public List<ApplicationDatabase.DatabaseEntry> listOfAllEntries;

  private void AllApplicationDatabase_Load(object sender, EventArgs e)
  {
   // posortujmy najpierw
   listOfAllEntries = listOfAllEntries.OrderBy(o => o.nameOfApplication).ToList();

   foreach (ApplicationDatabase.DatabaseEntry it in listOfAllEntries)
    lbAllAps.Items.Add(it.nameOfApplication);
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
   AddNewApplication ana = new AddNewApplication();

   ana.ShowDialog(this);

   if ( ana.isValid )
   {
    ApplicationDatabase.DatabaseEntry dbentry = new ApplicationDatabase.DatabaseEntry();

    dbentry.nameOfApplication = ana.entry_appName;
    dbentry.internalId = ana.entry_appInternal;
    dbentry.rules = ana.entry_rules;

    lbAllAps.Items.Add(dbentry.nameOfApplication);
    listOfAllEntries.Add(dbentry);
   }
  }
 }
}
