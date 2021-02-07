using System.Net.Sockets;

using FtpDataAccess.Repositories;

using SyncGateway.Contracts.In;
using SyncGateway.Exceptions;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class UserStorageRegistrationProcessor : BasicProcessor
    {
        private readonly IRepeater<UserFolderNotCreatedException> _repeater;

        private readonly IStorageRepository _storageRepository;

        public UserStorageRegistrationProcessor(IStorageRepository storageRepository,
            IRepeater<UserFolderNotCreatedException> repeater)
        {
            _storageRepository = storageRepository;
            _repeater = repeater;
        }

        #region Implementation of IProcessor

        public override void Process(object contract)
        {
            try
            {
                var data = contract as RegistrationContract;
                var folder = $"/{data!.Username}";

                _storageRepository.CreateFolder(folder);

                _repeater.Process(() =>
                {
                    if (!_storageRepository.IsFolderExists(folder))
                        throw new UserFolderNotCreatedException();
                });

                Successor?.Process(contract);
            }
            catch (SocketException)
            {
                throw new StorageNotAccessibleException();
            }
        }

        #endregion
    }
}
