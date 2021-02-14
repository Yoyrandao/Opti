using FtpDataAccess.Factories;
using FtpDataAccess.Helpers;
using FtpDataAccess.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace SyncGateway.Installers
{
    public static class FtpDataAccessInstaller
    {
        public static IServiceCollection InstallFtpDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IFtpUriBuilder, FtpUriBuilder>();
            services.AddTransient<IFtpWebRequestFactory, FtpWebRequestFactory>();
            services.AddTransient<IFtpClient, FtpClient>();

            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddTransient<IStorageRepository, StorageRepository>();

            return services;
        }
    }
}