using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsDeleteEventHandler
    {
        void OnDeleted(object sender, FileSystemEventArgs ea);
    }
}