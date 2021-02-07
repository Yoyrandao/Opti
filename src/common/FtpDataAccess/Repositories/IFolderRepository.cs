using System.Collections.Generic;

using FtpDataAccess.Models;

namespace FtpDataAccess.Repositories
{
    public interface IFolderRepository
    {
        void UploadFile(string localFolder, string fileName, string folder);

        IEnumerable<File> GetFiles(string folder);
    }
}
