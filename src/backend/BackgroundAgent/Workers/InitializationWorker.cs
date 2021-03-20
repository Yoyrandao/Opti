using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.FileSystemEventHandlers;

using Microsoft.Extensions.Hosting;

using Serilog;

namespace BackgroundAgent.Workers
{
    public class InitializationWorker : BackgroundService
    {
        public InitializationWorker(
            FileSystemWatcher fsWatcher,
            IFsChangeEventHandler changeEventHandler,
            IFsCreateEventHandler createEventHandler,
            IFsDeleteEventHandler deleteEventHandler)
        {
            _fsWatcher = fsWatcher;
            _changeEventHandler = changeEventHandler;
            _createEventHandler = createEventHandler;
            _deleteEventHandler = deleteEventHandler;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApplicationStructure();
            InitializeKeys();
            SubscribeToFilesystemEvents();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                InitializeApplicationStructure();

                await Task.Delay(10000, stoppingToken);
            }
        }

        private void InitializeApplicationStructure()
        {
            if (Directory.Exists(FsLocation.ApplicationRoot))
                return;

            _logger.Information($"Creating application folder ({FsLocation.ApplicationRoot})");

            Directory.CreateDirectory(FsLocation.ApplicationRoot);
            Directory.CreateDirectory(FsLocation.ApplicationData);
            Directory.CreateDirectory(FsLocation.ApplicationTempData);
        }

        private void SubscribeToFilesystemEvents()
        {
            _fsWatcher.Path = FsLocation.ApplicationData;
            
            // _fsWatcher.Changed += _changeEventHandler.OnChanged;
            _fsWatcher.Created += _createEventHandler.OnCreated;
            // _fsWatcher.Deleted += _deleteEventHandler.OnDeleted;
            
            _fsWatcher.EnableRaisingEvents = true;
            _fsWatcher.IncludeSubdirectories = true;
        }

        private static void InitializeKeys()
        {
            if (File.Exists(FsLocation.ApplicationPrivateKey) && File.Exists(FsLocation.ApplicationPublicKey))
                return;
            
            var cryptoProvider = new RSACryptoServiceProvider();
            
            var privateKey = cryptoProvider.ExportRSAPrivateKey();
            var publicKey = cryptoProvider.ExportRSAPublicKey();
            
            File.WriteAllBytes(FsLocation.ApplicationPrivateKey, privateKey);
            File.WriteAllBytes(FsLocation.ApplicationPublicKey, publicKey);
        }

        private readonly FileSystemWatcher _fsWatcher;
        
        private readonly IFsChangeEventHandler _changeEventHandler;
        private readonly IFsCreateEventHandler _createEventHandler;
        private readonly IFsDeleteEventHandler _deleteEventHandler;
        
        private readonly ILogger _logger = Log.ForContext<InitializationWorker>();
    }
}
