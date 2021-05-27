using System.Linq;

using DataAccess.Repositories;

using FtpDataAccess.Repositories;

namespace SyncGateway.Services
{
    public class FileInfoService : IFileInfoService
    {
        public FileInfoService(IFolderRepository folderRepository, IFilePartRepository filePartRepository)
        {
            _folderRepository = folderRepository;
            _filePartRepository = filePartRepository;
        }

        public long GetSize(string filename, string identity)
        {
            var partNames = _filePartRepository.GetFileByName(filename).Select(x => x.PartName);
            return partNames.Sum(partName => _folderRepository.GetFileSize(partName, identity));
        }

        private readonly IFolderRepository _folderRepository;
        private readonly IFilePartRepository _filePartRepository;
    }
}