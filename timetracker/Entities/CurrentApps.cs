using static timetracker.TrackSystem.Structs;

namespace timetracker.Entities
{
	public struct CurrentApps
	{
		public int PID;
		public string UniqueId;
		public AppRulePair RuleTriggered;
		public ulong StartTime;
		public ulong AllTime;
		public ulong StartCount;
		public App App;
		public bool Merged;
	}
}
