using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static timetracker.TrackSystem.Structs;

namespace timetracker.Entities
{
	public struct AppRuleSet
	{
		public RuleSet Kind { get; set; }

		public string UniqueId { get; set; }

		public RulePriority Priority { get; set; }

		public AppRule[] Rules { get; set; }
	}
}
