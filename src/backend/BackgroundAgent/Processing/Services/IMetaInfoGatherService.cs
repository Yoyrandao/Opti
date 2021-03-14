using BackgroundAgent.Processing.Models;

namespace BackgroundAgent.Processing.Services
{
    public interface IMetaInfoGatherService
    {
        FileMetaInfo Gather(string path);
    }
}