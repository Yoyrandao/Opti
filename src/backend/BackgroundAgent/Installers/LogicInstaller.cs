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
using Utils.Serialization;

namespace BackgroundAgent.Installers
{
    public static class LogicInstaller
    {
        public static IServiceCollection InstallLogic(this IServiceCollection services)
        {
            InstallProcessors(services);

            services.AddTransient<ISerializer, JsonSerializer>();

            services.AddTransient<ISymmentricalCryptoService, SymmetricalCryptoService>();
            services.AddTransient<IAsymmetricalCryptoService, AssymentricalCryptoService>();

            services.AddSingleton<IFsChangeEventHandler, FsChangeEventHandler>();
            services.AddSingleton<IFsCreateEventHandler, FsCreateEventHandler>();
            services.AddSingleton<IFsDeleteEventHandler, FsDeleteEventHandler>();

            services.AddTransient<IFileEntropyCalculator, FileEntropyCalculator>();
            services.AddTransient<IFileMetaInformationProvider, FileMetaInformationProvider>();

            services.AddSingleton<FileSystemWatcher>();

            services.AddTransient<IMetaInfoGatherService, MetaInfoGatherService>();

            services.AddTransient<IFileStateService, FileStateService>(
                x => new FileStateService(
                    x.GetService<IRequestFactory>(),
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway),
                    x.GetService<ISerializer>()));

            services.AddTransient<ICompressionCheckService, CompressionCheckService>(
                x => new CompressionCheckService(
                    x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.CompressionChecker),
                    x.GetService<IRequestFactory>()));

            services.AddTransient(
                x => new CreatedFileOperationTask(new BasicProcessor[]
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

            services.AddTransient(
                x => new DeletedFileOperationTask(new BasicProcessor[]
                {
                    x.GetService<SendDeleteProcessor>()
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
            services.AddTransient<DistinctProcessor>();
            services.AddTransient<SliceProcessor>();

            services.AddTransient(x => new SendDataProcessor(
                x.GetService<IRequestFactory>(),
                x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway)));

            services.AddTransient(x => new SendDeleteProcessor(
                x.GetService<IRequestFactory>(),
                x.GetService<IRestClientFactoryResolver>()?.Resolve(Endpoint.SyncGateway)));
        }
    }
}