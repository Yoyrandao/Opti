using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsCreateEventHandler : IEventHandler
    {
        void OnCreated(object sender, FileSystemEventArgs ea);
    }
}