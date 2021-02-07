using SyncGateway.Contracts.In;
using SyncGateway.Processors;

namespace SyncGateway.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IProcessor[] _processors;

        public UserRegistrationService(IProcessor[] processors)
        {
            _processors = processors;
        }

        #region Implementation of IUserRegistrationService

        public void Register(RegistrationContract contract)
        {
            foreach (var processor in _processors)
                processor.Process(contract);
        }

        #endregion
    }
}
