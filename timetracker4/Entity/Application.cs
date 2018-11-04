using System;
using SQLite;

namespace timetracker4.Entity
{
    [Table(nameof(Application))]
    internal class Application
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

        [Unique]
        public string UniqueId { get; set; }
        public string Name { get; set; }

        public bool AllowOnlyOne { get; set; }
        public bool OnlyCountMaster { get; set; }
    }
}
