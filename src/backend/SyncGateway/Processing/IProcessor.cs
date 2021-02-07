namespace SyncGateway.Processing
{
    public abstract class BasicProcessor
    {
        public void SetSuccessor(BasicProcessor successor)
        {
            Successor = successor;
        }

        public abstract void Process(object contract);

        protected BasicProcessor Successor;
    }
}
