using BackgroundAgent.Processing.Models;

namespace BackgroundAgent.Processing.Logic
{
    public interface IFileMetaInformationProvider
    {
        FileMetaInfo GetInformation(string path);
    }
}