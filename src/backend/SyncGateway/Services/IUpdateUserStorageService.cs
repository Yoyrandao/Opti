using SyncGateway.Contracts.Common;

namespace SyncGateway.Services
{
    public interface IUpdateUserStorageService
    {
        void Update(ChangeSet changeSet);
    }
}
