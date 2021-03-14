using System;

using RestSharp;

namespace Utils.Http
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClientFactory(string backendUrl)
        {
            _apiBaseUrl = backendUrl;
        }

        #region Implementation of IRestClientFactory

        public IRestClient Create()
        {
            return new RestClient
            {
                BaseUrl = new Uri(_apiBaseUrl)
            };
        }

        #endregion

        private readonly string _apiBaseUrl;
    }
}