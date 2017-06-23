namespace timetracker.Entities
{
	public struct AppRulePair
	{
		public string UniqueID;
		public string RuleSetID;
		public RulePriority Priority;

		public AppRulePair(string id, string gid, RulePriority p)
		{
			UniqueID = id;
			RuleSetID = gid;
			Priority = p;
		}
	}
}
