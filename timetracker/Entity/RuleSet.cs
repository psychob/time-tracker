using System.Collections.Generic;

namespace timetracker.Entity
{
    public interface RuleSet
    {
        RuleSetType GetRuleType();
        RuleSetPriority GetPriority();
        List<Rule> GetRules();
    }
}