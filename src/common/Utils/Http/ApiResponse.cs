#nullable enable
using System;

namespace Utils.Http
{
    [Serializable]
    public record ApiResponse
    {
        public ResponseError? Error { get; init; }

        public object? Data { get; init; }
    }
}