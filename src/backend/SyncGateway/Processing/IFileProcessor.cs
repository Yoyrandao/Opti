using System.Collections.Generic;

using DataAccess.Domain;

namespace SyncGateway.Processing
{
    public interface IFileProcessor
    {
        IEnumerable<File> GetFilesFromFolder(string folder);
    }
}
