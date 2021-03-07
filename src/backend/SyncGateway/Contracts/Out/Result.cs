using System;

namespace SyncGateway.Contracts.Out
{
    [Serializable]
    public record Result
    {
        public bool Success { get; init; }
    }
}
