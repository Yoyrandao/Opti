using RestSharp;

namespace Utils.Http
{
    public interface IRestClientFactory
    {
        IRestClient Create();
    }
}