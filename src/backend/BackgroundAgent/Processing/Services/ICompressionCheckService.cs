using BackgroundAgent.Processing.Models;

namespace BackgroundAgent.Processing.Services
{
    public interface ICompressionCheckService
    {
        bool Check(FileMetaInfo metaInfo);
    }
}