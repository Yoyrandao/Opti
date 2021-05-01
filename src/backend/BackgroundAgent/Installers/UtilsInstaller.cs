using BackgroundAgent.Requests;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Utils.Certificates;
using Utils.Hashing;
using Utils.Http;

namespace BackgroundAgent.Installers
{
    public static class UtilsInstaller
    {
        public static IServiceCollection InstallUtils(this IServiceCollection services)
        {
            services.AddTransient<IRestClientFactoryResolver, RestClientFactoryResolver>();
            services.AddTransient<IRequestFactory, RequestFactory>();

            services.AddTransient<ICertificateProvider, CertificateProvider>();

            services.AddTransient<IHashProvider, Sha256HashProvider>();

            return services;
        }
    }
}