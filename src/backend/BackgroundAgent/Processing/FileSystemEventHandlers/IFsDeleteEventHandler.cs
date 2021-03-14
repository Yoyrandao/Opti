using System.IO;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public interface IFsDeleteEventHandler
    {
        void OnDeleted(object sender, FileSystemEventArgs ea);
    }
}