using System.IO;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.Processing.EventHandling
{
    public class FsChangeEventHandler : IFsChangeEventHandler
    {
        public FsChangeEventHandler(ICompressionCheckService service)
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

        private void ProcessInternal() { }

        private volatile QueueSet<FsEvent> _changeQueue;
        private readonly ICompressionCheckService _service;
        
        private readonly ILogger _logger = Log.ForContext<FsChangeEventHandler>();
    }
}
