using System.Net;

using CommonTypes.Configuration;
using CommonTypes.Constants;

using FtpDataAccess.Helpers;

namespace FtpDataAccess.Factories
{
    public class FtpWebRequestFactory : IFtpWebRequestFactory
    {
        public FtpWebRequestFactory(FtpConnectionOptions options, IFtpUriBuilder uriBuilder)
        {
            _options = options;
            _baseUri = uriBuilder.Build(_options);
        }

        #region Implementation of IFtpWebRequestFactory

        public IFtpWebRequestFactory CreateFor(string resourcePath)
        {
            var request = (FtpWebRequest) WebRequest.Create(_baseUri + resourcePath);
            request.Credentials = new NetworkCredential(_options.Login, _options.Password);
            request.EnableSsl = _options.EncryptionMode != FtpEncryption.None;

            _request = request;

            return this;
        }

        public IFtpWebRequestFactory With(string method)
        {
            _request.Method = method;

            return this;
        }

        public FtpWebRequest Build()
        {
            return _request;
        }

        #endregion

        private FtpWebRequest _request;

        private readonly string _baseUri;
        private readonly FtpConnectionOptions _options;
    }
}