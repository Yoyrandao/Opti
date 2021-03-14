using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using BackgroundAgent.FileSystemEventHandlers;

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
            var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var applicationDirectory = Path.Combine(homeDirectory, ".opti");

            SubscribeToFilesystemEvents(applicationDirectory);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!Directory.Exists(applicationDirectory))
                {
                    _logger.Information($"Creating application folder ({applicationDirectory})");

                    Directory.CreateDirectory(applicationDirectory);
                    InitializeApplicationStructure(applicationDirectory);
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        private static void InitializeApplicationStructure(string applicationRoot)
        {
            Directory.CreateDirectory(Path.Combine(applicationRoot, "data"));

            // and more
        }

        private void SubscribeToFilesystemEvents(string applicationRoot)
        {
            _fsWatcher.Path = applicationRoot;
            
            _fsWatcher.Changed += _changeEventHandler.OnChanged;
            _fsWatcher.Created += _createEventHandler.OnCreated;
            _fsWatcher.Deleted += _deleteEventHandler.OnDeleted;
            
            _fsWatcher.IncludeSubdirectories = true;
            _fsWatcher.EnableRaisingEvents = true;
        }

        private readonly FileSystemWatcher _fsWatcher;
        
        private readonly IFsChangeEventHandler _changeEventHandler;
        private readonly IFsCreateEventHandler _createEventHandler;
        private readonly IFsDeleteEventHandler _deleteEventHandler;
        
        private readonly ILogger _logger = Log.ForContext<InitializationWorker>();
    }
}
