using SyncGateway.Contracts.In;

namespace SyncGateway.Services
{
    public interface IUserRegistrationService
    {
        void Register(RegistrationContract contract);
    }
}
