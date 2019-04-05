using System.Collections.Generic;

namespace timetracker.Entity
{
    public interface IApplication
    {
        string GetUniqueId();
        string GetName();
        List<IRuleSet> GetRules();
        bool GetReportOnlyOne();
        bool GetCountGroupAsOne();
    }
}
