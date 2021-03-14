using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
            var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var applicationDirectory = Path.Combine(homeDirectory, ".opti");
            var applicationDataDirectory = Path.Combine(applicationDirectory, "data");

            InitializeApplicationStructure(applicationDirectory);
            SubscribeToFilesystemEvents(applicationDataDirectory);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                InitializeApplicationStructure(applicationDirectory);

                await Task.Delay(10000, stoppingToken);
            }
        }

        private void InitializeApplicationStructure(string applicationRoot)
        {
            if (Directory.Exists(applicationRoot))
                return;

            _logger.Information($"Creating application folder ({applicationRoot})");

            Directory.CreateDirectory(applicationRoot);
            Directory.CreateDirectory(Path.Combine(applicationRoot, "data"));
        }

        private void SubscribeToFilesystemEvents(string applicationData)
        {
            _fsWatcher.Path = applicationData;
            
            _fsWatcher.Changed += _changeEventHandler.OnChanged;
            _fsWatcher.Created += _createEventHandler.OnCreated;
            _fsWatcher.Deleted += _deleteEventHandler.OnDeleted;
            
            _fsWatcher.EnableRaisingEvents = true;
            _fsWatcher.IncludeSubdirectories = true;
        }

        private readonly FileSystemWatcher _fsWatcher;
        
        private readonly IFsChangeEventHandler _changeEventHandler;
        private readonly IFsCreateEventHandler _createEventHandler;
        private readonly IFsDeleteEventHandler _deleteEventHandler;
        
        private readonly ILogger _logger = Log.ForContext<InitializationWorker>();
    }
}
