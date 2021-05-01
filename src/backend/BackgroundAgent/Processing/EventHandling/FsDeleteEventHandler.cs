using System.IO;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.EventHandling
{
    public class FsDeleteEventHandler : IFsDeleteEventHandler
    {
        public FsDeleteEventHandler(DeletedFileOperationTask task)
        {
            _task = task;
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

        private void ProcessInternal()
        {
            var file = _deleteQueue.Pop();

            if (file == null)
                return;
            
            _logger.Information($"Processing file deletion ({file.FilePath}).");
            _task.Process(new DeletionInfo { Identity = User.TempIdentity, Filename = file.FilePath });
        }

        private volatile QueueSet<FsEvent> _deleteQueue;
        private readonly DeletedFileOperationTask _task;

        private readonly ILogger _logger = Log.ForContext<FsDeleteEventHandler>();
    }
}