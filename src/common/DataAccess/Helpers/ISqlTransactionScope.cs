using System;

namespace DataAccess.Helpers
{
    public interface ISqlTransactionScope : IDisposable
    {
        void Commit();
    }
}