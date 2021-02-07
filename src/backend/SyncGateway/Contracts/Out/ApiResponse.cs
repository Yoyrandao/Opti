#nullable enable
using System;

namespace SyncGateway.Contracts.Out
{
    [Serializable]
    public record ApiResponse
    {
        public ResponseError? Error { get; init; }

        public object? Data { get; init; }
    }
}
