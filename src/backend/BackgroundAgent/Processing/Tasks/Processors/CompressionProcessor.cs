using BackgroundAgent.Processing.Models;

using EnsureThat;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class CompressionProcessor: BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            if (!snapshot.Compressed)
            {
                Successor?.Process(null);
            }
            
            //TODO: NEXT SERVICE HERE
        }
    }
}