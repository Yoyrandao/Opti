using Serilog;

using SyncGateway.Contracts.Common;
using SyncGateway.Processing;

namespace SyncGateway.Services
{
    public class UpdateUserStorageService : IUpdateUserStorageService
    {
        public UpdateUserStorageService(OperationTask processingTask)
        {
            _processingTask = processingTask;
        }

        #region Implementation of IUpdateUserStorageService

        public void Update(ChangeSet changeSet)
        {
            _logger.Information($"Executing UpdateUserStorageService ({changeSet.Identity})");
            
            _processingTask.Process(changeSet);
        }

        #endregion

        private readonly OperationTask _processingTask;

        private readonly ILogger _logger = Log.ForContext<UpdateUserStorageService>();
    }
}
