using System.IO;

using BackgroundAgent.FileSystemEventHandlers;

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

            return services;
        }
    }
}