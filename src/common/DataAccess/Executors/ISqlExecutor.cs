using System.Collections.Generic;

namespace DataAccess.Executors
{
    public interface ISqlExecutor
    {
        IEnumerable<T> List<T>(string query, object @params = null);

        T Get<T>(string query, object @params = null);

        void Execute(string query, object @params = null);
    }
}
