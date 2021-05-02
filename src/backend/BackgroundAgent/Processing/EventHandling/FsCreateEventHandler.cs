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
    public class FsCreateEventHandler : IFsCreateEventHandler
    {
        private readonly ILogger _logger = Log.ForContext<FsCreateEventHandler>();
        private readonly CreatedFileOperationTask _task;

        private volatile QueueSet<FsEvent> _createQueue;

        public FsCreateEventHandler(CreatedFileOperationTask task)
        {
            _task = task;
            _createQueue = new QueueSet<FsEvent>();
        }

        private async Task ProcessInternal()
        {
            while (true)
            {
                var file = _createQueue.Check();

                if (file == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    continue;
                }

                _logger.Information($"Processing file creation ({file.FilePath}).");
                // _task.Process(file.FilePath);

                _createQueue.Pop();
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        #region Implementation of IFsCreateEventHandler

        public void OnCreated(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Created)
                return;

            _createQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name, EventTimestamp = DateTime.Now });
        }

        public void Prepare(CancellationToken token)
        {
            Task.Run(ProcessInternal, token);
        }

        #endregion
    }
}