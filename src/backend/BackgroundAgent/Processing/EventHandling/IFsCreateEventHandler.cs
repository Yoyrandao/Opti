using System.IO;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IFsCreateEventHandler
    {
        void OnCreated(object sender, FileSystemEventArgs ea);
    }
}