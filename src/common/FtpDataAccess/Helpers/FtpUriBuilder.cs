using CommonTypes.Configuration;

namespace FtpDataAccess.Helpers
{
    public class FtpUriBuilder : IFtpUriBuilder
    {
        #region Implementation of IFtpUriBuilder

        public string Build(FtpConnectionOptions options)
        {
            return $"ftp://{options.Host}:{options.Port}/";
        }

        #endregion
    }
}