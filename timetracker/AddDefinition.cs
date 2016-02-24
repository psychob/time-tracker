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

		public TrackSystem.Structs.AppRuleSet[] ApplicationRules
		{
			get
			{
				List<TrackSystem.Structs.AppRuleSet> ars = new List<TrackSystem.Structs.AppRuleSet>();

				foreach (var it in rulesets)
				{
					TrackSystem.Structs.AppRuleSet rs = new TrackSystem.Structs.AppRuleSet();

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

		private TrackSystem.Structs.AppRule[] ExtractRules(int id)
		{
			List<TrackSystem.Structs.AppRule> lar = new List<TrackSystem.Structs.AppRule>();

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

		private TrackSystem.Structs.RuleSet RuleSetType
		{
			get
			{
				switch (cbGroupType.SelectedIndex)
				{
					case 0: // All
						return TrackSystem.Structs.RuleSet.All;

					case 1: // Any
						return TrackSystem.Structs.RuleSet.Any;
				}

				throw new ArgumentException();
			}

			set
			{
				switch (value)
				{
					case TrackSystem.Structs.RuleSet.All:
						cbGroupType.SelectedIndex = 0;
						break;

					case TrackSystem.Structs.RuleSet.Any:
						cbGroupType.SelectedIndex = 1;
						break;

					default:
						throw new NotSupportedException();
				}
			}
		}

		private TrackSystem.Structs.RulePriority RuleSetPriority
		{
			get
			{
				switch (cbGroupPriority.SelectedIndex)
				{
					case 0: // low
						return TrackSystem.Structs.RulePriority.Low;

					case 1: // medium
						return TrackSystem.Structs.RulePriority.Medium;

					case 2: // high
						return TrackSystem.Structs.RulePriority.High;
				}

				throw new NotSupportedException();
			}

			set
			{
				switch (value)
				{
					case TrackSystem.Structs.RulePriority.Low:
						cbGroupPriority.SelectedIndex = 0;
						break;

					case TrackSystem.Structs.RulePriority.Medium:
						cbGroupPriority.SelectedIndex = 1;
						break;

					case TrackSystem.Structs.RulePriority.High:
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

		List<IdContainer<TrackSystem.Structs.AppRuleSet>> rulesets = new List<IdContainer<TrackSystem.Structs.AppRuleSet>>();
		List<IdContainer<TrackSystem.Structs.AppRule>> rules = new List<IdContainer<TrackSystem.Structs.AppRule>>();
		Dictionary<int, int> rules_to_ruleset = new Dictionary<int, int>();

		private void button3_Click(object sender, EventArgs e)
		{
			if (tbGroupName.Text == string.Empty)
				return;

			if (rulesets.Contains(p => p.container.UniqueId == tbGroupName.Text))
				return;

			TrackSystem.Structs.AppRuleSet ars = new TrackSystem.Structs.AppRuleSet();

			ars.Kind = RuleSetType;
			ars.Priority = RuleSetPriority;
			ars.UniqueId = tbGroupName.Text;

			var idcnt = new IdContainer<TrackSystem.Structs.AppRuleSet>(ruleset_id++, ars);

			rulesets.Add(idcnt);

			AddToTreeView(idcnt);

			tbGroupName.Text = Guid.NewGuid().ToString();
			RuleSetType = TrackSystem.Structs.RuleSet.All;
			RuleSetPriority = TrackSystem.Structs.RulePriority.Medium;
		}

		private TreeNode AddToTreeView(IdContainer<TrackSystem.Structs.AppRuleSet> idcnt)
		{
			TreeNode tn = new TreeNode(idcnt.container.UniqueId + ", " +
				idcnt.container.Kind + ", " + idcnt.container.Priority, 0, 1);

			tn.Tag = (object)idcnt.id;

			treeView1.Nodes.Add(tn);

			return tn;
		}

		private void AddToTreeView(IdContainer<TrackSystem.Structs.AppRule> idcont,
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

			TrackSystem.Structs.AppRule ar = new TrackSystem.Structs.AppRule();

			ar.MatchString = tbPattern.Text;
			ar.MatchAlgorithm = MatchAlgorithm;
			ar.MatchTo = MatchTo;

			var idcont = new IdContainer<TrackSystem.Structs.AppRule>(rule_id++, ar);

			rules.Add(idcont);
			rules_to_ruleset.Add(idcont.id, (int)treeView1.SelectedNode.Tag);

			AddToTreeView(idcont, treeView1.SelectedNode);

			tbPattern.Text = "";
			tbMatchTest.Text = "";
		}

		private TrackSystem.Structs.AppRuleAlgorithm MatchAlgorithm
		{
			get
			{
				switch (cbMatchAlgo.SelectedIndex)
				{
					case 0:
						return TrackSystem.Structs.AppRuleAlgorithm.Exact;
					case 1:
						return TrackSystem.Structs.AppRuleAlgorithm.ExactInvariant;
					case 2:
						return TrackSystem.Structs.AppRuleAlgorithm.Near;
					case 3:
						return TrackSystem.Structs.AppRuleAlgorithm.NearInvariant;
					case 4:
						return TrackSystem.Structs.AppRuleAlgorithm.Regex;
					case 5:
						return TrackSystem.Structs.AppRuleAlgorithm.RegexInvariant;
					case 6:
						return TrackSystem.Structs.AppRuleAlgorithm.StartsWith;
					case 7:
						return TrackSystem.Structs.AppRuleAlgorithm.StartsWithInvariant;
					case 8:
						return TrackSystem.Structs.AppRuleAlgorithm.EndWith;
					case 9:
						return TrackSystem.Structs.AppRuleAlgorithm.EndWithInvariant;
				}

				throw new NotSupportedException();
			}
		}

		private TrackSystem.Structs.AppRuleMatchTo MatchTo
		{
			get
			{
				switch (cbRuleMatchTo.SelectedIndex)
				{
					case 0:
						return TrackSystem.Structs.AppRuleMatchTo.FileName;

					case 1:
						return TrackSystem.Structs.AppRuleMatchTo.FileNamePath;

					case 2:
						return TrackSystem.Structs.AppRuleMatchTo.FilePath;

					case 3:
						return TrackSystem.Structs.AppRuleMatchTo.FileVersionName;

					case 4:
						return TrackSystem.Structs.AppRuleMatchTo.FileVersionDesc;

					case 5:
						return TrackSystem.Structs.AppRuleMatchTo.FileVersionCompany;

					case 6:
						return TrackSystem.Structs.AppRuleMatchTo.FileVersionProductVersion;

					case 7:
						return TrackSystem.Structs.AppRuleMatchTo.FileVersionFileVersion;

					case 8:
						return TrackSystem.Structs.AppRuleMatchTo.FileMD5;
				}

				throw new NotSupportedException();
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
			TrackSystem.Structs.AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Kind = TrackSystem.Structs.RuleSet.All;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void anyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Kind = TrackSystem.Structs.RuleSet.Any;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void lowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = TrackSystem.Structs.RulePriority.Low;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = TrackSystem.Structs.RulePriority.Medium;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void highToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRuleSet rs = GetRuleSet(ruleset_id);
			rs.Priority = TrackSystem.Structs.RulePriority.High;

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
			TrackSystem.Structs.AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = TrackSystem.Structs.AppRuleAlgorithm.Exact;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void exactCaseInsensitiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = TrackSystem.Structs.AppRuleAlgorithm.ExactInvariant;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void nearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = TrackSystem.Structs.AppRuleAlgorithm.Near;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void nearCaseInsensitiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = TrackSystem.Structs.AppRuleAlgorithm.NearInvariant;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private void regularExpressionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ruleset_id = (int)treeView1.SelectedNode.Tag;
			TrackSystem.Structs.AppRule rs = GetRule(ruleset_id);
			rs.MatchAlgorithm = TrackSystem.Structs.AppRuleAlgorithm.Regex;

			ReplaceRuleSetNode(treeView1.SelectedNode, rs);
			ReplaceRuleSetNode(rs, ruleset_id);
		}

		private TrackSystem.Structs.AppRuleSet GetRuleSet(int ruleset_id)
		{
			foreach (var it in rulesets)
				if (it.id == ruleset_id)
					return it.container;

			throw new ArgumentException();
		}

		private TrackSystem.Structs.AppRule GetRule(int ruleset_id)
		{
			foreach (var it in rules)
				if (it.id == ruleset_id)
					return it.container;

			throw new ArgumentException();
		}

		private void ReplaceRuleSetNode(TreeNode selectedNode,
			TrackSystem.Structs.AppRuleSet idcnt)
		{
			selectedNode.Text = idcnt.UniqueId + ", " +
				idcnt.Kind + ", " + idcnt.Priority;
		}

		private void ReplaceRuleSetNode(TrackSystem.Structs.AppRuleSet rs, int ruleset_id)
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
			TrackSystem.Structs.AppRule container)
		{
			selectedNode.Text = container.MatchString + " = " + container.MatchTo + ", " +
				container.MatchAlgorithm;
		}

		private void ReplaceRuleSetNode(TrackSystem.Structs.AppRule rs, int ruleset_id)
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
