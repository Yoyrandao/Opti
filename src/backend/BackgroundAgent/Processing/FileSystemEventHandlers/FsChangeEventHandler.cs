using System.IO;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public class FsChangeEventHandler : IFsChangeEventHandler
    {
        public FsChangeEventHandler(IChangeEventProcessingService service)
        {
            _service = service;
            _changeQueue = new QueueSet<FsEvent>();
            _changeQueue.OnPush += ProcessInternal;
        }

        #region Implementation of IFsChangeEventHandler

        public void OnChanged(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Changed)
                return;

            _logger.Information($"Processing file changing ({ea.FullPath}).");
            _changeQueue.Push(new FsEvent { FilePath = ea.FullPath, Name = ea.Name });
        }

        #endregion

        private void ProcessInternal()
        {
            var @event = _changeQueue.Pop();

            _service.ApplyChangeEvent(@event);
        }

        private volatile QueueSet<FsEvent> _changeQueue;
        private readonly IChangeEventProcessingService _service;
        
        private readonly ILogger _logger = Log.ForContext<FsChangeEventHandler>();
    }
}
