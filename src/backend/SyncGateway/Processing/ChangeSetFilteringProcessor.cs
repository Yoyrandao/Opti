using System.Linq;

using DataAccess.Repositories;

using EnsureThat;

using FtpDataAccess.Repositories;

using SyncGateway.Contracts.Common;
using SyncGateway.Exceptions;

namespace SyncGateway.Processing
{
    public class ChangeSetFilterProcessor : BasicProcessor
    {
        public ChangeSetFilterProcessor(IStorageRepository storageRepository, IFilePartRepository filePartRepository)
        {
            _storageRepository = storageRepository;
            _filePartRepository = filePartRepository;
        }

        #region Implementation of BasicProcessor

        public override void Process(object contract)
        {
            var data = contract as ChangeSet;
            EnsureArg.IsNotNull(data);

            if (_storageRepository.IsFolderExists(data.Identity))
            {
                throw new UserFolderNotCreatedException();
            }

            data.Records = data.Records.Where(x => !_filePartRepository.IsFilePartExists(data.Identity, x.PartName))
               .ToList();

            Successor.Process(data);
        }

        #endregion

        private readonly IStorageRepository _storageRepository;
        private readonly IFilePartRepository _filePartRepository;
    }
}
