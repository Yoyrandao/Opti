using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using CommonTypes.Contracts;

using RestSharp;

namespace BackgroundAgent.Requests
{
    public class RequestFactory : IRequestFactory
    {
        #region Implementation of IRequestFactory

        public IRestRequest CreateGetFileStateRequest(string filename)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = Routes.Sync
            };
            request.AddParameter("filename", filename);

            return request;
        }

        public IRestRequest CreateCheckCompressionRequest(FileMetaInfo metaInfo)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = Routes.CheckCompression
            };

            request.AddJsonBody(metaInfo);

            return request;
        }

        public IRestRequest CreateChangeSetSendingRequest(ChangeSet changeSet)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = Routes.Update
            };

            request.AddJsonBody(changeSet);

            return request;
        }

        #endregion
    }
}