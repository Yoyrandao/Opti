using System.IO;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public class FsCreateEventHandler : IFsCreateEventHandler
    {
        public FsCreateEventHandler()
        {
            _createQueue = new QueueSet<string>();
            _createQueue.OnPush += ProcessInternal;
        }

        public void OnCreated(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Created)
                return;

            _logger.Information($"Processing file creation ({ea.FullPath}).");
            _createQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _createQueue;
        private readonly ILogger _logger = Log.ForContext<FsCreateEventHandler>();
    }
}