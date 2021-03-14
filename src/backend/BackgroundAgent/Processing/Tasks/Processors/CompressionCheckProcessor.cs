using BackgroundAgent.Contracts;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class CompressionCheckProcessor : BasicProcessor
    {
        public CompressionCheckProcessor(ICompressionCheckService compressionCheckService)
        {
            _compressionCheckService = compressionCheckService;
        }

        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var decision = _compressionCheckService.Check(snapshot.MetaInfo);
            snapshot.Compressed = decision;
            
            Successor?.Process(snapshot);
        }

        private readonly ICompressionCheckService _compressionCheckService;
    }
}