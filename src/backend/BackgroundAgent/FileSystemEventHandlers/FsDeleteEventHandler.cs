using System.IO;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public class FsDeleteEventHandler : IFsDeleteEventHandler
    {
        public FsDeleteEventHandler()
        {
            _deleteQueue = new QueueSet<string>();
            _deleteQueue.OnPush += ProcessInternal;
        }

        public void OnDeleted(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Deleted)
                return;

            _logger.Information($"Processing file deletion ({ea.FullPath}).");
            _deleteQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _deleteQueue;
        private readonly ILogger _logger = Log.ForContext<FsDeleteEventHandler>();
    }
}