using System.Collections.Generic;
using System.IO;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Helpers
{
    public interface IFtpClient
    {
        void UploadFile(Stream fileStream, string remotePath);

        void DeleteFile(string remotePath);

        ICollection<File> GetListing(string folder);

        void CreateDirectory(string path);
        
        bool IsDirectoryExists(string path);

        long GetFileSize(string path);
    }
}