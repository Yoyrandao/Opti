using System.Collections.Generic;
using System.IO;

using FtpDataAccess.Helpers;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        public FolderRepository(ICustomFtpClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        #region Implementation of IFolderRepository

        public void UploadFile(Stream stream, string fileName, string folder)
        {
            _ftpClient.UploadFile(stream, Path.Join(folder, fileName));
        }

        public IEnumerable<File> GetFiles(string folder)
        {
            return _ftpClient.GetListing(folder);
        }

        #endregion

        private readonly ICustomFtpClient _ftpClient;
    }
}