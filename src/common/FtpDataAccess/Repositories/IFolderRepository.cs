using System.Collections.Generic;
using System.IO;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Repositories
{
    public interface IFolderRepository
    {
        void UploadFile(Stream fileStream, string fileName, string folder);

        void RemoveFile(string filename, string folder);

        long GetFileSize(string filename, string folder);

        IEnumerable<File> GetFiles(string folder);
    }
}
