using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

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

            _logger.Information($"Running compression check process for {snapshot.BaseFileName}.");

            var decision = _compressionCheckService.Check(snapshot.MetaInfo);
            snapshot.Compressed = decision;

            _logger.Information($"Сompression check process completed. ({snapshot.BaseFileName}).");

            Successor?.Process(snapshot);
        }

        private readonly ICompressionCheckService _compressionCheckService;

        private readonly ILogger _logger = Log.ForContext<CompressionCheckService>();
    }
}