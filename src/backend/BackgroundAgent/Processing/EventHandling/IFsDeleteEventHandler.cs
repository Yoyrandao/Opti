using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsDeleteEventHandler : IEventHandler
    {
        void OnDeleted(object sender, FileSystemEventArgs ea);
    }
}