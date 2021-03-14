using DataAccess.Repositories;

using EnsureThat;

using Serilog;

using SyncGateway.Contracts.In;
using SyncGateway.Exceptions;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class UserDatabaseRegistrationProcessor : BasicProcessor
    {
        public UserDatabaseRegistrationProcessor(IUserRepository                       userRepository,
                                                 IRepeater<UserNotInDatabaseException> repeater)
        {
            _userRepository = userRepository;
            _repeater = repeater;
        }

        #region Implementation of BasicProcessor

        public override void Process(object contract)
        {
            EnsureArg.IsNotNull(contract);
            var username = (contract as RegistrationContract)!.Username;

            _logger.Information($"Executing UserDatabaseRegistrationProcessor ({username}).");

            _userRepository.Register(username);

            _repeater.Process(() =>
            {
                if (_userRepository.GetByLogin(username) == null)
                    throw new UserNotInDatabaseException(username);
            });

            Successor?.Process(contract);
        }

        #endregion

        private readonly IRepeater<UserNotInDatabaseException> _repeater;
        private readonly IUserRepository _userRepository;

        private readonly ILogger _logger = Log.ForContext<UserDatabaseRegistrationProcessor>();
    }
}