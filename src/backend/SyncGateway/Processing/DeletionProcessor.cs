using System.Linq;

using CommonTypes.Contracts;

using DataAccess.Helpers;
using DataAccess.Repositories;

using EnsureThat;

using FtpDataAccess.Repositories;

using Serilog;

namespace SyncGateway.Processing
{
    public class DeletionProcessor : BasicProcessor
    {
        public DeletionProcessor(IFilePartRepository filePartRepository, IFolderRepository storageRepository,
                                 ITransactionFactory transactionFactory)
        {
            _filePartRepository = filePartRepository;
            _storageRepository = storageRepository;
            _transactionFactory = transactionFactory;
        }

        public override void Process(object contract)
        {
            var data = contract as DeleteSet;
            EnsureArg.IsNotNull(data);

            _logger.Information($"Executing DeletionProcessor ({data.Identity}).");

            var transaction = _transactionFactory.BeginTransaction();

            var parts = _filePartRepository.GetFileByName(data.Filename).Select(x => x.PartName);

            foreach (var part in parts)
            {
                _storageRepository.RemoveFile(part, data.Identity);
            }

            _filePartRepository.DeleteFile(data.Filename);
            transaction.Commit();
        }

        private readonly ITransactionFactory _transactionFactory;
        private readonly IFilePartRepository _filePartRepository;
        private readonly IFolderRepository _storageRepository;

        private readonly ILogger _logger = Log.ForContext<DeletionProcessor>();
    }
}