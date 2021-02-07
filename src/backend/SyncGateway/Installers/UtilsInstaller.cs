using System;

using Microsoft.Extensions.DependencyInjection;

using SyncGateway.Contracts.Out;
using SyncGateway.Exceptions;
using SyncGateway.Exceptions.Shields;

using Utils.Retrying;

// ReSharper disable BadParensLineBreaks

namespace SyncGateway.Installers
{
    public static class UtilsInstaller
    {
        public static IServiceCollection InstallUtils(this IServiceCollection services)
        {
            var retryIntervals = new[] { TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5) };

            services.AddSingleton(
                RepeaterFactory.Create<UserNotInDatabaseException>(
                    PolicyFactory.Create<UserNotInDatabaseException>(retryIntervals)));

            services.AddSingleton(
                RepeaterFactory.Create<UserFolderNotCreatedException>(
                    PolicyFactory.Create<UserFolderNotCreatedException>(retryIntervals)));

            return services;
        }

        public static IServiceCollection InstallShields(this IServiceCollection services)
        {
            services.AddTransient<IExceptionShield<ApiResponse>, ApiExceptionShield>();

            return services;
        }
    }
}
