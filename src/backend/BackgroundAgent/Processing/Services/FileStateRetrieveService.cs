using System.Collections.Generic;

using BackgroundAgent.Contracts;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Requests;

using RestSharp;

using Serilog;

using Utils.Http;

namespace BackgroundAgent.Processing.Services
{
    public class FileStateRetrieveService : IFileStateRetrieveService
    {
        public FileStateRetrieveService(IRestClientFactory restClientFactory, IRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
            _client = restClientFactory.Create();
        }

        #region Implementation of IChangeEventProcessingService

        public ICollection<FileState> ApplyChangeEvent(FsEvent @event)
        {
            _logger.Information($"Retrieving infromation about {@event.Name}");
            
            var request = _requestFactory.CreateGetFileStateRequest(@event.Name);
            var response = _client.Execute<ApiResponse>(request);

            return response.Data.Data as ICollection<FileState>;
        }

        #endregion

        private readonly IRestClient _client;
        private readonly IRequestFactory _requestFactory;

        private readonly ILogger _logger = Log.ForContext<FileStateRetrieveService>();
    }
}