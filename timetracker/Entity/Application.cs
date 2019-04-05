using System.Collections.Generic;

namespace timetracker.Entity
{
    public interface Application
    {
        string GetUniqueId();
        string GetName();
        List<RuleSet> GetRules();
        bool GetReportOnlyOne();
        bool GetCountGroupAsOne();
    }
}
