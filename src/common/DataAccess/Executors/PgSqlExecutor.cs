using System;
using System.Collections.Generic;
using System.Linq;

using CommonTypes.Configuration;
using CommonTypes.Extensions;

using Dapper;

using Npgsql;

namespace DataAccess.Executors
{
    public class PgSqlExecutor : ISqlExecutor
    {
        public PgSqlExecutor(DatabaseConnectionSettings connectionSettings)
        {
            _connectionString = connectionSettings.Build();
        }

        #region Implementation of ISqlTransactionScope

        public IEnumerable<T> List<T>(string query, object @params = null)
        {
            return Process(connection => connection.Query<T>(query, @params));
        }

        public T Get<T>(string query, object @params = null)
        {
            return Process(connection => connection.Query<T>(query, @params))
               .SingleOrDefault();
        }

        public void Execute(string query, object @params = null)
        {
            Process(connecton => connecton.Query(query, @params));
        }

        #endregion
        
        private T Process<T>(Func<NpgsqlConnection, T> func)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                return func(connection);
            }
        }

        private readonly string _connectionString;
    }
}