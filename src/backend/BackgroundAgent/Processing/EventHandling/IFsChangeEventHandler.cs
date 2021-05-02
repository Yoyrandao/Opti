using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsChangeEventHandler : IEventHandler
    {
        void OnChanged(object sender, FileSystemEventArgs ea);
    }
}