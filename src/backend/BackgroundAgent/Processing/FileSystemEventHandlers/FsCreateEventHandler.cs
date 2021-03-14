using System.IO;

using BackgroundAgent.Processing.Models;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public class FsCreateEventHandler : IFsCreateEventHandler
    {
        public FsCreateEventHandler()
        {
            _createQueue = new QueueSet<FsEvent>();
            _createQueue.OnPush += ProcessInternal;
        }

        #region Implementation of IFsCreateEventHandler

        public void OnCreated(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Created)
                return;

            _logger.Information($"Processing file creation ({ea.FullPath}).");
            _createQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name });
        }

        #endregion

        private static void ProcessInternal() { }

        private volatile QueueSet<FsEvent> _createQueue;
        private readonly ILogger _logger = Log.ForContext<FsCreateEventHandler>();
    }
}