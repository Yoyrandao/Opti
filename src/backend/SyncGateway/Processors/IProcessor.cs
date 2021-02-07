namespace SyncGateway.Processors
{
    public interface IProcessor
    {
        void Process(object contract);
    }
}
