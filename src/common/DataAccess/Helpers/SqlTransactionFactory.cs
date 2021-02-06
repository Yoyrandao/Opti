namespace DataAccess.Helpers
{
    public class SqlTransactionFactory : ITransactionFactory
    {
        #region Implementation of ITransactionFactory

        public ISqlTransactionScope BeginTransaction()
        {
            return new SqlTransactionScope();
        }

        #endregion
    }
}