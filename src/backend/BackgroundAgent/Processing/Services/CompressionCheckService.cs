using BackgroundAgent.Contracts;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Requests;

using RestSharp;

using Serilog;

using Utils.Http;

namespace BackgroundAgent.Processing.Services
{
    public class CompressionCheckService : ICompressionCheckService
    {
        public CompressionCheckService(IRestClientFactory restClientFactory, IRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
            _client = restClientFactory.Create();
        }
        
        public bool Check(FileMetaInfo metaInfo)
        {
            _logger.Information($"Retrivieng compression decision about {metaInfo.FileName}");
            
            var request = _requestFactory.CreateCheckCompressionRequest(metaInfo);
            var response = _client.Execute<CompressionDecision>(request);

            return response.Data.Decision;
        }

        private readonly IRestClient _client;
        private readonly IRequestFactory _requestFactory;

        private readonly ILogger _logger = Log.ForContext<CompressionCheckService>();
    }
}