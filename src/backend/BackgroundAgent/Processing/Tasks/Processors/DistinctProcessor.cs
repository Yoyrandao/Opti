using System.Collections.Generic;
using System.Linq;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;
using BackgroundAgent.Requests;

using CommonTypes.Contracts;

using EnsureThat;

using RestSharp;

using Utils.Http;

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

            var fileStateMap = _fileStateService.GetStateOf(snapshot.BaseFileName)
               .ToDictionary(fs => fs.PartName, fs => fs);

            snapshot.Parts = snapshot.Parts
               .Where(p => !fileStateMap.ContainsKey(p.PartName)
                           || !fileStateMap[p.PartName].CompressionHash.Equals(p.CompressionHash)).ToList();
            
            Successor?.Process(snapshot);
        }

        private readonly IFileStateService _fileStateService;
    }
}