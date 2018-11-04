using System;
using SQLite;

namespace timetracker4.Entity
{
    [Table(nameof(Rule))]
    internal class Rule
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

        [Indexed]
        public Int64 GroupId { get; set; }

        public string Pattern { get; set; }
        public RuleMatchTo MatchTo { get; set; }
        public RuleMatchAlgorithm Algorithm { get; set; }
    }
}
