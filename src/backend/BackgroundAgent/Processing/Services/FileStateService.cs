using System.Collections.Generic;

using BackgroundAgent.Requests;

using CommonTypes.Contracts;

using RestSharp;

using Utils.Http;
using Utils.Serialization;

namespace BackgroundAgent.Processing.Services
{
    public class FileStateService : IFileStateService
    {
        public FileStateService(IRequestFactory requestFactory, IRestClientFactory factory, ISerializer serializer)
        {
            _client = factory.Create();
            _requestFactory = requestFactory;
            _serializer = serializer;
        }

        #region Implementation of IFileStateService

        public List<FileState> GetStateOf(string filename)
        {
            var request = _requestFactory.CreateGetFileStateRequest(filename);
            var responseData = _client.Execute<ApiResponse>(request).Data;

            return responseData?.Error != null
                ? null
                : _serializer.Deserialize<List<FileState>>(responseData?.Data?.ToString());
        }

        #endregion

        private readonly IRequestFactory _requestFactory;
        private readonly IRestClient _client;

        private readonly ISerializer _serializer;
    }
}