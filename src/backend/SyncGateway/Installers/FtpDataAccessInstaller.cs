using FtpDataAccess.Factories;
using FtpDataAccess.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace SyncGateway.Installers
{
    public static class FtpDataAccessInstaller
    {
        public static IServiceCollection InstallFtpDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IFtpConnectionFactory, FtpConnectionFactory>();

            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddTransient<IStorageRepository, StorageRepository>();

            return services;
        }
    }
}
