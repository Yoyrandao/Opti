using System.Transactions;

namespace DataAccess.Helpers
{
    public class SqlTransactionScope : ISqlTransactionScope
    {
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
            _transactionScope.Dispose();
        }

        #endregion
        
        private readonly TransactionScope _transactionScope;
    }
}
