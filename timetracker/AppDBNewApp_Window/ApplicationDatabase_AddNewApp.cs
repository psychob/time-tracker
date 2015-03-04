using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker.AppDBNewApp_Window
{
 public partial class ApplicationDatabase_AddNewApp : Form
 {
  public ApplicationDatabase_AddNewApp()
  {
   InitializeComponent();
  }

  public bool entry_isValid
  {
   get
   {
    return entry_name != "" && entry_internalName != "" && entry_rules.Length != 0;
   }
  }

  public string entry_name { get { return tbNameOfApplication.Text; } }
  public string entry_internalName { get { return tbInternalNameOfApplication.Text; } }
  public AppDB_Entry_Rules[] entry_rules { get { return _entry_rules.ToArray(); } }
  private List<AppDB_Entry_Rules> _entry_rules = new List<AppDB_Entry_Rules>();

  private void tbNameOfApplication_TextChanged(object sender, EventArgs e)
  {
   string str = tbNameOfApplication.Text.ToLowerInvariant();
   str = str.Replace(":", "");
   str = str.Replace("+", "");
   str = str.Replace(" ", "-");
   tbInternalNameOfApplication.Text = str;
  }

  private void ApplicationDatabase_AddNewApp_Load(object sender, EventArgs e)
  {
   cbCompareUsingMethod.SelectedIndex = 0;
   cbCompareWith.SelectedIndex = 0;
  }

  private void btnNewRule_Click(object sender, EventArgs e)
  {
   if ( tbNameOfApplication.Text == "" )
   {
    MessageBox.Show("You must set name of application!");
    return;
   }

   if ( tbInternalNameOfApplication.Text == "" )
   {
    MessageBox.Show("You Must Set Internal Name Of Application!");
    return;
   }

   if ( cbCompareWith.SelectedIndex == -1 )
   {
    MessageBox.Show("Compare With need to be set!");
    return;
   }

   if ( cbCompareUsingMethod.SelectedIndex == -1 )
   {
    MessageBox.Show("Compare algorithm must be set!");
    return;
   }

   if ( tbTextToCompare.Text == "" )
   {
    MessageBox.Show("Compare text need to be set");
    return;
   }

   AppDB_Entry_Rules newRule = new AppDB_Entry_Rules();
   string to_add = "Text: '";
   newRule.matchString = tbTextToCompare.Text;

   to_add += tbTextToCompare.Text;
   to_add += "' is compared to: ";

   switch ( cbCompareWith.SelectedIndex )
   {
    case 0:
     newRule.matchToWhat = AppDB_Entry_Properties.FileName;
     to_add += "File Name";
     break;

    case 1:
     newRule.matchToWhat = AppDB_Entry_Properties.FilePath;
     to_add += "File Path";
     break;
   }

   to_add += " using algorithm: ";

   switch ( cbCompareUsingMethod.SelectedIndex )
   {
    case 0:
     newRule.matchAlgorithm = Utils.MatchAlgorithm.ExactSensitive;
     to_add += "Exact Sensitive";
     break;

    case 1:
     newRule.matchAlgorithm = Utils.MatchAlgorithm.ExactInsensitive;
     to_add += "Exact Insensitive";
     break;

    case 2:
     newRule.matchAlgorithm = Utils.MatchAlgorithm.NearSensitive;
     to_add += "Near Sensitive";
     break;

    case 3:
     newRule.matchAlgorithm = Utils.MatchAlgorithm.NearInsensitive;
     to_add += "Near Insensitive";
     break;

    case 4:
     newRule.matchAlgorithm = Utils.MatchAlgorithm.RegularExpression;
     to_add += "Regular Expression";
     break;
   }

   _entry_rules.Add(newRule);
   lbAllRules.Items.Add( to_add );

   tbTextToCompare.Text = "";
  }

  private void btnAddApplication_Click(object sender, EventArgs e)
  {
   Close();
  }
 }
}
