using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.EventHandling
{
    public class FsDeleteEventHandler : IFsDeleteEventHandler
    {
        private readonly ILogger _logger = Log.ForContext<FsDeleteEventHandler>();
        private readonly DeletedFileOperationTask _task;

        private volatile QueueSet<FsEvent> _deleteQueue;

        public FsDeleteEventHandler(DeletedFileOperationTask task)
        {
            _task = task;
            _deleteQueue = new QueueSet<FsEvent>();
        }

        private async Task ProcessInternal()
        {
            while (true)
            {
                var file = _deleteQueue.Check();

                if (file == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    continue;
                }

                _logger.Information($"Processing file deletion ({file.FilePath}).");
                // _task.Process(new DeletionInfo { Filename = file.Name, Identity = User.TempIdentity });

                _deleteQueue.Pop();
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        #region Implementation of IFsDeleteEventHandler

        public void OnDeleted(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Deleted)
                return;

            _logger.Information($"Processing file deletion ({ea.FullPath}).");
            _deleteQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name, EventTimestamp = DateTime.Now });
        }

        public void Prepare(CancellationToken token)
        {
            Task.Run(ProcessInternal, token);
        }

        #endregion
    }
}