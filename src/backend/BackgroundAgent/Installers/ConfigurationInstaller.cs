using BackgroundAgent.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundAgent.Installers
{
    public static class ConfigurationInstaller
    {
        public static IServiceCollection InstallConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            var endpointConfiguration = new EndpointConfiguration();
            
            configuration.Bind("Endpoint", endpointConfiguration);
            services.AddSingleton(endpointConfiguration);

            return services;
        }
    }
}