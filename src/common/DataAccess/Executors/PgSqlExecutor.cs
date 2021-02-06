using System;
using System.Collections.Generic;

using Npgsql;

namespace DataAccess
{
    public class PgSqlExecutor : ISqlExecutor
    {
        
        
        public IEnumerable<T> List<T>(string query, object param = null) => throw new System.NotImplementedException();

        public T Get<T>(string query, object param = null) => throw new System.NotImplementedException();

        public void Execute(string query, object param = null)
        {
            throw new System.NotImplementedException();
        }

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