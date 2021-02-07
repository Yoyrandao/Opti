using System;
using System.Collections.Generic;
using System.IO;

using FluentFTP;

using FtpDataAccess.Factories;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Repositories
{
    public class FolderRepository : IFolderRepository, IDisposable
    {
        private readonly IFtpClient _client;

        public FolderRepository(IFtpConnectionFactory connectionFactory)
        {
            _client = connectionFactory.Create();
        }

        public void Dispose()
        {
            _client.Disconnect();
            _client?.Dispose();
        }

        #region Implementation of IFolderRepository

        public void UploadFile(string localFolder, string fileName, string folder)
        {
            _client.Connect();

            _client.UploadFile(Path.Join(localFolder, fileName),
                Path.Join(folder, fileName),
                FtpRemoteExists.Overwrite,
                false,
                FtpVerify.Retry);

            _client.Disconnect();
        }

        public IEnumerable<File> GetFiles(string folder)
        {
            _client.Connect();

            var files = new List<File>();

            foreach (var item in _client.GetListing(folder))
            {
                switch (item.Type)
                {
                    case FtpFileSystemObjectType.File:
                        files.Add(new File { Name = item.Name, Folder = folder });

                        break;

                    default:
                        throw new Exception("Incorrect storage structure"); // Replace with concrete Exception
                }
            }

            _client.Disconnect();

            return files;
        }

        #endregion
    }
}
