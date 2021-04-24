using CommonTypes.Contracts;

namespace SyncGateway.Services
{
    public interface IUpdateUserStorageService
    {
        void Update(ChangeSet changeSet);
    }
}
