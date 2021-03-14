using BackgroundAgent.Processing.Models;

using RestSharp;

namespace BackgroundAgent.Requests
{
    public interface IRequestFactory
    {
        IRestRequest CreateGetFileStateRequest(string filename);

        IRestRequest CreateCheckCompressionRequest(FileMetaInfo metaInfo);
    }
}