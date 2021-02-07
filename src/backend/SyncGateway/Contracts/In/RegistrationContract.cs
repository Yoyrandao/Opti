using System;

namespace SyncGateway.Contracts.In
{
    [Serializable]
    public record RegistrationContract
    {
        public string Username { get; init; }
    }
}
