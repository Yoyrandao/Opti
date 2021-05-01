using CommonTypes.Contracts;

namespace SyncGateway.Services
{
    public interface IUpdateUserStorageService
    {
        void Update(ChangeSet changeSet);

        void Delete(DeleteSet deleteSet);
    }
}
