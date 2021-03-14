using System.IO;

using CommonTypes.Programmability;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public class FsCreateEventHandler : IFsCreateEventHandler
    {
        public FsCreateEventHandler()
        {
            _createQueue = new QueueSet<string>();
            _createQueue.OnPush += ProcessInternal;
        }

        public void OnCreated(object sender, FileSystemEventArgs ea)
        {
            _createQueue.Push(ea.FullPath);
        }

        private static void ProcessInternal() { }

        private volatile QueueSet<string> _createQueue;
    }
}