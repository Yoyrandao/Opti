using System;

using CommonTypes.Constants;

namespace Utils.Http
{
    [Serializable]
    public record ResponseError
    {
        public ErrorCode ErrorCode { get; init; }

        public string Message { get; init; }
    }
}