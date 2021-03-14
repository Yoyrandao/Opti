using System.IO;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public interface IFsDeleteEventHandler
    {
        void OnDeleted(object sender, FileSystemEventArgs ea);
    }
}