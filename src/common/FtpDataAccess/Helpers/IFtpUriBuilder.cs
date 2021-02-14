using CommonTypes.Configuration;

namespace FtpDataAccess.Helpers
{
    public interface IFtpUriBuilder
    {
        string Build(FtpConnectionOptions options);
    }
}