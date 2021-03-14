using System.IO;

namespace BackgroundAgent.FileSystemEventHandlers
{
    public interface IFsCreateEventHandler
    {
        void OnCreated(object sender, FileSystemEventArgs ea);
    }
}