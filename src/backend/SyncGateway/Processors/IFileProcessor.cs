using System.Collections.Generic;

using DataAccess.Domain;

namespace SyncGateway.Processors
{
    public interface IFileProcessor
    {
        IEnumerable<File> GetFilesFromFolder(string folder);
    }
}