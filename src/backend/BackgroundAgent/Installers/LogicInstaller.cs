using System.IO;

using BackgroundAgent.Processing.EventHandling;
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
            InstallProcessors(services);

            services.AddTransient<ISymmentricalCryptoService, SymmetricalCryptoService>();
            services.AddTransient<IAsymmetricalCryptoService, AssymentricalCryptoService>();

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
                    x.GetService<MetaInfoProcessor>(),
                    x.GetService<CompressionCheckProcessor>(),
                    x.GetService<CompressionProcessor>(),
                    x.GetService<SliceProcessor>(),
                    x.GetService<EncryptionProcessor>(),
                    x.GetService<SendDataProcessor>()
                }));
            
            services.AddTransient(
                x => new ChangedFileOperationTask(new BasicProcessor[]
                {
                    x.GetService<MetaInfoProcessor>(),
                    x.GetService<CompressionCheckProcessor>(),
                    x.GetService<CompressionProcessor>(),
                    x.GetService<SliceProcessor>(),
                    x.GetService<DistinctProcessor>(),
                    x.GetService<EncryptionProcessor>(),
                    x.GetService<SendDataProcessor>()
                }));

            return services;
        }

        private static void InstallProcessors(IServiceCollection services)
        {
            services.AddTransient<MetaInfoProcessor>();
            services.AddTransient<CompressionCheckProcessor>();
            services.AddTransient<CompressionProcessor>();
            services.AddTransient<DecompressionProcessor>();
            services.AddTransient<EncryptionProcessor>();
            services.AddTransient<DecryptionProcessor>();
            services.AddTransient<SliceProcessor>();

            services.AddTransient(x => new SendDataProcessor(
                x.GetService<IRequestFactory>(),
                x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway)));
            services.AddTransient(x => new DistinctProcessor(
                x.GetService<IRequestFactory>(),
                x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway)));
        }
    }
}