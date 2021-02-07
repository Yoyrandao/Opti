using System.Transactions;

namespace DataAccess.Helpers
{
    public class SqlTransactionScope : ISqlTransactionScope
    {
        private readonly TransactionScope _transactionScope;

        public SqlTransactionScope()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    Timeout = TransactionManager.DefaultTimeout, IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        #region Implementation of ISqlTransactionScope

        public void Dispose()
        {
            _transactionScope.Dispose();
        }

        public void Commit()
        {
            _transactionScope.Complete();
        }

        #endregion
    }
}
