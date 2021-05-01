using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Requests;

using CommonTypes.Contracts;

using EnsureThat;

using RestSharp;

using Serilog;

using Utils.Http;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class SendDeleteProcessor : BasicProcessor
    {
        public SendDeleteProcessor(IRequestFactory requestFactory, IRestClientFactory clientFactory)
        {
            _requestFactory = requestFactory;
            _client = clientFactory.Create();
        }
        
        public override void Process(object contract)
        {
            var snapshot = contract as DeletionInfo;
            EnsureArg.IsNotNull(snapshot);
            
            _logger.Information($"Running deleting data process for {snapshot.Filename}.");

            var deleteSet = new DeleteSet
            {
                Identity = User.TempIdentity,
                Filename = snapshot.Filename
            };

            var request = _requestFactory.CreateDeleteSetSendingRequest(deleteSet);
            _client.Execute(request);
            
            _logger.Information($"Sending deletion process complete ({snapshot.Filename}).");

            Successor?.Process(snapshot);
        }
        
        private readonly IRestClient _client;
        private readonly IRequestFactory _requestFactory;

        private ILogger _logger = Log.ForContext<SendDataProcessor>();
    }
}