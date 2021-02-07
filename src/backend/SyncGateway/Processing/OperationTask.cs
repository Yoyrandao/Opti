using System.Linq;

namespace SyncGateway.Processing
{
    public class OperationTask
    {
        public OperationTask(BasicProcessor[] processors)
        {
            _processors = processors;

            for (var i = 0; i < _processors.Length - 1; i++)
            {
                _processors[i].SetSuccessor(_processors[i + 1]);
            }
        }

        public void Process(object contract)
        {
            _processors.First().Process(contract);
        }

        private readonly BasicProcessor[] _processors;
    }
}
