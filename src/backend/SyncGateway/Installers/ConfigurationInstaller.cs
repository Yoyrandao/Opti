using CommonTypes.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SyncGateway.Installers
{
    public static class ConfigurationInstaller
    {
        public static IServiceCollection InstallConfigurationTypes(this IServiceCollection services,
            IConfiguration configuration)
        {
            var databaseConnectionOptions = new DatabaseConnectionOptions();
            var ftpConnectionOptions = new FtpConnectionOptions();

            configuration.Bind("DatabaseConnection", databaseConnectionOptions);
            configuration.Bind("FtpConnection", ftpConnectionOptions);

            services.AddSingleton(databaseConnectionOptions);
            services.AddSingleton(ftpConnectionOptions);

            return services;
        }
    }
}
