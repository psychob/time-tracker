using System;
using SQLite;

namespace timetracker4.Entity
{
    [Table(nameof(Event))]
    internal class Event
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

        [Indexed]
        public DateTime When { get; set; }

        [Indexed]
        public string Type { get; set; }

        public string MetaData { get; set; }
    }
}
