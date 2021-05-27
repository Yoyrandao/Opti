using CommonTypes.Configuration;

using FtpDataAccess.Factories;
using FtpDataAccess.Helpers;

using Microsoft.Extensions.Configuration;

using NUnit.Framework;

namespace FtpDataAccess.Test.BlackBox
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.Tests.json", false, true)
               .Build();

            FtpConnectionOptions options = new FtpConnectionOptions();
            configuration.Bind("FtpConnection", options);

            _target = new FtpClient(new FtpWebRequestFactory(options, new FtpUriBuilder()));
        }

        [Test]
        public void GetFileSizeTest()
        {
            _target.GetFileSize("/aaron/a.txt");
        }

        private IFtpClient _target;
    }
}