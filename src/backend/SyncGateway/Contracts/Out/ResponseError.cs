using System;

using CommonTypes.Constants;

namespace SyncGateway.Contracts.Out
{
    [Serializable]
    public record ResponseError
    {
        public ErrorCode ErrorCode { get; init; }

        public string Message { get; init; }
    }
}
