using System.IO;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public interface IFsChangeEventHandler
    {
        void OnChanged(object sender, FileSystemEventArgs ea);
    }
}