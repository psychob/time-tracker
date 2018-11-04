using System;
using System.IO;
using System.Linq;
using SQLite;
using timetracker4.Entity;
using Version = timetracker4.Entity.Version;

namespace timetracker4.Services
{
    internal class Database: IDisposable
    {
        private SQLiteConnection _connection;

        public void Init()
        {
            if (!IsDatabaseExist())
            {
                CreateDatabase();
            }
            else
            {
                UpdateDatabase();
            }
        }

        private static bool IsDatabaseExist()
        {
            return File.Exists("./tracks.db3");
        }

        private void CreateDatabase()
        {
            _connection = new SQLiteConnection("./tracks.db3", true, null);

            Migrate(1);
        }

        private void UpdateDatabase()
        {
            var last = QueryFirst<Version>("selet * from version order by Id Desc");
        }

        public T QueryFirst<T>(string sql) where T : new()
        {
            return _connection.Query<T>(sql).FirstOrDefault();
        }

        private void Migrate(int which)
        {
            if (which == 1)
            {
                _connection.CreateTables<Application, RuleGroup, Rule, Version>();
                Insert(new Version { Id = 1 });
            }
        }

        public int Insert<T>(T obj)
        {
            return _connection.Insert(obj, typeof(T));
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection = null;
        }
    }
}
