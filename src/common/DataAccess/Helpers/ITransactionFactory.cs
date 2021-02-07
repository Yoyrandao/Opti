namespace DataAccess.Helpers
{
    public interface ITransactionFactory
    {
        ISqlTransactionScope BeginTransaction();
    }
}
