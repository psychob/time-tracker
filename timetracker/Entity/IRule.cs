namespace timetracker.Entity
{
    public interface IRule
    {
        RuleMatchTo GetMatchTo();
        RuleAlgorithm GetAlgorithm();
        string GetString();
    }
}