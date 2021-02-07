using DataAccess.Repositories;

using EnsureThat;

using SyncGateway.Contracts.In;
using SyncGateway.Exceptions;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class UserDatabaseRegistrationProcessor : BasicProcessor
    {
        private readonly IRepeater<UserNotInDatabaseException> _repeater;

        private readonly IUserRepository _userRepository;

        public UserDatabaseRegistrationProcessor(IUserRepository userRepository,
            IRepeater<UserNotInDatabaseException> repeater)
        {
            _userRepository = userRepository;
            _repeater = repeater;
        }

        #region Implementation of IProcessor

        public override void Process(object contract)
        {
            EnsureArg.IsNotNull(contract);

            var username = (contract as RegistrationContract)!.Username;
            _userRepository.Register(username);

            _repeater.Process(() =>
            {
                if (_userRepository.GetByLogin(username) == null)
                    throw new UserNotInDatabaseException();
            });

            Successor?.Process(contract);
        }

        #endregion
    }
}
