namespace timetracker.Entities
{
	public struct AppRulePair
	{
		public string UniqueID { get; set; }

		public string RuleSetID { get; set; }

		public RulePriority Priority { get; set; }

		public AppRulePair(string id, string gid, RulePriority p)
		{
			UniqueID = id;
			RuleSetID = gid;
			Priority = p;
		}
	}
}
