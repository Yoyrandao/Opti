using System.IO;

using BackgroundAgent.Processing.FileSystemEventHandlers;
using BackgroundAgent.Processing.Logic;
using BackgroundAgent.Processing.Logic.Calculators;
using BackgroundAgent.Processing.Services;
using BackgroundAgent.Processing.Tasks;
using BackgroundAgent.Processing.Tasks.Processors;
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

            services.AddTransient<IFileEntropyCalculator, FileEntropyCalculator>();
            services.AddTransient<IFileMetaInformationProvider, FileMetaInformationProvider>();

            services.AddSingleton<FileSystemWatcher>();

            services.AddTransient<IMetaInfoGatherService, MetaInfoGatherService>();

            services.AddTransient<IFileStateRetrieveService, FileStateRetrieveService>(
                x => new FileStateRetrieveService(
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway),
                    x.GetService<IRequestFactory>()));

            services.AddTransient<ICompressionCheckService, CompressionCheckService>(
                x => new CompressionCheckService(
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.CompressionChecker),
                    x.GetService<IRequestFactory>()));

            services.AddTransient(
                x => new NewFileOperationTask(new BasicProcessor[]
                {
                    new MetaInfoProcessor(x.GetService<IMetaInfoGatherService>()),
                    new CompressionCheckProcessor(x.GetService<ICompressionCheckService>()),
                    new CompressionProcessor()
                }));

            return services;
        }
    }
}