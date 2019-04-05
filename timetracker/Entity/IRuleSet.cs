using System.Collections.Generic;

namespace timetracker.Entity
{
    public interface IRuleSet
    {
        RuleSetType GetRuleType();
        RuleSetPriority GetPriority();
        List<IRule> GetRules();
    }
}