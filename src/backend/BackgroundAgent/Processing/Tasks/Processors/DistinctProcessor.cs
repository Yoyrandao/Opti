using System.Linq;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DistinctProcessor : BasicProcessor
    {
        public DistinctProcessor(IFileStateService fileStateService)
        {
            _fileStateService = fileStateService;
        }

        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            _logger.Information($"Running distinction process for {snapshot.BaseFileName}.");

            var filesStates = _fileStateService.GetStateOf(snapshot.BaseFileName);

            if (filesStates == null || !filesStates.Any())
                return;

            var fileStateMap = filesStates.ToDictionary(fs => fs.PartName, fs => fs);

            snapshot.Parts = snapshot.Parts
               .Where(p => !fileStateMap.ContainsKey(p.PartName)
                           || !fileStateMap[p.PartName].CompressionHash.Equals(p.CompressionHash)).ToList();

            _logger.Information($"Distinction process complete ({snapshot.BaseFileName}).");

            Successor?.Process(snapshot);
        }
        
        private readonly IFileStateService _fileStateService;

        private readonly ILogger _logger = Log.ForContext<DistinctProcessor>();
    }
}