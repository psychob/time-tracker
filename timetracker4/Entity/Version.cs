using System;
using SQLite;

namespace timetracker4.Entity
{
    [Table(nameof(Version))]
    internal class Version
    {
        [PrimaryKey]
        public Int64 Id { get; set; }
    }
}
