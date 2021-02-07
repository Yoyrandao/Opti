using System;

namespace SyncGateway.Contracts.Out
{
    [Serializable]
    public record RegistrationResult
    {
        public bool Success { get; init; }
    }
}
