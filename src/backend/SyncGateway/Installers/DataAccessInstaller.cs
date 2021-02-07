using DataAccess.Executors;
using DataAccess.Helpers;
using DataAccess.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace SyncGateway.Installers
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection InstallDataAccess(this IServiceCollection services)
        {
            services.AddTransient<ISqlExecutor, PgSqlExecutor>();

            services.AddTransient<ISqlTransactionScope, SqlTransactionScope>();
            services.AddTransient<ITransactionFactory, SqlTransactionFactory>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFilePartRepository, FilePartRepository>();

            return services;
        }
    }
}
