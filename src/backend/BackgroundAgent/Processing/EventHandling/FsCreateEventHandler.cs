using System.IO;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.EventHandling
{
    public class FsCreateEventHandler : IFsCreateEventHandler
    {
        public FsCreateEventHandler(DeletedFileOperationTask task)
        {
            _task = task;
            _createQueue = new QueueSet<FsEvent>();
            _createQueue.OnPush += ProcessInternal;
        }

        #region Implementation of IFsCreateEventHandler

        public void OnCreated(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Created)
                return;

            _createQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name });
        }

        #endregion

        private void ProcessInternal()
        {
            var file = _createQueue.Pop();

            if (file == null)
                return;
            
            _logger.Information($"Processing file creation ({file.FilePath}).");
            _task.Process(file.FilePath);
        }

        private volatile QueueSet<FsEvent> _createQueue;
        private readonly DeletedFileOperationTask _task;

        private readonly ILogger _logger = Log.ForContext<FsCreateEventHandler>();
    }
}