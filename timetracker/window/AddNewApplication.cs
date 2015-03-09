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
 public partial class AddNewApplication : Form
 {
  public AddNewApplication()
  {
   InitializeComponent();
  }

  public bool isEditable = true;
  public bool isValid
  {
   get
   {
    return entry_appName != "" && entry_appInternal != "" && entry_rules_list.Count != 0;
   }
  }

  public string entry_appName
  {
   get
   {
    return tbApplicationTitle.Text;
   }

   set
   {
    tbApplicationTitle.Text = value;
   }
  }
  public string entry_appInternal
  {
   get
   {
    return tbInternalString.Text;
   }

   set
   {
    tbInternalString.Text = value;
   }
  }

  public ApplicationDatabase.DatabaseEntryRuleCompareTo entry_currentRule_compareTo
  {
   get
   {
    switch ( cbCompareWith.SelectedIndex )
    {
     case 0:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileName;

     case 1:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FilePath;

     case 2:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionName;

     case 3:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionDescription;

     case 4:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionCompany;

     case 5:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionProductVersion;

     case 6:
      return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionFileVersion;
    }

    return ApplicationDatabase.DatabaseEntryRuleCompareTo.FileName;
   }

   set
   {
    switch (value)
    {
     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileName:
      cbCompareWith.SelectedIndex = 0;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FilePath:
      cbCompareWith.SelectedIndex = 1;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionName:
      cbCompareWith.SelectedIndex = 2;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionDescription:
      cbCompareWith.SelectedIndex = 3;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionCompany:
      cbCompareWith.SelectedIndex = 4;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionProductVersion:
      cbCompareWith.SelectedIndex = 5;
      break;

     case ApplicationDatabase.DatabaseEntryRuleCompareTo.FileVersionFileVersion:
      cbCompareWith.SelectedIndex = 6;
      break;
    }

    // retszte ignorujemy
   }
  }
  public Utils.MatchAlgorithm entry_currentRule_compareAlgorithm
  {
   get
   {
    switch ( cbCompareAlgorithm.SelectedIndex )
    {
     case 0:
      return Utils.MatchAlgorithm.ExactSensitive;

     case 1:
      return Utils.MatchAlgorithm.ExactInsensitive;

     case 2:
      return Utils.MatchAlgorithm.NearSensitive;

     case 3:
      return Utils.MatchAlgorithm.NearInsensitive;

     case 4:
      return Utils.MatchAlgorithm.RegularExpression;
    }

    return Utils.MatchAlgorithm.ExactSensitive;
   }

   set
   {
    switch ( value )
    {
     case Utils.MatchAlgorithm.ExactSensitive:
      cbCompareAlgorithm.SelectedIndex = 0;
      break;

     case Utils.MatchAlgorithm.ExactInsensitive:
      cbCompareAlgorithm.SelectedIndex = 1;
      break;

     case Utils.MatchAlgorithm.NearSensitive:
      cbCompareAlgorithm.SelectedIndex = 2;
      break;

     case Utils.MatchAlgorithm.NearInsensitive:
      cbCompareAlgorithm.SelectedIndex = 3;
      break;

     case Utils.MatchAlgorithm.RegularExpression:
      cbCompareAlgorithm.SelectedIndex = 4;
      break;
    }

    // reszte wartości ignorujemy
   }
  }
  public string entry_currentRule_stringMatch
  {
   get
   {
    return tbPattern.Text;
   }

   set
   {
    tbPattern.Text = value;
   }
  }
  public bool entry_currentRule_isRequired
  {
   get
   {
    return cbIsReqired.Checked;
   }

   set
   {
    cbIsReqired.Checked = value;
   }
  }

  private List<ApplicationDatabase.DatabaseEntryRule> entry_rules_list = new List<ApplicationDatabase.DatabaseEntryRule>();
  public List<ApplicationDatabase.DatabaseEntryRule> entry_rules_list_;
  public ApplicationDatabase.DatabaseEntryRule[] entry_rules
  {
   get
   {
    return entry_rules_list.ToArray();
   }

   set
   {
    entry_rules_list = value.ToList();
   }
  }

  private void tbApplicationTitle_TextChanged(object sender, EventArgs e)
  {
   if (isEditable)
    generateInternalName(tbApplicationTitle.Text);
  }

  private void generateInternalName(string str)
  {
   string tmp = str.ToLower();
   foreach (var it in new char[] { ':', '.', '+', '(', ')', '*', '&', ' ', '\t', '\n', '\r' })
    tmp = tmp.Replace(it, '-');

   while (tmp.IndexOf("--") != -1)
    tmp = tmp.Replace("--", "-");

   tbInternalString.Text = tmp;
  }

  private void AddNewApplication_Load(object sender, EventArgs e)
  {
   entry_currentRule_compareAlgorithm = Utils.MatchAlgorithm.ExactInsensitive;
   entry_currentRule_compareTo = ApplicationDatabase.DatabaseEntryRuleCompareTo.FileName;

   btnNewRule.Enabled = false;

   if (!isEditable)
   {
    tbInternalString.Enabled = false;

    foreach ( var it in entry_rules_list_ )
    {
     _AddNewRule(it);
    }
   }
  }

  private void tbPattern_TextChanged(object sender, EventArgs e)
  {
   btnNewRule.Enabled = tbPattern.Text.Length != 0;
   checkIfPatternTextMatch();
  }

  private void tbTestingString_TextChanged(object sender, EventArgs e)
  {
   checkIfPatternTextMatch();
  }

  private void checkIfPatternTextMatch()
  {
   if (tbPattern.Text == "" || tbTestingString.Text == "")
    return;

   if (Utils.compareStrings(entry_currentRule_stringMatch, tbTestingString.Text, entry_currentRule_compareAlgorithm))
    tbTestingString.BackColor = Color.LimeGreen;
   else
    tbTestingString.BackColor = Color.Red;

  }

  private void cbCompareAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
  {
   checkIfPatternTextMatch();
  }

  private void btnNewRule_Click(object sender, EventArgs e)
  {
   if (entry_currentRule_stringMatch == "")
    return;

   _AddNewRule();

   string to_add = "Compare: " + entry_currentRule_compareTo.ToString() + " to: '" + entry_currentRule_stringMatch + "' with: " + entry_currentRule_compareAlgorithm.ToString();

   ApplicationDatabase.DatabaseEntryRule der = new ApplicationDatabase.DatabaseEntryRule();

   der.required = entry_currentRule_isRequired;
   der.str = entry_currentRule_stringMatch;
   der.how = entry_currentRule_compareAlgorithm;
   der.what = entry_currentRule_compareTo;

   entry_rules_list.Add(der);
   lbAllRules.Items.Add(to_add);

   entry_currentRule_isRequired = false;
   entry_currentRule_stringMatch = "";
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
   Close();
  }

  private void btnDiscard_Click(object sender, EventArgs e)
  {
   entry_appInternal = "";
   Close();
  }

  private void removeToolStripMenuItem_Click(object sender, EventArgs e)
  {
   if ( lbAllRules.SelectedIndex != -1 )
   {
    entry_rules_list.RemoveAt(lbAllRules.SelectedIndex);
    lbAllRules.Items.RemoveAt(lbAllRules.SelectedIndex);
   }
  }

  private void _AddNewRule(ApplicationDatabase.DatabaseEntryRule it)
  {
   _AddNewRule(it.how, it.required, it.str, it.what);
  }

  private void _AddNewRule()
  {
   _AddNewRule(entry_currentRule_compareAlgorithm, entry_currentRule_isRequired,
               entry_currentRule_stringMatch, entry_currentRule_compareTo);

   entry_currentRule_isRequired = false;
   entry_currentRule_stringMatch = "";
  }

  private void _AddNewRule(Utils.MatchAlgorithm matchAlgorithm, bool isRequired,
                           string matchString,
                           ApplicationDatabase.DatabaseEntryRuleCompareTo databaseEntryRuleCompareTo)
  {
   string to_add = "String: '" + matchString + "' will be matched with: " +
                   databaseEntryRuleCompareTo.ToString() + " with algorithm: " +
                   matchAlgorithm.ToString() + " is required: " + isRequired.ToString();

   lbAllRules.Items.Add(to_add);

   ApplicationDatabase.DatabaseEntryRule der = new ApplicationDatabase.DatabaseEntryRule();

   der.required = isRequired;
   der.str = matchString;
   der.how = matchAlgorithm;
   der.what = databaseEntryRuleCompareTo;

   entry_rules_list.Add(der);
  }
 }
}
