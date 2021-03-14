using System.IO;

namespace BackgroundAgent.Processing.FileSystemEventHandlers
{
    public interface IFsCreateEventHandler
    {
        void OnCreated(object sender, FileSystemEventArgs ea);
    }
}