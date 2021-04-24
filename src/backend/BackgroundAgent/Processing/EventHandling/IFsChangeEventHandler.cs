using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsChangeEventHandler
    {
        void OnChanged(object sender, FileSystemEventArgs ea);
    }
}