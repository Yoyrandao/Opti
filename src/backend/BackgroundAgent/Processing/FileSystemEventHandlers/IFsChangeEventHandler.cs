using System.IO;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public interface IFsChangeEventHandler
    {
        void OnChanged(object sender, FileSystemEventArgs ea);
    }
}