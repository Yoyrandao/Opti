namespace DataAccess.Helpers
{
    public class SqlTransactionFactory : ISqlTransactionScope
    {
        #region Implementation of ISqlTransactionScope

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        #endregion
        
    }
}