using System;

using BackgroundAgent.Configuration;
using BackgroundAgent.Requests;

namespace Utils.Http
{
    public class RestClientFactoryResolver : IRestClientFactoryResolver
    {
        public RestClientFactoryResolver(EndpointConfiguration endpointConfiguration)
        {
            _endpointConfiguration = endpointConfiguration;
        }

        public IRestClientFactory Resolve(Endpoint endpoint)
        {
            return endpoint switch
            {
                Endpoint.SyncGateway        => new RestClientFactory(_endpointConfiguration.Backend),
                Endpoint.CompressionChecker => new RestClientFactory(_endpointConfiguration.CompressionChecker),
                _                           => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }

        private readonly EndpointConfiguration _endpointConfiguration;
    }
}