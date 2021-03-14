using BackgroundAgent.Constants;

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

        #endregion
    }
}