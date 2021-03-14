using System.IO;

using CommonTypes.Programmability;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public class FsDeleteEventHandler : IFsDeleteEventHandler
    {
        public FsDeleteEventHandler()
        {
            _deleteQueue = new QueueSet<string>();
            _deleteQueue.OnPush += ProcessInternal;
        }

        public void OnDeleted(object sender, FileSystemEventArgs ea)
        {
            _deleteQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _deleteQueue;
    }
}