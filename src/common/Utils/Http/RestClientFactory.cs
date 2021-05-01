using System;
using System.Security.Cryptography.X509Certificates;

using RestSharp;

namespace Utils.Http
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClientFactory(string backendUrl)
        {
            _apiBaseUrl = backendUrl;
        }

        public RestClientFactory(string backendUrl, X509Certificate2 certificate)
        {
            _apiBaseUrl = backendUrl;
            _certificate = certificate;
        }

        #region Implementation of IRestClientFactory

        public IRestClient Create()
        {
            return new RestClient
            {
                BaseUrl = new Uri(_apiBaseUrl),
                ClientCertificates = _certificate != null ? new X509Certificate2Collection(_certificate) : default
            };
        }

        #endregion

        private readonly string _apiBaseUrl;
        private readonly X509Certificate2 _certificate;
    }
}