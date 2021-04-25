using System.IO;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.EventHandling
{
    public class FsChangeEventHandler : IFsChangeEventHandler
    {
        public FsChangeEventHandler(ChangedFileOperationTask task)
        {
            _task = task;
            _changeQueue = new QueueSet<FsEvent>();
            _changeQueue.OnPush += ProcessInternal;
        }

        #region Implementation of IFsChangeEventHandler

        public void OnChanged(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Changed)
                return;

            _changeQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name });
        }

        #endregion

        private void ProcessInternal()
        {
            var file = _changeQueue.Pop();

            if (file == null)
                return;
            
            _logger.Information($"Processing file changing ({file.FilePath}).");
            _task.Process(file.FilePath);
        }

        private volatile QueueSet<FsEvent> _changeQueue;
        private readonly ChangedFileOperationTask _task;

        private readonly ILogger _logger = Log.ForContext<FsChangeEventHandler>();
    }
}
