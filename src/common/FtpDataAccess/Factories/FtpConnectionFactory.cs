using System.Net;

using CommonTypes.Configuration;
using CommonTypes.Constants;

using FluentFTP;

namespace FtpDataAccess.Factories
{
    public class FtpConnectionFactory : IFtpConnectionFactory
    {
        public FtpConnectionFactory(FtpConnectionOptions options)
        {
            _options = options;
        }

        #region Implementation of IFtpConnectionFactory

        public IFtpClient Create()
        {
            var client = new FtpClient
            {
                Host = _options.Host,
                Port = _options.Port,
                Credentials = new NetworkCredential(_options.Login, _options.Password),
                EncryptionMode = _options.EncryptionMode switch
                {
                    FtpEncryption.Explicit => FtpEncryptionMode.Explicit,
                    FtpEncryption.Implicit => FtpEncryptionMode.Implicit,
                    FtpEncryption.None => FtpEncryptionMode.None,
                    _ => FtpEncryptionMode.Auto
                }
            };

            return client;
        }

        #endregion
        
        private readonly FtpConnectionOptions _options;
    }
}
