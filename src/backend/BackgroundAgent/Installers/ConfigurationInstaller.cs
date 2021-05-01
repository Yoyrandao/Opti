using BackgroundAgent.Configuration;

using CommonTypes.Configuration;

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
            var certificateConfiguration = new CertificateConfiguration();
            
            configuration.Bind("Endpoint", endpointConfiguration);
            configuration.Bind("Certificate", certificateConfiguration);
            
            services.AddSingleton(endpointConfiguration);
            services.AddSingleton(certificateConfiguration);

            return services;
        }
    }
}