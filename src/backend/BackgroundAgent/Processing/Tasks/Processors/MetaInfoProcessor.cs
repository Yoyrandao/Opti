using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class MetaInfoProcessor : BasicProcessor
    {
        public MetaInfoProcessor(IMetaInfoGatherService infoGatherService)
        {
            _infoGatherService = infoGatherService;
        }

        public override void Process(object contract)
        {
            var path = contract as string;
            EnsureArg.IsNotNull(path);

            _logger.Information($"Running info gathering process for {path}.");

            var fileMetaInfo = _infoGatherService.Gather(path);

            var snapshot = new FileSnapshot
            {
                Path = path,
                MetaInfo = fileMetaInfo,
                BaseFileName = fileMetaInfo.FileName
            };
            
            _logger.Information($"Info gathering process complete ({path}).");

            Successor?.Process(snapshot);
        }

        private readonly IMetaInfoGatherService _infoGatherService;

        private readonly ILogger _logger = Log.ForContext<MetaInfoGatherService>();
    }
}