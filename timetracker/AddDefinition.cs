using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using timetracker.Entities;

namespace timetracker
{
	public partial class AddDefinition : Form
	{
		public AddDefinition()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			cmsNaming.Show(Cursor.Position);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			tbGroupName.Text = Guid.NewGuid().ToString();
		}

		private void AddDefinition_Load(object sender, EventArgs e)
		{
			if (tbUniqueName.Text == string.Empty)
				tbUniqueName.Text = Guid.NewGuid().ToString();

			tbGroupName.Text = Guid.NewGuid().ToString();
			cbGroupType.SelectedIndex = 0;
			cbGroupPriority.SelectedIndex = 1;
			cbMatchAlgo.SelectedIndex = 0;
			cbRuleMatchTo.SelectedIndex = 0;
		}

		private void intelligentNamingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetUniqueNameGenerator(true);

			RunIntelligentNamer();
		}

		private void guidToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetUniqueNameGenerator(false, true);

			tbUniqueName.Text = Guid.NewGuid().ToString();
		}

		private void exactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetUniqueNameGenerator(false, false, true);

			tbUniqueName.Text = tbAppName.Text;
		}

		private void userDefinedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetUniqueNameGenerator(false, false, false, true);
		}

		private void SetUniqueNameGenerator(bool i = false, bool g = false,
			bool e = false, bool u = false)
		{
			intelligentNamingToolStripMenuItem.Checked = i;
			guidToolStripMenuItem.Checked = g;
			exactToolStripMenuItem.Checked = e;
			userDefinedToolStripMenuItem.Checked = u;

			tbUniqueName.ReadOnly = !u;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (intelligentNamingToolStripMenuItem.Checked)
				RunIntelligentNamer();
			else if (exactToolStripMenuItem.Checked)
				tbUniqueName.Text = tbAppName.Text;
		}

		internal void RunIntelligentNamer()
		{
			if (tbAppName.Text == string.Empty)
				tbUniqueName.Text = Guid.NewGuid().ToString();
			else
			{
				string unique_name = tbAppName.Text[0].ToString();
				bool is_next = false;

				for (int it = 1; it < tbAppName.Text.Length; ++it)
				{
					if (tbAppName.Text[it].IsSymbol())
						is_next = true;
					else if (tbAppName.Text[it].IsWhite())
						is_next = true;
					else if (is_next || tbAppName.Text[it].IsUpper() || tbAppName.Text[it].IsNumber())
					{
						is_next = false;
						unique_name += tbAppName.Text[it];
					}
				}

				if (unique_name.Length < 3)
				{
					tbUniqueName.Text = tbAppName.Text.ToLower().Replace(" ", "");
				}
				else
				{
					tbUniqueName.Text = unique_name.ToLower();
				}
			}
		}

		public string ApplicationName
		{
			get
			{
				return tbAppName.Text;
			}

			set
			{
				tbAppName.Text = value;
			}
		}

		public string ApplicationUniqueID
		{
			get
			{
				return tbUniqueName.Text;
			}

			set
			{
				tbUniqueName.Text = value;
				SetUniqueNameGenerator(false, false, false, true);
			}
		}

		public AppRuleSet[] ApplicationRules
		{
			get
			{
				List<AppRuleSet> ars = new List<AppRuleSet>();

				foreach (var it in rulesets)
				{
					AppRuleSet rs = new AppRuleSet();

					rs = it.container;
					rs.Rules = ExtractRules(it.id);

					ars.Add(rs);
				}

				return ars.ToArray();
			}

			set
			{
				rule_id = 0;
				ruleset_id = 0;
				rules.Clear();
				rulesets.Clear();
				rules_to_ruleset.Clear();

				int rule_id_ = 0;
				int ruleset_id_ = 0;

				foreach (var it in value)
				{
					var rs = PackRule(ruleset_id_++, it);

					var tnrs = AddToTreeView(rs);
					rulesets.Add(rs);

					if (it.Rules == null)
						continue;

					foreach (var jt in it.Rules)
					{
						var r = PackRule(rule_id_++, jt);

						rules.Add(r);
						AddToTreeView(r, tnrs);

						rules_to_ruleset.Add(r.id, rs.id);
					}
				}

				rule_id = rule_id_;
				ruleset_id = ruleset_id_;
			}
		}

		private AppRule[] ExtractRules(int id)
		{
			List<AppRule> lar = new List<AppRule>();

			List<int> rules = rules_to_ruleset.Filter((k, v) => v == id).Keys.ToList();

			foreach (var it in this.rules)
			{
				if (rules.Contains(it.id))
					lar.Add(it.container);
			}

			return lar.ToArray();
		}

		private IdContainer<T> PackRule<T>(int id, T it)
		{
			return new IdContainer<T>(id, it);
		}

		private RuleSet RuleSetType
		{
			get
			{
				switch (cbGroupType.SelectedIndex)
				{
					case 0: // All
						return RuleSet.All;

					case 1: // Any
						return RuleSet.Any;
				}

				throw new ArgumentException();
			}

			set
			{
				switch (value)
				{
					case RuleSet.All:
						cbGroupType.SelectedIndex = 0;
						break;

					case RuleSet.Any:
						cbGroupType.SelectedIndex = 1;
						break;

					default:
						throw new NotSupportedException();
				}
			}
		}

		private RulePriority RuleSetPriority
		{
			get
			{
				switch (cbGroupPriority.SelectedIndex)
				{
					case 0: // low
						return RulePriority.Low;

					case 1: // medium
						return RulePriority.Medium;

					case 2: // high
						return RulePriority.High;
				}

				throw new NotSupportedException();
			}

			set
			{
				switch (value)
				{
					case RulePriority.Low:
						cbGroupPriority.SelectedIndex = 0;
						break;

					case RulePriority.Medium:
						cbGroupPriority.SelectedIndex = 1;
						break;

					case RulePriority.High:
						cbGroupPriority.SelectedIndex = 2;
						break;

					default:
						throw new NotSupportedException();
				}
			}
		}

		struct IdContainer<T>
		{
			public int id;
			public T container;

			public IdContainer(int id, T obj)
			{
				this.id = id;
				container = obj;
			}
		}

		int ruleset_id = 0;
		int rule_id = 0;

		List<IdContainer<AppRuleSet>> rulesets = new List<IdContainer<AppRuleSet>>();
		List<IdContainer<AppRule>> rules = new List<IdContainer<AppRule>>();
		Dictionary<int, int> rules_to_ruleset = new Dictionary<int, int>();

		private void button3_Click(object sender, EventArgs e)
		{
			if (tbGroupName.Text == string.Empty)
				return;

			if (rulesets.Contains(p => p.container.UniqueId == tbGroupName.Text))
				return;

			AppRuleSet ars = new AppRuleSet();

			ars.Kind = RuleSetType;
			ars.Priority = RuleSetPriority;
			ars.UniqueId = tbGroupName.Text;

			var idcnt = new IdContainer<AppRuleSet>(ruleset_id++, ars);

			rulesets.Add(idcnt);

			AddToTreeView(idcnt);

			tbGroupName.Text = Guid.NewGuid().ToString();
			RuleSetType = RuleSet.All;
			RuleSetPriority = RulePriority.Medium;
		}

		private TreeNode AddToTreeView(IdContainer<AppRuleSet> idcnt)
		{
			TreeNode tn = new TreeNode(idcnt.container.UniqueId + ", " +
				idcnt.container.Kind + ", " + idcnt.container.Priority, 0, 1);

			tn.Tag = (object)idcnt.id;

			treeView1.Nodes.Add(tn);

			return tn;
		}

		private void AddToTreeView(IdContainer<AppRule> idcont,
			TreeNode selectedNode)
		{
			TreeNode tn = new TreeNode(idcont.container.MatchString + " = " +
				idcont.container.MatchTo + ", " + idcont.container.MatchAlgorithm,
				2, 2);

			tn.Tag = (object)idcont.id;

			selectedNode.Nodes.Add(tn);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (tbPattern.Text == string.Empty)
				return;

			if (treeView1.SelectedNode == null)
				return;

			if (treeView1.SelectedNode.Parent != null)
				return;

			AppRule ar = new AppRule();

			ar.MatchString = tbPattern.Text;
			ar.MatchAlgorithm = MatchAlgorithm;
			ar.MatchTo = MatchTo;

			var idcont = new IdContainer<AppRule>(rule_id++, ar);

			rules.Add(idcont);
			rules_to_ruleset.Add(idcont.id, (int)treeView1.SelectedNode.Tag);

			AddToTreeView(idcont, treeView1.SelectedNode);

			tbPattern.Text = "";
			tbMatchTest.Text = "";
		}

		private AppRuleAlgorithm MatchAlgorithm
		{
			get
			{
				switch (cbMatchAlgo.SelectedIndex)
				{
					case 0:
						return AppRuleAlgorithm.Exact;
					case 1:
						return AppRuleAlgorithm.ExactInvariant;
					case 2:
						return AppRuleAlgorithm.Near;
					case 3:
						return AppRuleAlgorithm.NearInvariant;
					case 4:
						return AppRuleAlgorithm.Regex;
					case 5:
						return AppRuleAlgorithm.RegexInvariant;
					case 6:
						return AppRuleAlgorithm.StartsWith;
					case 7:
						return AppRuleAlgorithm.StartsWithInvariant;
					case 8:
						return AppRuleAlgorithm.EndWith;
					case 9:
						return AppRuleAlgorithm.EndWithInvariant;
				}

				throw new NotSupportedException();
			}
		}

		private AppRuleMatchTo MatchTo
		{
			get
			{
				switch (cbRuleMatchTo.SelectedIndex)
				{
					case 0:
						return AppRuleMatchTo.FileName;

					case 1:
						return AppRuleMatchTo.FileNamePath;

					case 2:
						return AppRuleMatchTo.FilePath;

					case 3:
						return AppRuleMatchTo.FileVersionName;

					case 4:
						return AppRuleMatchTo.FileVersionDesc;

					case 5:
						return AppRuleMatchTo.FileVersionCompany;

					case 6:
						return AppRuleMatchTo.FileVersionProductVersion;

					case 7:
						return AppRuleMatchTo.FileVersionFileVersion;

					case 8:
						return AppRuleMatchTo.FileMD5;
				}

				throw new NotSupportedException();
			}

			set
			{
				switch (value)
				{
					case AppRuleMatchTo.FileName:
						cbRuleMatchTo.SelectedIndex = 0;
						break;

					case AppRuleMatchTo.FileNamePath:
						cbRuleMatchTo.SelectedIndex = 1;
						break;

					case AppRuleMatchTo.FilePath:
						cbRuleMatchTo.SelectedIndex = 2;
						break;

					case AppRuleMatchTo.FileVersionName:
						cbRuleMatchTo.SelectedIndex = 3;
						break;

					case AppRuleMatchTo.FileVersionDesc:
						cbRuleMatchTo.SelectedIndex = 4;
						break;

					case AppRuleMatchTo.FileVersionCompany:
						cbRuleMatchTo.SelectedIndex = 5;
						break;

					case AppRuleMatchTo.FileVersionProductVersion:
						cbRuleMatchTo.SelectedIndex = 6;
						break;

					case AppRuleMatchTo.FileVersionFileVersion:
						cbRuleMatchTo.SelectedIndex = 7;
						break;

					case AppRuleMatchTo.FileMD5:
						cbRuleMatchTo.SelectedIndex = 8;
						break;

					default:
						throw new NotSupportedException();
				}
			}
		}

		private void tbMatchTest_TextChanged(object sender, EventArgs e)
		{
			TextTestChanged();
		}

		private void cbMatchAlgo_SelectedIndexChanged(object sender, EventArgs e)
		{
			TextTestChanged();
		}

		private void tbPattern_TextChanged(object sender, EventArgs e)
		{
			TextTestChanged();

			if (tbPattern.Text.IndexOf('\\') > -1 &&
				MatchTo == AppRuleMatchTo.FileName)
			{
				MatchTo = AppRuleMatchTo.FileNamePath;
			}
		}

		private void TextTestChanged()
		{
			if (tbPattern.Text == string.Empty || tbMatchTest.Text == string.Empty)
			{
				tbMatchTest.BackColor = Color.White;
				return;
			}

			if (TrackSystem.Utils.IsStringMatch(tbPattern.Text, tbMatchTest.Text, MatchAlgorithm))
				tbMatchTest.BackColor = Color.Lime;
			else
				tbMatchTest.BackColor = Color.Red;
		}

		private bool _IsValid = true;

		public bool IsValid
		{
			get
			{
				return tbAppName.Text != string.Empty &&
					tbUniqueName.Text != string.Empty &&
					rules.Count > 0 && rulesets.Count > 0 && _IsValid;
			}
		}

		public bool ApplicationEditing { get; internal set; }

		public bool ApplicationAllowOnlyOne
		{
			get
			{
				return cbAllowOnlyOne.Checked;
			}

			set
			{
				cbAllowOnlyOne.Checked = value;
			}
		}

		public bool MergeSpawned
		{
			get
			{
				return cbMergeSpawned.Checked;
			}

			set
			{
				cbMergeSpawned.Checked = value;
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			_IsValid = false;
			Close();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (!IsValid)
				return;

			Close();
		}

		private void tbUniqueName_TextChanged(object sender, EventArgs e)
		{
			tbUniqueName.Text = tbUniqueName.Text.ToLower();
		}

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (treeView1.SelectedNode == null)
					return;

				if (treeView1.SelectedNode.Parent == null)
					cmsGroup.Show(Cursor.Position);
				else
					cmsRule.Show(Cursor.Position);
			}
		}

		private void allToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Kind = RuleSet.All;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void anyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Kind = RuleSet.Any;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void lowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = RulePriority.Low;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = RulePriority.Medium;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void highToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = RulePriority.High;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			treeView1.Nodes.Remove(treeView1.SelectedNode);

			foreach (var it in rulesets)
			{
				if (it.id == ruleset_id)
				{
					rulesets.Remove(it);
					break;
				}
			}
		}

		private void exactToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = AppRuleAlgorithm.Exact;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void exactCaseInsensitiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = AppRuleAlgorithm.ExactInvariant;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void nearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = AppRuleAlgorithm.Near;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void nearCaseInsensitiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = AppRuleAlgorithm.NearInvariant;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void regularExpressionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = AppRuleAlgorithm.Regex;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private AppRuleSet GetRuleSet(int ruleset_id)
		{
			foreach (var it in rulesets)
				if (it.id == ruleset_id)
					return it.container;

			throw new ArgumentException();
		}

		private AppRule GetRule(int ruleset_id)
		{
			foreach (var it in rules)
				if (it.id == ruleset_id)
					return it.container;

			throw new ArgumentException();
		}

		private void ReplaceRuleSetNode(TreeNode selectedNode,
			AppRuleSet idcnt)
		{
			selectedNode.Text = idcnt.UniqueId + ", " +
				idcnt.Kind + ", " + idcnt.Priority;
		}

		private void ReplaceRuleSetNode(AppRuleSet rs, int ruleset_id)
		{
			foreach (var it in rulesets)
				if (it.id == ruleset_id)
				{
					rulesets.Remove(it);
					break;
				}

			rulesets.Add(PackRule(ruleset_id, rs));
		}

		private void ReplaceRuleSetNode(TreeNode selectedNode,
			AppRule container)
		{
			selectedNode.Text = container.MatchString + " = " + container.MatchTo + ", " +
				container.MatchAlgorithm;
		}

		private void ReplaceRuleSetNode(AppRule rs, int ruleset_id)
		{
			foreach (var it in rules)
				if (it.id == ruleset_id)
				{
					rules.Remove(it);
					break;
				}

			rules.Add(PackRule(ruleset_id, rs));
		}

		private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;

			foreach (var it in rules)
				if (it.id == ruleset_id)
				{
					rules.Remove(it);
					break;
				}

			rules_to_ruleset.Remove(ruleset_id);

			treeView1.Nodes.Remove(treeView1.SelectedNode);
		}
	}
}
