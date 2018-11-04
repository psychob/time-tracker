using System;
using SQLite;

namespace timetracker4.Entity
{
    [Table(nameof(RuleGroup))]
    internal class RuleGroup
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

        [Indexed]
        public Int64 AppId { get; set; }

        [Unique]
        public string Unique { get; set; }

        public RuleGroupType Type { get; set; }
        public int Priority { get; set; }
    }
}
