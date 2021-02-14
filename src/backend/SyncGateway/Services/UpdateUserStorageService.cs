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
            _processingTask.Process(changeSet);
        }

        #endregion

        private readonly OperationTask _processingTask;
    }
}
