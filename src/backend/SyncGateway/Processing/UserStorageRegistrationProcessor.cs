using System.Net.Sockets;

using EnsureThat;

using FtpDataAccess.Repositories;

using Serilog;

using SyncGateway.Contracts.In;
using SyncGateway.Exceptions;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class UserStorageRegistrationProcessor : BasicProcessor
    {
        public UserStorageRegistrationProcessor(IStorageRepository storageRepository,
            IRepeater<UserFolderNotCreatedException> repeater)
        {
            _storageRepository = storageRepository;
            _repeater = repeater;
        }

        #region Implementation of BasicProcessor

        public override void Process(object contract)
        {
            EnsureArg.IsNotNull(contract);

            try
            {
                var data = contract as RegistrationContract;
                var folder = $"/{data!.Username}";
                
                _logger.Information($"Executing UserStorageRegistrationProcessor ({data.Username}).");

                _storageRepository.CreateFolder(folder);

                _repeater.Process(() =>
                {
                    if (!_storageRepository.IsFolderExists(folder))
                        throw new UserFolderNotCreatedException(data.Username);
                });

                Successor?.Process(contract);
            }
            catch (SocketException)
            {
                throw new StorageNotAccessibleException();
            }
        }

        #endregion
        
        private readonly IRepeater<UserFolderNotCreatedException> _repeater;
        private readonly IStorageRepository _storageRepository;
        
        private readonly ILogger _logger = Log.ForContext<UserStorageRegistrationProcessor>();
    }
}
