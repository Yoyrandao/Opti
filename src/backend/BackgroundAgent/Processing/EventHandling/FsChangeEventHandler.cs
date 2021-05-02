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
    public class FsChangeEventHandler : IFsChangeEventHandler
    {
        public FsChangeEventHandler(ChangedFileOperationTask task)
        {
            _task = task;
            _changeQueue = new QueueSet<FsEvent>();
        }

        private async Task ProcessInternal()
        {
            while (true)
            {
                var file = _changeQueue.Check();

                if (file == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    continue;
                }

                _logger.Information($"Processing file changing ({file.FilePath}).");
                _task.Process(file.FilePath);

                _changeQueue.Pop();
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        #region Implementation of IFsChangeEventHandler

        public void OnChanged(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Changed)
                return;

            _changeQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name, EventTimestamp = DateTime.Now });
        }

        public void Prepare(CancellationToken token)
        {
            Task.Run(ProcessInternal, token);
        }

        #endregion
        
        private volatile QueueSet<FsEvent> _changeQueue;
        private readonly ChangedFileOperationTask _task;

        private readonly ILogger _logger = Log.ForContext<FsChangeEventHandler>();
    }
}