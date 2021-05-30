using BackgroundAgent.Processing.Models;

using CommonTypes.Contracts;

using RestSharp;

namespace BackgroundAgent.Requests
{
    public interface IRequestFactory
    {
        IRestRequest CreateGetFileStateRequest(string filename);

        IRestRequest CreateCheckCompressionRequest(FileMetaInfo metaInfo);

        IRestRequest CreateChangeSetSendingRequest(ChangeSet changeSet);

        IRestRequest CreateDeleteSetSendingRequest(DeleteSet deleteSet);

        IRestRequest CreateGetFileInfoRequest(string filename);
    }
}