namespace timetracker.Entity
{
    public interface Rule
    {
        RuleMatchTo GetMatchTo();
        RuleAlgorithm GetAlgorithm();
        string GetString();
    }
}