using System;
using System.Security.Cryptography.X509Certificates;

using BackgroundAgent.Configuration;
using BackgroundAgent.Requests;

using CommonTypes.Configuration;

using Utils.Certificates;

namespace Utils.Http
{
    public class RestClientFactoryResolver : IRestClientFactoryResolver
    {
        public RestClientFactoryResolver(
            EndpointConfiguration endpointConfiguration,
            ICertificateProvider certificateProvider,
            CertificateConfiguration certificateConfiguration)
        {
            _endpointConfiguration = endpointConfiguration;
            _certificateProvider = certificateProvider;
            _certificateConfiguration = certificateConfiguration;
        }

        public IRestClientFactory Resolve(Endpoint endpoint)
        {
            X509Certificate2 certificate = null;
            if (_certificateConfiguration != null)
            {
                certificate = _certificateProvider.GetCertificate(new CertificateSearch(_certificateConfiguration));
            }
            
            return endpoint switch
            {
                Endpoint.SyncGateway        => new RestClientFactory(_endpointConfiguration.Backend, certificate),
                Endpoint.CompressionChecker => new RestClientFactory(_endpointConfiguration.CompressionChecker, certificate),
                _                           => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }

        private readonly EndpointConfiguration _endpointConfiguration;
        private readonly ICertificateProvider _certificateProvider;
        private readonly CertificateConfiguration _certificateConfiguration;
    }
}