using System.Collections.Generic;
using System.Linq;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Requests;

using CommonTypes.Contracts;

using EnsureThat;

using RestSharp;

using Utils.Http;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DistinctProcessor : BasicProcessor
    {
        public DistinctProcessor(IRequestFactory requestFactory, IRestClientFactory factory)
        {
            _client = factory.Create();
            _requestFactory = requestFactory;
        }

        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var request = _requestFactory.CreateGetFileStateRequest(snapshot.BaseFileName);

            var fileStateMap = _client
               .Execute<IEnumerable<FileState>>(request).Data
               .ToDictionary(fs => fs.PartName, fs => fs);

            snapshot.Parts = snapshot.Parts
               .Where(p => !fileStateMap.ContainsKey(p.PartName)
                           || !fileStateMap[p.PartName].CompressionHash.Equals(p.CompressionHash)).ToList();
            
            Successor?.Process(snapshot);
        }

        private readonly IRequestFactory _requestFactory;
        private readonly IRestClient _client;
    }
}