namespace timetracker.Entities
{
	public struct CurrentApps
	{
		public int PID { get; set; }

		public string UniqueId { get; set; }

		public AppRulePair RuleTriggered { get; set; }

		public ulong StartTime { get; set; }

		public ulong AllTime { get; set; }

		public ulong StartCount { get; set; }

		public App App { get; set; }

		public bool Merged { get; set; }
	}
}
