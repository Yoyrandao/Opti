using System.Net;

namespace FtpDataAccess.Factories
{
    public interface IFtpWebRequestFactory
    {
        IFtpWebRequestFactory CreateFor(string resourcePath);

        IFtpWebRequestFactory With(string method);

        FtpWebRequest Build();
    }
}