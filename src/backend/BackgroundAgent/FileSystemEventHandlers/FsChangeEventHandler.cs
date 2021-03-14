using System.IO;

using CommonTypes.Programmability;

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

            _changeQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _changeQueue;
    }
}