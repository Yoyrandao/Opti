using System.Security.Cryptography.X509Certificates;

using RestSharp;

namespace Utils.Http
{
    public interface IRestClientFactory
    {
        IRestClient Create();
    }
}