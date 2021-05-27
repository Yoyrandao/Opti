using System.Collections.Generic;
using System.IO;

using FtpDataAccess.Helpers;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        public FolderRepository(IFtpClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        #region Implementation of IFolderRepository

        public void UploadFile(Stream stream, string filename, string folder)
        {
            _ftpClient.UploadFile(stream, Path.Join(folder, filename));
        }

        public void RemoveFile(string filename, string folder)
        {
            _ftpClient.DeleteFile(Path.Join(folder, filename));
        }

        public long GetFileSize(string filename, string folder)
        {
            return _ftpClient.GetFileSize(Path.Join(folder, filename));
        }

        public IEnumerable<File> GetFiles(string folder)
        {
            return _ftpClient.GetListing(folder);
        }

        #endregion

        private readonly IFtpClient _ftpClient;
    }
}