#nullable enable
namespace SyncGateway.Contracts.Out
{
    public record ApiResponse
    {
        public ResponseError? Error { get; init; }

        public object? Data { get; init; }
    }
}
