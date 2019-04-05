﻿using System;
using System.IO;
using System.Linq;
using log4net;
using Newtonsoft.Json;
using Ninject.Extensions.Logging;
using SQLite;
using timetracker4.Entity;
using timetracker4.Exceptions;
using Version = timetracker4.Entity.Version;

namespace timetracker4.Services
{
    internal class Database : IDisposable, IDatabase
    {
        private SQLiteConnection _connection;
        protected ILogger Logger { get; set; }
        private const int LastMigration = 3;

        public Database(ILogger logger)
        {
            Logger = logger;

            Init();
        }

        private void Init()
        {
            Logger.Debug("Initialize database");

            if (!IsDatabaseExist())
            {
                CreateDatabase();
            }
            else
            {
                OpenDatabase();
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

            Migrate(LastMigration, 0);
        }

        private void OpenDatabase()
        {
            _connection = new SQLiteConnection("./tracks.db3", true, null);
        }

        private void UpdateDatabase()
        {
            var last = QueryFirst<Version>("select * from Version order by Id Desc");

            if (last == null)
            {
                // This should not happen, since when database is created we initialize it with correct
                // tables
                throw new IncorrectStateException();
            }

            if (last.Id < LastMigration)
            {
                Migrate(LastMigration, last.Id);
            }
        }

        public T QueryFirst<T>(string sql) where T : new()
        {
            return _connection.Query<T>(sql).FirstOrDefault();
        }

        private void Migrate(int which, Int64 currentMigration)
        {
            if (which >= 1 && currentMigration < 1)
            {
                _connection.CreateTables<Application, RuleGroup, Rule, Version>();
                Insert(new Version { Id = 1 });
            }

            if (which >= 2 && currentMigration < 2)
            {
                _connection.CreateTable<Event>();
                Insert(new Version { Id = 2 });
            }

            if (which >= 3 && currentMigration < 3)
            {
                _connection.CreateTable<Application>();
                Insert(new Version { Id = 3 });
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

        public void NewEvent<T>(T obj, DateTime? when = null)
        {
            var eva = new Event
            {
                Type = typeof(T).FullName,
                When = when ?? new DateTime(),
                MetaData = JsonConvert.SerializeObject(obj)
            };

            _connection.Insert(eva);
        }
    }
}
