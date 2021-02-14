using System.Collections.Generic;
using System.IO;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Repositories
{
    public interface IFolderRepository
    {
        void UploadFile(Stream fileStream, string fileName, string folder);

        IEnumerable<File> GetFiles(string folder);
    }
}
