using static timetracker.TrackSystem.Structs;

namespace timetracker.Entities
{
	public struct AppRule
	{
		public AppRuleMatchTo MatchTo;
		public string MatchString;
		public AppRuleAlgorithm MatchAlgorithm;

		public AppRule(AppRuleMatchTo mt, string str, AppRuleAlgorithm a)
		{
			MatchTo = mt;
			MatchString = str;
			MatchAlgorithm = a;
		}
	}
}
