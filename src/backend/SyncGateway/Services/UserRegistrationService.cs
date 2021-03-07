using Serilog;

using SyncGateway.Contracts.In;
using SyncGateway.Processing;

namespace SyncGateway.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        public UserRegistrationService(OperationTask processingTask)
        {
            _processingTask = processingTask;
        }

        #region Implementation of IUserRegistrationService

        public void Register(RegistrationContract contract)
        {
            _logger.Information($"Executing UpdateUserStorageService ({contract.Username})");

            _processingTask.Process(contract);
        }

        #endregion
        
        private readonly OperationTask _processingTask;
        
        private readonly ILogger _logger = Log.ForContext<UpdateUserStorageService>();
    }
}
