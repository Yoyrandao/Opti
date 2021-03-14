using System.IO;

using BackgroundAgent.Processing.Models;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public class FsDeleteEventHandler : IFsDeleteEventHandler
    {
        public FsDeleteEventHandler()
        {
            _deleteQueue = new QueueSet<FsEvent>();
            _deleteQueue.OnPush += ProcessInternal;
        }

        #region Implementation of IFsDeleteEventHandler

        public void OnDeleted(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Deleted)
                return;

            _logger.Information($"Processing file deletion ({ea.FullPath}).");
            _deleteQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name });
        }

        #endregion

        private static void ProcessInternal() { }

        private volatile QueueSet<FsEvent> _deleteQueue;
        private readonly ILogger _logger = Log.ForContext<FsDeleteEventHandler>();
    }
}