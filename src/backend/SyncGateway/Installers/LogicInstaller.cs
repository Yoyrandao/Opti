using DataAccess.Repositories;

using FtpDataAccess.Repositories;

using Microsoft.Extensions.DependencyInjection;

using SyncGateway.Exceptions;
using SyncGateway.Processing;
using SyncGateway.Services;

using Utils.Retrying;

namespace SyncGateway.Installers
{
    public static class LogicInstaller
    {
        public static IServiceCollection InstallLogic(this IServiceCollection services)
        {
            services.AddTransient<IFileProcessor, FileProcessor>();

            services.AddTransient<IUserRegistrationService, UserRegistrationService>(x =>
                new UserRegistrationService(new OperationTask(new BasicProcessor[]
                {
                    new UserDatabaseRegistrationProcessor(x.GetService<IUserRepository>(),
                        x.GetService<IRepeater<UserNotInDatabaseException>>()),
                    new UserStorageRegistrationProcessor(x.GetService<IStorageRepository>(),
                        x.GetService<IRepeater<UserFolderNotCreatedException>>())
                })));

            return services;
        }
    }
}
