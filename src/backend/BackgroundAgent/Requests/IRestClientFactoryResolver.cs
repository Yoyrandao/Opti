using Utils.Http;

namespace BackgroundAgent.Requests
{
    public interface IRestClientFactoryResolver
    {
        IRestClientFactory Resolve(Endpoint endpoint);
    }
}