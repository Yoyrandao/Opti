using FtpDataAccess.Helpers;

namespace FtpDataAccess.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        public StorageRepository(IFtpClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        #region Implementation of IStorageRepository

        public void CreateFolder(string name)
        { 
            _ftpClient.CreateDirectory(name);
        }

        public bool IsFolderExists(string name)
        {
            return _ftpClient.IsDirectoryExists(name);
        }

        #endregion

        private readonly IFtpClient _ftpClient;
    }
}