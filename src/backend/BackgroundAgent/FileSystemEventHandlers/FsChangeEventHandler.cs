using System.IO;

using CommonTypes.Programmability;

using Serilog;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public class FsChangeEventHandler : IFsChangeEventHandler
    {
        public FsChangeEventHandler()
        {
            _changeQueue = new QueueSet<string>();
            _changeQueue.OnPush += ProcessInternal;
        }

        public void OnChanged(object sender, FileSystemEventArgs ea)
        {
            if (ea.ChangeType != WatcherChangeTypes.Changed)
                return;

            _logger.Information($"Processing file changing ({ea.FullPath}).");
            _changeQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _changeQueue;
        private readonly ILogger _logger = Log.ForContext<FsChangeEventHandler>();
    }
}