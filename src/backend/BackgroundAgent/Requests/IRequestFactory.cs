using RestSharp;

namespace BackgroundAgent.Requests
{
    public interface IRequestFactory
    {
        IRestRequest CreateGetFileStateRequest(string filename);
    }
}