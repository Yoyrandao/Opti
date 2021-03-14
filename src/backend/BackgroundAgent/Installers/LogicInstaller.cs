using System.IO;

using BackgroundAgent.Processing.FileSystemEventHandlers;
using BackgroundAgent.Processing.Services;
using BackgroundAgent.Requests;

using Microsoft.Extensions.DependencyInjection;

using Utils.Http;

namespace BackgroundAgent.Installers
{
    public static class LogicInstaller
    {
        public static IServiceCollection InstallLogic(this IServiceCollection services)
        {
            services.AddTransient<IFsChangeEventHandler, FsChangeEventHandler>();
            services.AddTransient<IFsCreateEventHandler, FsCreateEventHandler>();
            services.AddTransient<IFsDeleteEventHandler, FsDeleteEventHandler>();

            services.AddSingleton<FileSystemWatcher>();

            services.AddTransient<IFileStateRetrieveService, FileStateRetrieveService>(
                x => new FileStateRetrieveService(
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway),
                    x.GetService<IRequestFactory>()));
            services.AddTransient<ICompressionCheckService, CompressionCheckService>(
                x => new CompressionCheckService(
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.CompressionChecker),
                    x.GetService<IRequestFactory>()));

            return services;
        }
    }
}