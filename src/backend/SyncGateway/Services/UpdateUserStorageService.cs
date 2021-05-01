using CommonTypes.Contracts;

using Serilog;

using SyncGateway.Processing;

namespace SyncGateway.Services
{
    public class UpdateUserStorageService : IUpdateUserStorageService
    {
        public UpdateUserStorageService(OperationTask changeProcessingTask, OperationTask deleteProcessingTask)
        {
            _changeProcessingTask = changeProcessingTask;
            _deleteProcessingTask = deleteProcessingTask;
        }

        #region Implementation of IUpdateUserStorageService

        public void Update(ChangeSet changeSet)
        {
            _logger.Information($"Executing change processing task for {changeSet.Identity}");
            
            _changeProcessingTask.Process(changeSet);
        }

        public void Delete(DeleteSet deleteSet)
        {
            _logger.Information($"Executing delete processing task for {deleteSet.Identity}");
            
            _deleteProcessingTask.Process(deleteSet);
        }

        #endregion

        private readonly OperationTask _changeProcessingTask;
        private readonly OperationTask _deleteProcessingTask;

        private readonly ILogger _logger = Log.ForContext<UpdateUserStorageService>();
    }
}
