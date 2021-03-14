using System.IO;

using BackgroundAgent.Processing.FileSystemEventHandlers;
using BackgroundAgent.Processing.Services;

using Microsoft.Extensions.DependencyInjection;

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

            services.AddTransient<IChangeEventProcessingService, ChangeEventProcessingService>();

            return services;
        }
    }
}