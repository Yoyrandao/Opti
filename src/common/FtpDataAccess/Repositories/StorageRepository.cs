using System;

using FluentFTP;

using FtpDataAccess.Factories;

namespace FtpDataAccess.Repositories
{
    public class StorageRepository : IStorageRepository, IDisposable
    {
        private readonly IFtpClient _client;

        public StorageRepository(IFtpConnectionFactory connectionFactory)
        {
            _client = connectionFactory.Create();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _client.Disconnect();
            _client?.Dispose();
        }

        #endregion

        #region Implementation of IStorageRepository

        public void CreateFolder(string name)
        {
            _client.Connect();
            _client.CreateDirectory(name, true);
            _client.Disconnect();
        }

        public bool IsFolderExists(string name)
        {
            _client.Connect();
            var result = _client.DirectoryExists(name);
            _client.Disconnect();

            return result;
        }

        #endregion
    }
}
