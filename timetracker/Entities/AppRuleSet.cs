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
