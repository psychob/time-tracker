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
 public partial class Win_ItemDisplay : Form
 {
  public Win_ItemDisplay()
  {
   InitializeComponent();
  }

  public string entry_name
  {
   get
   {
    return tbName.Text;
   }
   set
   {
    tbName.Text = value;
   }
  }

  public Guid entry_guid
  {
   get
   {
    return Guid.Parse(tbGuid.Text);
   }
   set
   {
    tbGuid.Text = value.ToString();
   }
  }

  public application_ruleset[] entry_ruleset
  {
   get
   {
    List<application_ruleset> lar = new List<application_ruleset>();

    foreach ( var it in rulesets )
    {
     application_ruleset ar = new application_ruleset();

     ar.rules_name = it.Value.rules_name;
     ar.kind = it.Value.kind;

     List<application_rule> lar_ = new List<application_rule>();
     // pobieramy zasady
     foreach ( var jt in rule_to_ruleset )
     {
      if (it.Key == jt.Value)
       lar_.Add(all_rules[jt.Key]);
     }

     if (lar_.Count > 0)
     {
      ar.rules = lar_.ToArray();
      lar.Add(ar);
     }
    }

    return lar.ToArray();
   }

   set
   {
    foreach ( var it in value )
    {
     int current = last_rulesets;
     last_rulesets++;

     rulesets[current] = it;

     foreach ( var jt in it.rules )
     {
      int cr = last_rule;
      last_rule++;

      all_rules[cr] = jt;
      rule_to_ruleset[cr] = current;
     }
    }

    update_tree_node();
   }
  }

  private void update_tree_node()
  {
   foreach ( var it in rulesets )
   {
    TreeNode tn = new TreeNode();
    tn.Tag = (object)it.Key;
    tn.Text = "Name: " + it.Value.rules_name + " (match type: " + it.Value.kind.ToString() + ")";

    foreach ( var jt in rule_to_ruleset )
    {
     if ( jt.Value == it.Key )
     {
      TreeNode tnc = new TreeNode();

      tnc.Tag = (object)jt.Key;
      tnc.Text = all_rules[jt.Key].match_to.ToString() + " == " +
                 all_rules[jt.Key].math_what + " with: " +
                 all_rules[jt.Key].algorithm.ToString();

      tn.Nodes.Add(tnc);
     }
    }

    treeView1.Nodes.Add(tn);
   }
  }

  private void button1_Click(object sender, EventArgs e)
  {
   entry_guid = Guid.NewGuid();
  }

  public bool entry_valid {
   get
   {
    return tbGuid.Text != string.Empty && entry_name != string.Empty &&
           all_rules.Count > 0 && rule_to_ruleset.Count > 0;
   }
  }

  private void Win_ItemDisplay_Load(object sender, EventArgs e)
  {
   if (entry_valid)
   {
    button1.Enabled = false;
   } else
   {
    entry_guid = Guid.NewGuid();
   }

   cbGroupType.SelectedIndex = 0;
   cbCompareTo.SelectedIndex = 0;
   cbAlgorithm.SelectedIndex = 1;
  }

  Dictionary<int, application_ruleset> rulesets = new Dictionary<int, application_ruleset>();
  Dictionary<int, application_rule> all_rules = new Dictionary<int, application_rule>();
  Dictionary<int, int> rule_to_ruleset = new Dictionary<int, int>();

  int last_rulesets = 0;
  int last_rule = 0;

  private void button2_Click(object sender, EventArgs e)
  {
   if (tbGroupName.Text != string.Empty && cbGroupType.SelectedIndex != -1 )
   {
    application_ruleset ar = new application_ruleset();
    ar.kind = group_kind;
    ar.rules_name = tbGroupName.Text;

    int ruleset_id = last_rulesets;
    rulesets[ruleset_id] = ar;

    TreeNode tn = new TreeNode();
    tn.Text = "Name: " + ar.rules_name + " (match type: " + cbGroupType.Text + ")";
    tn.Tag = (object)ruleset_id;

    treeView1.Nodes.Add(tn);

    tbGroupName.Text = "";
    last_rulesets++;
   }
  }

  public application_ruleset_kind group_kind
  {
   get
   {
    switch ( cbGroupType.SelectedIndex )
    {
     case 0:
      return application_ruleset_kind.all;

     case 1:
      return application_ruleset_kind.any;
    }

    return application_ruleset_kind.any;
   }
   set
   {
    switch ( value )
    {
     case application_ruleset_kind.all:
      cbGroupType.SelectedIndex = 0;
      break;

     case application_ruleset_kind.any:
      cbGroupType.SelectedIndex = 1;
      break;
    }
   }
  }

  private void button3_Click(object sender, EventArgs e)
  {
   if (tbMatchString.Text == string.Empty || cbAlgorithm.SelectedIndex == -1 ||
        cbCompareTo.SelectedIndex == -1)
    return;

   if (treeView1.SelectedNode == null ||
       (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
      )
    return;

   application_rule ar = new application_rule();
   ar.algorithm = rule_algorithm;
   ar.match_to = rule_match_to;
   ar.math_what = tbMatchString.Text;

   int rule_id = last_rule;
   all_rules[rule_id] = ar;
   rule_to_ruleset[rule_id] = (int)treeView1.SelectedNode.Tag;

   TreeNode tn = new TreeNode();
   tn.Text = tbMatchString.Text + " == " + cbCompareTo.Text + " with: "
                                + cbAlgorithm.Text;
   tn.Tag = (object)rule_id;

   treeView1.SelectedNode.Nodes.Add(tn);

   last_rule++;
   tbMatchString.Text = "";
  }

  public Utils.MatchAlgorithm rule_algorithm
  {
   get
   {
    switch ( cbAlgorithm.SelectedIndex )
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
    switch (value)
    {
     case Utils.MatchAlgorithm.ExactInsensitive:
      cbAlgorithm.SelectedIndex = 1;
      break;

     case Utils.MatchAlgorithm.ExactSensitive:
      cbAlgorithm.SelectedIndex = 0;
      break;

     case Utils.MatchAlgorithm.NearInsensitive:
      cbAlgorithm.SelectedIndex = 3;
      break;

     case Utils.MatchAlgorithm.NearSensitive:
      cbAlgorithm.SelectedIndex = 2;
      break;

     case Utils.MatchAlgorithm.RegularExpression:
      cbAlgorithm.SelectedIndex = 4;
      break;
    }
   }
  }

  public application_rule_match_to rule_match_to
  {
   get
   {
    switch ( cbCompareTo.SelectedIndex )
    {
     case 0:
      return application_rule_match_to.file_name;

     case 1:
      return application_rule_match_to.file_name_path;

     case 2:
      return application_rule_match_to.file_path;

     case 3:
      return application_rule_match_to.file_version_name;

     case 4:
      return application_rule_match_to.file_version_desc;

     case 5:
      return application_rule_match_to.file_version_company;

     case 6:
      return application_rule_match_to.file_version_product_version;

     case 7:
      return application_rule_match_to.file_version_file_version;

     case 8:
      return application_rule_match_to.file_md5;
    }

    return application_rule_match_to.file_name;
   }

   set
   {
    switch (value)
    {
     case application_rule_match_to.file_md5:
      cbCompareTo.SelectedIndex = 8;
      break;

     case application_rule_match_to.file_name:
      cbCompareTo.SelectedIndex = 0;
      break;

     case application_rule_match_to.file_name_path:
      cbCompareTo.SelectedIndex = 1;
      break;

     case application_rule_match_to.file_path:
      cbCompareTo.SelectedIndex = 2;
      break;

     case application_rule_match_to.file_version_company:
      cbCompareTo.SelectedIndex = 5;
      break;

     case application_rule_match_to.file_version_desc:
      cbCompareTo.SelectedIndex = 4;
      break;

     case application_rule_match_to.file_version_file_version:
      cbCompareTo.SelectedIndex = 7;
      break;

     case application_rule_match_to.file_version_name:
      cbCompareTo.SelectedIndex = 3;
      break;

     case application_rule_match_to.file_version_product_version:
      cbCompareTo.SelectedIndex = 6;
      break;
    }
   }
  }

  private void button4_Click(object sender, EventArgs e)
  {
   if (entry_valid)
    Close();
  }

  private void button5_Click(object sender, EventArgs e)
  {
   tbGuid.Text = "";
   Close();
  }

  private void textBox5_TextChanged(object sender, EventArgs e)
  {
   update_test_box();
  }

  private void update_test_box()
  {
   if (textBox5.Text == "" || tbMatchString.Text == "")
    return;

   if (Utils.compareStrings(tbMatchString.Text, textBox5.Text, rule_algorithm))
    textBox5.BackColor = Color.Lime;
   else
    textBox5.BackColor = Color.Red;
  }

  private void tbMatchString_TextChanged(object sender, EventArgs e)
  {
   update_test_box();
  }

  private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
  {
   update_test_box();
  }

  private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
  {
   if (treeView1.SelectedNode == null)
    return;

   var cnode = treeView1.SelectedNode;

   int rid = (int)cnode.Tag;
   treeView1.Nodes.Remove(cnode);

   if (cnode.Parent != null)
    rule_to_ruleset.Remove(rid);
   else
    rulesets.Remove(rid);
  }
 }
}
