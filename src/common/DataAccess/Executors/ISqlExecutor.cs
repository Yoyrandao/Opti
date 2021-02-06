using System.Collections.Generic;

namespace DataAccess
{
    public interface ISqlExecutor
    {
        IEnumerable<T> List<T>(string query, object param = null);

        T Get<T>(string query, object param = null);

        void Execute(string query, object param = null);
    }
}