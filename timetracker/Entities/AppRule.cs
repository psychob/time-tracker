
namespace timetracker.Entities
{
	public struct AppRule
	{
		public AppRuleMatchTo MatchTo { get; set; }

		public string MatchString { get; set; }

		public AppRuleAlgorithm MatchAlgorithm { get; set; }

		public AppRule(AppRuleMatchTo mt, string str, AppRuleAlgorithm a)
		{
			MatchTo = mt;
			MatchString = str;
			MatchAlgorithm = a;
		}
	}
}
