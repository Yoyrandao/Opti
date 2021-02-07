using CommonTypes.Constants;

namespace SyncGateway.Contracts.Out
{
    public record ResponseError
    {
        public ErrorCode ErrorCode { get; init; }

        public string Message { get; init; }
    }
}
