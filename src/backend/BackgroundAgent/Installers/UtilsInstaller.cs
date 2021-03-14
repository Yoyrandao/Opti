using BackgroundAgent.Configuration;
using BackgroundAgent.Requests;

using Microsoft.Extensions.DependencyInjection;

using Utils.Http;

namespace BackgroundAgent.Installers
{
    public static class UtilsInstaller
    {
        public static IServiceCollection InstallUtils(this IServiceCollection services)
        {
            services.AddTransient<IRestClientFactory, RestClientFactory>(x =>
                new RestClientFactory(x.GetService<EndpointConfiguration>()?.Backend));

            services.AddTransient<IRequestFactory, RequestFactory>();

            return services;
        }
    }
}